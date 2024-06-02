using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public float spawnRate = 2.0f;
    public float spawnAreaWidth = 10.0f;
    public float spawnHeight = 5.0f;

    void Start()
    {
        InvokeRepeating("SpawnEnemyM", 1.5f, spawnRate);
        InvokeRepeating("SpawnEnemyS", 2.5f, spawnRate);
    }

    void SpawnEnemyM()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            spawnHeight,
            0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnEnemyS()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            spawnHeight,
            0);
        Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
    }
}
