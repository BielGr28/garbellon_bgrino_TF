using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public int AliveEnemies { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy()
    {
        AliveEnemies++;
    }

    public void UnregisterEnemy()
    {
        AliveEnemies = Mathf.Max(0, AliveEnemies - 1);
    }
}
