using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Developer(s): Evita Kanaan
// Last Updated: 11-21-24
// Enemy Attack Class

// the intended purpose of this class is to control enemy attacks.

public class ZombieAttack : MonoBehaviour
{
    // Reference to the collider for the attack area
    private Collider2D attackCollider;

    // Start is called before the first frame update
    // In this function, initialized attack collider object
    void Start()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    // This will detect if the zombie is in contact with a target object (like a Player or Tower but is subject to change)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the "Player" or "Tower" tag
        if (other.CompareTag("Player"))
        {
            // Here you can add logic to deal damage to the player, etc.
            Debug.Log("Zombie attacked the player!");
        }
        else if (other.CompareTag("Weapon"))
        {
            // Here you can add logic to damage the tower, etc.
            Debug.Log("Zombie attacked the weapon!");
        }
    }
}
