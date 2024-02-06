using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    int maxSpawnCount = 3;
    [SerializeField]
    float spawnRadius = 3f;
    public GameObject spawnPrefab;
    void Update()
    {
        int existingSpawnCount = EnemiesInRadius();
        if (existingSpawnCount < maxSpawnCount)
        {
            Vector2 rndSpawnPos = new Vector2((transform.position.x + Random.Range(-4, 5)), (transform.position.y + Random.Range(-4, 5)));
            Instantiate(spawnPrefab, rndSpawnPos, Quaternion.identity);
        }
    }
    int EnemiesInRadius() 
    {
        GameObject[] spawnPrefabs = GameObject.FindGameObjectsWithTag(spawnPrefab.tag);
        int count = 0;
        foreach (var prefab in spawnPrefabs)
        {
            if (Vector2.Distance(transform.position, prefab.transform.position) <= spawnRadius)
            {
                count++;
            }
        }
        return count;
    }
}
