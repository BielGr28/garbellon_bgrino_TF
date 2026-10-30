using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public ParticleSystem bodyExplosionVFX; //TEMP
    private Enemy_Stats stats;
    private Animator animator;

    void Awake()
    {
        stats = GetComponent<Enemy_Stats>();
        animator = GetComponent<Animator>();
    }


    public void EnemyDamaged(int damage)
    {
        if (stats.currentHealth > 0) //DAMAGED//
        {
            stats.currentHealth -= damage;
            //animator.SetTrigger("Damaged"); 
            Debug.Log($"{gameObject.name} recibió {damage} de daño");
        }
        else if (stats.currentHealth <= 0) //DEAD//
        {
            Die();
            Invoke(nameof(Loot), 0.5f); //based on destroy time
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Dead");
        //animator.SetTrigger("Dead");

        ParticleSystem particle = Instantiate(bodyExplosionVFX, transform.position, Quaternion.identity);
        particle.name = "im gona explode!"; particle.Play();

        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        Destroy(particle.gameObject, 1f);
        Destroy(gameObject, 1f);
    }

    private void Loot()
    {
        
    }
}
