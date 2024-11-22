
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
    public GameObject zombiePrefab;  // Reference to the zombie prefab
    public float spawnInterval = 10f; // Time between zombie spawns
    //public float spawnRangeX = 20f;  // Range for random spawn position along the X-axis
    public Transform[] spawnPoints;

    private void Start()
    {
        // Start spawning zombies at regular intervals
        InvokeRepeating("SpawnZombie", 0f, spawnInterval);
    }

    void SpawnZombie()
    {
        int maxLength;
        maxLength = spawnPoints.Length;

        // Randomly position the zombie along the X-axis at the top of the screen
        int spawnPosX = Random.Range(0, maxLength);
        Vector3 spawnPosition = spawnPoints[spawnPosX].position; 

        // Instantiate the zombie prefab at the spawn position
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
