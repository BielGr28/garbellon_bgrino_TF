using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    private int projectileDamage;
    [SerializeField] private float speed;
    private Vector3 targetPosition;
    

    public void InitializeValues(int damage, Vector3 position)
    {
        projectileDamage = damage;
        targetPosition = position.normalized;
    }

    private void Update()
    {
        ProjectileMovement();
    }

    void ProjectileMovement()
    {
        transform.position += speed * Time.deltaTime * targetPosition;
    }

    private void OnTriggerEnter(Collider other) //COLISIONS
    {
      
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Hitted!");

            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //playerHealth?.TakeDamage(projectileDamage);
            
            Destroy(gameObject);
        }
    
    }
}
