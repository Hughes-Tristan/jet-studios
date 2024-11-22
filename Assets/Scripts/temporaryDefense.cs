
// Developer(s): Tristan Hughes
// Last Updated: 11-21-24
// Temporary Defense Class

// the intended purpose of this class is to control the defenses but is only temporary.
// it also provides methods taking damage and object death 

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class temporaryDefense : MonoBehaviour
{
    public float health = 6.0f;
    public float projectileFirerate = 1f;
    public float nextShot;

    public Transform projectileSpawnpoint;
    public GameObject projectilePrefab;


    public void initializeHealth(float healthAmount)
    {
        health = healthAmount;

        if (projectileSpawnpoint == null)
        {
            projectileSpawnpoint = transform.Find("projectileSpawnpoint");
        }
    }


    public void Update()
    {
        if(nextShot <= Time.time) {
            projectileFire();
            nextShot = Time.time + 1/ projectileFirerate;
        }
    }

    // this function is used to instantiate the object once there is a prefab or a spawnpoint
    public void projectileFire()
    {
        if(projectileSpawnpoint != null && projectilePrefab != null)
        {
            Instantiate(projectilePrefab, projectileSpawnpoint.position, projectileSpawnpoint.rotation);
        }
    }

    // this function destroys the game object to simulate a death
    void onCharDeath()
    {
        Destroy(gameObject);
    }

    // this function subtracts the damage amount from the health
    // if the health is less than or equal to 0 then destroy the object
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            onCharDeath();
        }
    }
}
