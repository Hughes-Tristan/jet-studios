
// Developer(s): Evita Kanaan, Tristan Hughes
// Last Updated: 11-21-24
// Enemy Movement Class

// the intended purpose of this class is to control enemy movement.
// it also provides methods for attacking, collision special functions, and character death 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool attacking = false;
    public float health = 10.0f;

    // hit reation variables
    private SpriteRenderer enemyRender;
    private Color enemyOriginal;
    public float flashTime = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component that's attached to enemy

        enemyRender = GetComponent<SpriteRenderer>();
        if(enemyRender != null)
        {
            enemyOriginal = enemyRender.color;
        }
    }

    void Update()
    {
        if (!attacking){
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        } 
        if (health <= 0)
        {
            onCharDeath();
        }
    }

    // this function is another coroutine that makes the enemy flash white
    public IEnumerator hitReaction()
    {
        if(enemyRender != null)
        {
            enemyRender.color = Color.white;
            yield return new WaitForSeconds(0.2f);

        }
        
    }

    // this function destroys the game object to simulate a death
    public void onCharDeath()
    {
        Destroy(gameObject);
    }

    // this function is a special event function provided by the monobehavior base class
    // if the enemy collides with a defense then it starts attacking
    void OnCollisionEnter2D(Collision2D collision)
    {
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
        if (collision.gameObject.CompareTag("defense"))
        {
            attacking = false;
        }
    }

    // this is an attacking coroutines
    // it is used to do damage to the defense when the zombie is attacking
    public IEnumerator attack(GameObject defense)
    {
        while(defense != null && attacking)
        {
            temporaryDefense defenseComponent = defense.GetComponent<temporaryDefense>();
            GameOverObject gameEnder = defense.GetComponent<GameOverObject>();
            if (defenseComponent == null)
            {
                Debug.Log("no defense found");
                if (gameEnder != null)
                {
                    gameEnder.takeDamage(1.0f);
                }
                
            } else
            {
                defenseComponent.takeDamage(1.0f);
            }
            yield return new WaitForSeconds(1f);
        }
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
