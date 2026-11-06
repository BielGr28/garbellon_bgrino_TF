using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    [Header("Type")]
    public EnemyType currentType;

    [Header("Runtime Stats")]
    public int maxHealth;
    public int currentHealth;
    public int damage;
    public float speed;
    public float detectionRange;
    public float attackRange;


    private void Awake()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        EnemyStatsData data = Enemy_Data.Database[currentType];

        maxHealth = data.maxHealth;
        currentHealth = maxHealth;
        damage = data.damage;
        speed = data.speed;
        detectionRange = data.detectionRange;
        attackRange = data.attackRange;
    }
}