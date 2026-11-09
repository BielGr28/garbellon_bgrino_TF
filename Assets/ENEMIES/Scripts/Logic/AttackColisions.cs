using UnityEngine;

public class AttackColisions : MonoBehaviour
{
    private EnemyLogic_Tank tank;

    private void Awake()
    {
        tank = GetComponentInParent<EnemyLogic_Tank>();
    }


    private void OnTriggerEnter(Collider other)
    {
        tank.OnAttackHit(other);
    }
}
