using UnityEngine;

public class BasicEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawnerBin;
    [SerializeField] private GameObject dronPrefab;
    [SerializeField] private float spawnCooldown;
    private float maxSpawnCooldown;


    void Awake()
    {
        maxSpawnCooldown = spawnCooldown;
    }
    void Update()
    {
        if(enemySpawnerBin.transform.childCount == 0)
        {
            spawnCooldown -= Time.deltaTime;
            if(spawnCooldown <= 0)
            {
                Spawner();
            }
        }
    }

    void Spawner()
    {
        GameObject prefab = Instantiate(dronPrefab, transform.position, Quaternion.identity);
        prefab.transform.SetParent(enemySpawnerBin.transform);
        spawnCooldown = maxSpawnCooldown;
    }
}

