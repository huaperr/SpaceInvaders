using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2.0f;
    public float spawnAreaWidth = 10.0f;
    public float spawnHeight = 5.0f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2.0f, spawnRate);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            spawnHeight,
            0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
