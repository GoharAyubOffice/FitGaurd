using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 4;
    public float spawnDistance = 50.0f;
    public GameObject player;

    public Transform[] spawnPoints;
    public float spawnOffset = 2.0f; // Define your spawn offset

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // Spawn enemies at the defined spawn points
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, 1, spawnPoint.position.z); // Set y position to 1
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnPoint.rotation);
        }
    }
}