// Author: Jazzel Radaza
// Created: 2024-11-13
// Last Modified: 2024-12-05 by Jazzel (refactored death logic)
// Description: Manages the Axe Knight's combat behavior, including attacking zombies in range, taking damage, and handling death animations.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe_knight_attack : MonoBehaviour
{
    private Animator axeAnimator; // Animator for controlling attack and idle animations

    [Header("Soldier Attributes")]
    [SerializeField] private float health = 50f; // Axe Knight's initial health
    [SerializeField] private float attackDamage = 35f; // Damage dealt to zombies
    [SerializeField] private float attackRate = 0.5f; // Time between attacks

    private List<ZombieMovement> zombiesInRange = new List<ZombieMovement>(); // List of zombies within attack range
    private bool isAttacking = false; // Indicates whether the knight is currently attacking

    public float Health => health; // Read-only property to access health

    // Sets the Axe Knight's health, ensuring it doesn't drop below 0
    public void SetHealth(float value)
    {
        health = Mathf.Max(0, value);
    }

    void Start()
    {
        axeAnimator = GetComponent<Animator>(); // Initialize the animator
    }

    // Triggered when a zombie enters the knight's attack range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (axeAnimator != null)
        {
            axeAnimator.SetTrigger("AxeAttack"); // Trigger attack animation
        }

        ZombieMovement enemy = collision.GetComponent<ZombieMovement>();
        if (enemy != null && !zombiesInRange.Contains(enemy))
        {
            Debug.Log("Zombie detected and added to the list.");
            zombiesInRange.Add(enemy); // Add zombie to the attack list

            // Start attacking if not already doing so
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    // Triggered when a zombie exits the knight's attack range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (axeAnimator != null)
        {
            axeAnimator.SetTrigger("AxeIdle"); // Switch back to idle animation
        }

        ZombieMovement enemy = collision.GetComponent<ZombieMovement>();
        if (enemy != null && zombiesInRange.Contains(enemy))
        {
            Debug.Log("Zombie left the range and removed from the list.");
            zombiesInRange.Remove(enemy); // Remove zombie from the attack list
        }
    }

    // Coroutine for continuously attacking zombies in range
    private IEnumerator Attack()
    {
        isAttacking = true;

        while (zombiesInRange.Count > 0)
        {
            ZombieMovement target = zombiesInRange[0]; // Attack the first zombie in the list
            if (target != null)
            {
                Debug.Log("Soldier attacking zombie: " + target.name);
                target.takeDamage(attackDamage); // Inflict damage on the zombie

                // Remove the zombie from the list if it dies
                if (target.health <= 0f)
                {
                    Debug.Log("Zombie " + target.name + " died and removed from the list.");
                    zombiesInRange.Remove(target);
                }
            }

            yield return new WaitForSeconds(attackRate); // Wait before the next attack
        }

        isAttacking = false; // Stop attacking when no zombies are in range
    }

    // Handles the knight taking damage from zombies
    public void TakeDamage(float damageTaken)
    {
        SetHealth(health - damageTaken); // Reduce health
        Debug.Log("Soldier took damage. Current health: " + health);

        if (health <= 0f)
        {
            Debug.Log("Soldier has died.");
            if (axeAnimator != null)
            {
                axeAnimator.SetTrigger("AxeDead"); // Trigger death animation
            }
            StartCoroutine(HandleDeath()); // Handle the death process
        }
    }

    // Coroutine to delay the knight's destruction for death animation
    private IEnumerator HandleDeath()
    {
        float animationLength = 1.5f; // Default duration for death animation
        if (axeAnimator != null)
        {
            AnimatorStateInfo stateInfo = axeAnimator.GetCurrentAnimatorStateInfo(0);
            animationLength = stateInfo.length; // Get actual animation length
        }

        yield return new WaitForSeconds(animationLength); // Wait for animation to finish
        DestroyKnight(); // Remove the knight from the game
    }

    // Destroys the knight object
    private void DestroyKnight()
    {
        Debug.Log("Soldier has been destroyed.");
        Destroy(gameObject);
    }
}
