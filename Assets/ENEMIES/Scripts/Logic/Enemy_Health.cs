using UnityEngine;

[RequireComponent(typeof(EnemyLogic))]
public class Enemy_Health : MonoBehaviour
{
    private Enemy_Stats stats;
    private EnemyLogic enemy;

    [Header("Drops")]
    [SerializeField] private GameObject xpOrb;
    [SerializeField] private GameObject coin;

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
            Loot();
            Debug.Log("Enemy Dead");

            ParticleSystem particle = Instantiate(enemy.effects[0], transform.position, Quaternion.identity);
            particle.Play(); particle.name = "im gona explode!"; particle.transform.SetParent(GameObject.Find("bin").transform);

            Destroy(gameObject);
            Destroy(particle.gameObject, 2f);
        }
    }

    private void Loot()
    {
        //Basic Drops/Resources
        GameObject lootXP = Instantiate(xpOrb, new Vector3(transform.position.x+1, 1, transform.position.z), Quaternion.identity);
        GameObject lootCoin = Instantiate(coin, new Vector3(transform.position.x, 1, transform.position.z), coin.transform.rotation);
 
        lootXP.transform.SetParent(GameObject.Find("bin").transform);
        lootCoin.transform.SetParent(GameObject.Find("bin").transform); 

        //Rare drops (ammo, health, etc)
    }
}
