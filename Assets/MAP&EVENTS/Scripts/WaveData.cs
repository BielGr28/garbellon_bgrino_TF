using UnityEngine;


[System.Serializable]
public class WaveData
{
    [Header("Info")]
    public string waveId;

    [Header("Spawn")]
    public int enemyCount;
    public float spawnDelay = 0.5f;

    [Header("Flow")]
    public float delayBeforeNextWave = 2f;
}
