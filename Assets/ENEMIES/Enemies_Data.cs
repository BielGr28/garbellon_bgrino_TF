using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    Tank,
    Dron,
    Boss
}

public class Enemy_Data : MonoBehaviour
{
    public static readonly Dictionary<EnemyType, EnemyStatsData> Database = new()
    {
        {
            EnemyType.Basic,
            new EnemyStatsData { maxHealth = 100, damage = 10, speed = 3f}
        },

        {
            EnemyType.Tank,
            new EnemyStatsData { maxHealth = 200, damage = 15, speed = 2f }
        },

        {
            EnemyType.Dron,
            new EnemyStatsData { maxHealth = 75, damage = 8, speed = 6f }
        },
    };
}

public class EnemyStatsData
{
    public int maxHealth;
    public int damage;
    public float speed;
}