using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;  // Reference to the zombie prefab
    public float spawnInterval = 10f; // Time between zombie spawns
    public float spawnRangeX = 20f;  // Range for random spawn position along the X-axis

    private void Start()
    {
        // Start spawning zombies at regular intervals
        InvokeRepeating("SpawnZombie", 0f, spawnInterval);
    }

    void SpawnZombie()
    {
        // Randomly position the zombie along the X-axis at the top of the screen
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(spawnPosX, 2f, 0f); 

        // Instantiate the zombie prefab at the spawn position
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
