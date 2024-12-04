
// Developer(s): Evita Kanaan, Tristan Hughes
// Last Updated: 11-21-24
// Enemy Movement Class

// the intended purpose of this class is to control enemy spawning.
// it also provides methods for spawning enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    //public static ZombieSpawner Instance { get; private set; }
    public GameObject zombiePrefab;  // Reference to the zombie prefab
    public float spawnInterval = 10f; // Time between zombie spawns
    //public float spawnRangeX = 20f;  // Range for random spawn position along the X-axis
    public Transform[] spawnPoints;
    public float spawnDelay = 0.0f;
    private AudioSource audioSourceDeath;
    private AudioSource audioSourceHit;


    private void Start()
    {
        // Start spawning zombies at regular intervals
        InvokeRepeating("SpawnZombie", spawnDelay, spawnInterval);
        AudioSource[] audiosources = GetComponents<AudioSource>();
        //audioSourceDeath = audiosources[0];
        //audioSourceHit = audiosources[1];
    }

  

    void SpawnZombie()
    {
        int maxLength;
        maxLength = spawnPoints.Length;

        
        int spawnPosX = Random.Range(0, maxLength);
        Vector3 spawnPosition = spawnPoints[spawnPosX].position; 

        // Instantiate the zombie prefab at the spawn position

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }

    public void playDeathSound()
    {
        audioSourceDeath.Play();
    }
    public void playHitSound()
    {
        audioSourceHit.Play();
    }
}
