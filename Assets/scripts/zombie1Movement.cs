
// Developer(s): Evita Kanaan, Tristan Hughes, Jazzel Radaza
// Last Updated: 12-06-24
// Enemy Movement Class

// the intended purpose of this class is to control enemy movement.
// it also provides methods for attacking, collision special functions, and character death 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{

    private Animator zombieAnimator;
    private Animator bossAnimator;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    public bool attacking = false;
    public float health = 10.0f;
    public float maxHealth;

    public float damageDealt = 3.0f;

    public bool isArmored = false;
    public bool isArmorBroken = false;
    public bool isGiant = false;

    private ZombieSpawner zombieSpawner;

    private AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;
    


    //public float spawnDelay = 0.0f;


    // hit reation variables
    private SpriteRenderer enemyRender;
    private Color enemyOriginal;
    public float flashTime = 0.2f;

    virtual public void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        bossAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component that's attached to enemy
        audioSource = GetComponent<AudioSource>();

        enemyRender = GetComponent<SpriteRenderer>();
        if(enemyRender != null)
        {
            enemyOriginal = enemyRender.color;
        }
        maxHealth = health;


    }

    virtual public void Update()
    {
        if (!attacking){
            if(zombieAnimator != null){
                zombieAnimator.SetTrigger("basicZombieMove");
            }
            if(bossAnimator != null){
                bossAnimator.SetTrigger("bossMove");
            }
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        } 

    }

    // this function is another coroutine that makes the enemy flash white
    public IEnumerator hitReaction()
    {
        if(enemyRender != null)
        {
            enemyRender.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            enemyOriginal = enemyRender.color;
        }
        
    }

    // this function destroys the game object to simulate a death
    virtual public void onCharDeath()
    {
        Debug.Log("character death");
        if (isGiant)
        {
            Debug.Log("is giant");
            GameManager.Instance.bossKillCount();
        }
        //audioSource.PlayOneShot(deathSound);
        Destroy(gameObject);
    }

    // this function is a special event function provided by the monobehavior base class
    // if the enemy collides with a defense then it starts attacking
    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(zombieAnimator != null){
            zombieAnimator.SetTrigger("basicZombieAttack");
        }
        if(bossAnimator != null){
                bossAnimator.SetTrigger("bossAttack");
            }
        if (collision.gameObject.CompareTag("defense"))
        {
            attacking = true;
            StartCoroutine(attack(collision.gameObject));
        }
    }

    // this function is a special event function provided by the monobehavior base class
    // if the enemy is finishing the collision it will stop attack
    void OnCollisionExit2D(Collision2D collision)
    {   
        if(zombieAnimator != null){
            zombieAnimator.SetTrigger("basicZombieMove");
        }
        if(bossAnimator != null){
                bossAnimator.SetTrigger("bossMove");
            }
        if (collision.gameObject.CompareTag("defense"))
        {
            attacking = false;
        }
    }

    // this is an attacking coroutines
    // it is used to do damage to the defense when the zombie is attacking
    virtual public IEnumerator attack(GameObject defense)
    {
        while(defense != null && attacking)
        {
            temporaryDefense defenseComponent = defense.GetComponent<temporaryDefense>();
            axe_knight_attack axeKnightComponent = defense.GetComponent<axe_knight_attack>();
            GameOverObject gameEnder = defense.GetComponent<GameOverObject>();

            if (defenseComponent != null){
                Debug.Log("Zombie attacking archers");
                defenseComponent.takeDamage(damageDealt);
            }
            else if (axeKnightComponent != null){
                Debug.Log("Zombie attacking soldier");
                axeKnightComponent.TakeDamage(damageDealt);
            }
            else{
                Debug.Log("No valid defense found");
                if (gameEnder != null){
                    gameEnder.takeDamage(damageDealt);
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
    }

    public void takeDamage(float damageTaken)
    {
        //audioSource.PlayOneShot(hitSound);
        if (!isArmorBroken && isArmored)
        {
            health -= damageTaken * 0.5f;
            bool armorShouldBreak;
            armorShouldBreak = health <= maxHealth /2;
            if (armorShouldBreak)
            {
                isArmorBroken = true;
            }
        }
        else
        {
            health = health - damageTaken;
            if(health <= 0)
            {
                if (zombieAnimator != null){
                    zombieAnimator.SetTrigger("basicZombieDead"); // Trigger death animation
                }
                if (bossAnimator != null){
                    bossAnimator.SetTrigger("bossDead"); // Trigger death animation
                }
                StartCoroutine(HandleDeath()); // Wait before destroying
            }
            else
            {
                StartCoroutine(hitReaction());
            }
        }
    }

    // Coroutine to handle delay before destruction
    private IEnumerator HandleDeath()
    {
        // Get the length of the animation clip (optional)
        float animationLength = 1.5f; // Replace with your animation's duration
        if (zombieAnimator != null)
        {
            AnimatorStateInfo stateInfo = zombieAnimator.GetCurrentAnimatorStateInfo(0);
            animationLength = stateInfo.length; // Dynamically fetch animation length
        }

        yield return new WaitForSeconds(animationLength); // Wait for the animation to finish
        onCharDeath(); // Destroy the zombie
    }

    // Handles soldier death
    private void DestroyZombie()
    {
        Debug.Log("Soldier has been destroyed.");
        Destroy(gameObject); // Remove zombie from the game
    }

    /*
    void MoveZombie()
    {
        // Basic movement to the right
        Vector2 movement = new Vector2(moveSpeed, 0);
        rb.velocity = movement; // Apply the movement to the Rigidbody2D
    }
    */
}
