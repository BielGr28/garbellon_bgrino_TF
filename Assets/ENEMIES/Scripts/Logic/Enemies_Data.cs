using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Ranged,
    Tank,
    Dron,
}

public class Enemy_Data : MonoBehaviour
{
    public static readonly Dictionary<EnemyType, EnemyStatsData> Database = new()
    {
        {
            EnemyType.Ranged,
            new EnemyStatsData 
            { 
                maxHealth = 200, 
                damage = 10, 
                speed = 5,
                detectionRange = 12,
                attackRange = 8,
                XP = 6,
                Coins = 2
            }
        },

        {
            EnemyType.Tank,
            new EnemyStatsData 
            { 
                maxHealth = 300, 
                damage = 15, 
                speed = 3, 
                detectionRange = 12,
                attackRange = 2.5f,
                XP = 10,
                Coins = 3
            }
        },

        {
            EnemyType.Dron,
            new EnemyStatsData 
            { 
                maxHealth = 100, 
                damage = 20, 
                speed = 15,
                detectionRange = 12,
                attackRange = 4,  
                XP = 4,
                Coins = 1              
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
    public int XP;
    public int Coins;
}