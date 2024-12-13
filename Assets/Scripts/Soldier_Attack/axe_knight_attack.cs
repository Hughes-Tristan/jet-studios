using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe_knight_attack : MonoBehaviour
{
    private Animator axeAnimator;

    [Header("Soldier Attributes")]
    [SerializeField] private float health = 50f; // Soldier's initial health
    [SerializeField] private float attackDamage = 30f; // Damage dealt to enemies
    [SerializeField] private float attackRate = 0.5f; // Time between attacks

    private List<ZombieMovement> zombiesInRange = new List<ZombieMovement>(); // List of zombies in range
    private bool isAttacking = false; // To check if the soldier is already attacking

    public float Health => health; // Property for read-only health access

    // Method to set health (controlled)
    public void SetHealth(float value)
    {
        health = Mathf.Max(0, value); // Prevent health from dropping below 0
    }

    void Start(){
        axeAnimator = GetComponent<Animator>();
    }


    // Triggered when an enemy enters the soldier's attack range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(axeAnimator != null){
            axeAnimator.SetTrigger("AxeAttack");
        }
        ZombieMovement enemy = collision.GetComponent<ZombieMovement>();
        if (enemy != null && !zombiesInRange.Contains(enemy))
        {
            Debug.Log("Zombie detected and added to the list.");
            zombiesInRange.Add(enemy); // Add the zombie to the list

            // Start attacking if not already attacking
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    // Triggered when an enemy leaves the soldier's attack range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(axeAnimator != null){
            axeAnimator.SetTrigger("AxeIdle");
        }
        ZombieMovement enemy = collision.GetComponent<ZombieMovement>();
        if (enemy != null && zombiesInRange.Contains(enemy))
        {
            Debug.Log("Zombie left the range and removed from the list.");
            zombiesInRange.Remove(enemy); // Remove the zombie from the list
        }
    }

    // Coroutine to handle continuous attacking
    private IEnumerator Attack()
    {
        isAttacking = true;

        while (zombiesInRange.Count > 0)
        {
            ZombieMovement target = zombiesInRange[0]; // Attack the first zombie in the list
            if (target != null)
            {
                Debug.Log("Soldier attacking zombie: " + target.name);
                target.takeDamage(attackDamage); // Deal damage to the zombie

                // Remove the zombie if it dies
                if (target.health <= 0f)
                {
                    Debug.Log("Zombie " + target.name + " died and removed from the list.");
                    zombiesInRange.Remove(target);
                }
            }

            yield return new WaitForSeconds(attackRate); // Wait before the next attack
        }

        isAttacking = false; // Stop attacking when there are no zombies in range
    }

    // Handles taking damage from a zombie
    public void TakeDamage(float damageTaken){
        SetHealth(health - damageTaken); // Adjust health using the setter

        Debug.Log("Soldier took damage. Current health: " + health);

        if (health <= 0f){
            Debug.Log("Soldier has died.");
            if (axeAnimator != null)
            {
                axeAnimator.SetTrigger("AxeDead"); // Trigger death animation
            }
            StartCoroutine(HandleDeath()); // Wait before destroying
        }
    }

    // Coroutine to handle delay before destruction
    private IEnumerator HandleDeath()
    {
        // Get the length of the animation clip (optional)
        float animationLength = 1.5f; // Replace with your animation's duration
        if (axeAnimator != null)
        {
            AnimatorStateInfo stateInfo = axeAnimator.GetCurrentAnimatorStateInfo(0);
            animationLength = stateInfo.length; // Dynamically fetch animation length
        }

        yield return new WaitForSeconds(animationLength); // Wait for the animation to finish
        DestroyKnight(); // Destroy the knight
    }

    // Handles soldier death
    private void DestroyKnight()
    {
        Debug.Log("Soldier has been destroyed.");
        Destroy(gameObject); // Remove soldier from the game
    }

}
