using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Chooses enemy to spawn / Instantiates enemies / Choose spawn pos
/// </summary>

[System.Serializable]
public class EnemySpawnData
{
    public EnemyLogic enemyPrefab;
    [Range(0f, 100f)] public float weight = 1f;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Tooltip("Contenedor en la jerarquía para mantener ordenados los clones.")]
    [SerializeField] private Transform bin;

    [Header("Enemies")]
    [SerializeField] private List<EnemySpawnData> enemies;


    #region Spawn Mode
    public IEnumerator SpawnWave(int amount,float delay,List<EnemyLogic> spawnedEnemies)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnRandomEnemy(spawnedEnemies);

            yield return new WaitForSeconds(delay);
        }
    }
    
    public IEnumerator SpawnOverTime(int totalEnemies,float spawnInterval,List<EnemyLogic> spawnedEnemies)
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnRandomEnemy(spawnedEnemies);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    public void SpawnInfinite(List<EnemyLogic> spawnedEnemies)
    {
        SpawnRandomEnemy(spawnedEnemies);
    }
    #endregion Spawn Mode


    #region Settings
    private void SpawnRandomEnemy(List<EnemyLogic> spawnedEnemies)
    {
        EnemyLogic enemyPrefab = GetRandomEnemy();

        if (enemyPrefab == null)
            return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        EnemyLogic enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        enemy.transform.SetParent(bin);
        spawnedEnemies.Add(enemy); //Adds enemy to list so it can be counted on events
    }

    private EnemyLogic GetRandomEnemy()
    {
        float totalWeight = 0;

        foreach (EnemySpawnData enemy in enemies)
        {
            totalWeight += enemy.weight;
        }

        float randomWeight = Random.Range(0, totalWeight);
        float currentWeight = 0;

        foreach (EnemySpawnData enemy in enemies)
        {
            currentWeight += enemy.weight;

            if (randomWeight <= currentWeight)
                return enemy.enemyPrefab;
        }

        return enemies[^1].enemyPrefab;
    }
    #endregion Settings
}


