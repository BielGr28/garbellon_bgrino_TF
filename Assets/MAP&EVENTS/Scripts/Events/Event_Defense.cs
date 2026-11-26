using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Defense : GameEvent
{   
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private List<WaveData> waves;
    
    private List<EnemyLogic> spawnedEnemies = new();


    public override void StartEvent()
    {
        base.StartEvent();

        StartCoroutine(DefenseRoutine());
    }

    private IEnumerator DefenseRoutine()
    {
        foreach (WaveData wave in waves)
        {
            yield return StartCoroutine(spawner.SpawnWave(wave.enemyCount,wave.spawnDelay,spawnedEnemies));

            yield return new WaitUntil(() =>
            {
                spawnedEnemies.RemoveAll(e => e == null);
                return spawnedEnemies.Count == 0;
            });

            yield return new WaitForSeconds(wave.delayBeforeNextWave);
        }
        CompleteEvent();
    }
}
