using UnityEngine;

[RequireComponent(typeof(EnemyLogic))]
public class Enemy_Health : MonoBehaviour
{
    private Enemy_Stats stats;
    private EnemyLogic enemy;

    void Awake()
    {
        stats = GetComponent<Enemy_Stats>();
        enemy = GetComponent<EnemyLogic>();
    }

    void Update()
    {
        Die();
    }


    public void EnemyDamaged(int damage)
    {
        if (stats.currentHealth > 0) //DAMAGED//
        {
            Debug.Log($"{gameObject.name} recibió {damage} de daño");
            stats.currentHealth -= damage;

            //VFX sparks
            ParticleSystem particle = Instantiate(enemy.effects[1], transform.position, enemy.effects[1].transform.rotation);
            particle.Play(); particle.name = "electricity!"; particle.transform.SetParent(GameObject.Find("bin").transform);
            Destroy(particle.gameObject, 2f);
        }
    }

    private void Die()
    {
        if(stats.currentHealth <= 0)
        {
            Debug.Log("Enemy Dead");

            ParticleSystem particle = Instantiate(enemy.effects[0], transform.position, Quaternion.identity);
            particle.Play(); particle.name = "im gona explode!"; particle.transform.SetParent(GameObject.Find("bin").transform);

            Destroy(gameObject);
            Destroy(particle.gameObject, 2f);
        }
    }

    private void Loot()
    {
        //*ECONOMIA* coins/gems...
        //*PLAYER EXPERIENCE* orbs...

        //object drop > pop and fall to ground
        //coin only can be reached on collide
        //xp moves to player when in short range
    }
}
