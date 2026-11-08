using UnityEngine;

public class PLAYER_TEST_SHOOT : MonoBehaviour
{


    [SerializeField] private int damage = 25;
    [SerializeField] private float attackRange = 20f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            Enemy_Health enemyHealth = hit.collider.GetComponentInParent<Enemy_Health>();

            if(enemyHealth != null)
            {
                enemyHealth.EnemyDamaged(damage);

                Debug.Log("Impacto en enemigo");
            }
        }
    }

}
