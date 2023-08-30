using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int maxEnemies = 50;
    [SerializeField]
    private float spawnDelay = 2f;

    private EnemyPool enemiesPool;
    private bool spawnActive;

    void Start()
    {
        enemiesPool = GetComponent<EnemyPool>();
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (enemiesPool.enemyPool.CountActive < maxEnemies && !spawnActive)
            StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        spawnActive = true;
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        
        while (enemiesPool.enemyPool.CountActive < maxEnemies)
        {
            enemiesPool.enemyPool.Get();
            yield return wait;
        }
        spawnActive = false;
    }
}
