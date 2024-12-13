using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is used to represent boss movements for the second boss in the game
public class bossMovement : ZombieMovement
{
    public GameObject miniBoss;
    public Transform spawnPoint;
    public float bombDamage = 30.0f;
    public float bombSize = 3.0f;
    //public GameObject projectile;
    //public Transform projectileSpawn;

    public float spawnInterval = 7f;
    private float spawnTime = 0.0f;
//public float projectileInterval = 5.0f;
    //private float projectileTime = 0.0f;

    // this function overrides start from the zombimovement parent class and sets isGiant to true
    public override void Start()
    {
        base.Start();
        isGiant = true;
    }

    // this function overrides the onchardeath function from the parent function and starts the explode function
    public override void onCharDeath()
    {
        explode();
        base.onCharDeath();
    }

    public override void Update()
    {
        if (!attacking)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        }
        spawnTime += Time.deltaTime;

        if (spawnTime > spawnInterval) { 
            spawnHelp();
            spawnTime = 0.0f;
        }
    }
    
    private void spawnHelp()
    {
        if(miniBoss != null && spawnPoint != null) {
            Instantiate(miniBoss, spawnPoint.position, Quaternion.identity);
        }
    }

    private void explode()
    {
        Collider2D[] storedHits = Physics2D.OverlapCircleAll(transform.position, bombSize);
        foreach (Collider2D hit in storedHits)
        {
            if (hit.CompareTag("defense"))
            {

                axe_knight_attack axeKnightComponent = hit.GetComponent<axe_knight_attack>();
                temporaryDefense defense = hit.GetComponent<temporaryDefense>();

                if (defense != null)
                {
                    defense.takeDamage(bombDamage);

                }
                else if (axeKnightComponent != null)
                {
                    axeKnightComponent.TakeDamage(bombDamage);
                }
            }
        }
    }

}
