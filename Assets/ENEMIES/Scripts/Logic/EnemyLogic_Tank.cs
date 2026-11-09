using System;
using UnityEngine;

public class EnemyLogic_Tank : EnemyLogic
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;
    [SerializeField] private Collider attackCollider;
    private bool canDealDamage;


    protected override void Awake()
    {
        base.Awake();

        canDealDamage = false;
        attackCollider.enabled = false;
    }

#region States
    public override void IdleLogic()
    {
        agent.ResetPath();
    }
    public override void ChaseLogic()
    {
        FacePlayer();

        agent.SetDestination(player.position);
    }
    public override void AttackLogic()
    {
        FacePlayer();
        agent.ResetPath();

        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            HammerAttack();
            cooldownTimer = attackCooldown;
        }
    }

    public override void OnEnterChase()
    {
        cooldownTimer = 0f;
        //agent.updateRotation = false;
        agent.speed = stats.speed; //sets agent speed to current enemy speed
    }
    public override bool CanTransitToAttack()
    {
        return base.CanTransitToAttack();
    }
#endregion States

#region AttackLogic
    private void HammerAttack()
    {
        Debug.Log("Hammer time!");
        EnableAttackCollider();

        //Animation
        //animator.SetTrigger("Attack");
    }

    public void OnAttackHit(Collider other)
    {
        if(canDealDamage && other.CompareTag("Player"))
        {
            Debug.Log("Player Hitted!");
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //playerHealth?.TakeDamage(owner.Damage);
        }

        DisableAttackCollider();
    }

    public void EnableAttackCollider()
    {
        canDealDamage = true;
        attackCollider.enabled = true;
    }
    public void DisableAttackCollider()
    {
        canDealDamage = false;
        attackCollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, stats.attackRange); //display attack range
        }
    }

#endregion AttackLogic
}
