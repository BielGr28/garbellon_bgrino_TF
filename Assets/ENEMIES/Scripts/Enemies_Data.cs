using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    Tank,
    Dron,
}

public class Enemy_Data : MonoBehaviour
{
    public static readonly Dictionary<EnemyType, EnemyStatsData> Database = new()
    {
        {
            EnemyType.Basic,
            new EnemyStatsData 
            { 
                maxHealth = 200, 
                damage = 10, 
                speed = 3f,
                detectionRange = 0,
                attackRange = 0
            }
        },

        {
            EnemyType.Tank,
            new EnemyStatsData 
            { 
                maxHealth = 300, 
                damage = 15, 
                speed = 2f, 
                detectionRange = 0,
                attackRange = 0
            }
        },

        {
            EnemyType.Dron,
            new EnemyStatsData 
            { 
                maxHealth = 100, 
                damage = 20, 
                speed = 15f,
                detectionRange = 12,
                attackRange = 4                 
            }
        },
    };
}

public class EnemyStatsData
{
    public int maxHealth;
    public int damage;
    public float speed;
    public float detectionRange;
    public float attackRange;
}