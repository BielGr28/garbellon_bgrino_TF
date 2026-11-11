using System;
using UnityEngine;

public class EnemyLogic_Tank : EnemyLogic
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;
    [SerializeField] private Collider attackCollider;
    private bool canDealDamage;
    [Header("Materials")]
    [SerializeField] private SkinnedMeshRenderer tancRender;
    private Material reactorMat,energyMat;


    protected override void Awake()
    {
        base.Awake();

        canDealDamage = false;
        attackCollider.enabled = false;

        //Set instances for modifiying local mat
        Material[] mats = tancRender.materials;
        reactorMat = mats[1];
        energyMat = mats[2];
    }

#region States
    public override void IdleLogic()
    {
        agent.ResetPath();

        //Makes effect of dron disabled
        energyMat.SetColor("_EmissionColor", Color.black);
        reactorMat.SetColor("_EmissionColor", Color.black);
    }
    public override void ChaseLogic()
    {
        FacePlayer();
        agent.SetDestination(player.position);

        //Anim
        animator.SetTrigger("isWalking");
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
        agent.updateRotation = false;
        agent.speed = stats.speed; //sets agent speed to current enemy speed
        energyMat.SetColor("_EmissionColor", Color.yellow);
        reactorMat.SetColor("_EmissionColor", Color.green);
    }
    public override bool CanTransitToAttack() { return base.CanTransitToAttack(); }
#endregion States

#region AttackLogic
    private void HammerAttack()
    {
        Debug.Log("Hammer time!");
        EnableAttackCollider();

        //Animation
        animator.SetTrigger("isAttacking");
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