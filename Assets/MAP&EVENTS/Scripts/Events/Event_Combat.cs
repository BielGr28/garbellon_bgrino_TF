using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages waves / And combat flow
/// </summary>

public class Event_Combat : GameEvent
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private int totalEnemies = 20;
    [SerializeField] private float spawnInterval = 2f;

    private List<EnemyLogic> spawnedEnemies = new();


    public override void StartEvent()
    {
        base.StartEvent();
        
        StartCoroutine(CombatRoutine());
    }

    private IEnumerator CombatRoutine()
    {
        yield return StartCoroutine(spawner.SpawnOverTime(totalEnemies,spawnInterval,spawnedEnemies));

        yield return new WaitUntil( ()=>
        {
            spawnedEnemies.RemoveAll(enemy => enemy == null);
            return spawnedEnemies.Count == 0;
        });        
        CompleteEvent();
    }
}