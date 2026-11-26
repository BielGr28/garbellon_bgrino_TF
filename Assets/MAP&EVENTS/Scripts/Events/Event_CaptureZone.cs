using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Event_CaptureZone : GameEvent
{
    [Header("References")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int startMaxEnemies = 6;
    [SerializeField] private int endMaxEnemies = 12;
    private List<EnemyLogic> spawnedEnemies = new();
    private Coroutine spawnRoutine;
    [Header("Capture")]
    [SerializeField] private float captureTime = 20f;
    private bool isPlayerInside;
    private float currentProgress;
    [Header("UI")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    

    public override void StartEvent()
    {
        base.StartEvent();

        currentProgress = 0;
        spawnRoutine = StartCoroutine(CaptureSpawnRoutine());

        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
    {
        progressBar.gameObject.SetActive(true); progressText.gameObject.SetActive(true);

        while (currentProgress < captureTime)
        {
            progressBar.value = currentProgress / captureTime;
            progressText.text = $"{progressBar.value * 100f:0}%";

            if (isPlayerInside)
            {
                currentProgress += Time.deltaTime;
            }
            else
            {

                currentProgress -= Time.deltaTime/4;
                currentProgress = Mathf.Clamp(currentProgress, 0, captureTime);
            }
            yield return null;
        }

        //On capture successfull > Stop Infinite Enemy Spawn
        if(spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
        //"Kill" rest of enemies
        /*yield return new WaitUntil(() => 
        {
            spawnedEnemies.RemoveAll(enemy => enemy == null);
            return spawnedEnemies.Count == 0;
        });*/
        foreach(EnemyLogic enemy in spawnedEnemies)
        {
            if(enemy != null)
            {
                enemy.GetComponent<Enemy_Health>().Die();
            }
        }
        spawnedEnemies.Clear();

        progressBar.gameObject.SetActive(false); progressText.gameObject.SetActive(false);

        CompleteEvent();
    }

    private IEnumerator CaptureSpawnRoutine()
    {
        while(!IsCompleted)
        {
            spawnedEnemies.RemoveAll(enemy => enemy == null);


            float normalizedProgress = currentProgress / captureTime;
            //Increases maxAliveEnemies by 50% based on progress
            int currentMaxEnemies = Mathf.RoundToInt(Mathf.Lerp(startMaxEnemies, endMaxEnemies, normalizedProgress));

            if(spawnedEnemies.Count < currentMaxEnemies)
            {
                spawner.SpawnInfinite(spawnedEnemies);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
