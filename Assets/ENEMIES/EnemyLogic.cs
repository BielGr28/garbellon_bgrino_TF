using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyLogic : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    protected Enemy_Stats stats;
    protected Animator animator;

    [Header("Ranges")]
    public float detectionRange = 10f;
    public float attackRange = 2f;

    [Header ("Detection")]
    public float viewAngle = 120f;
    public LayerMask obstacleLayer;

    public float distanceToPlayer { get; private set; }
    public bool canSeePlayer  { get; private set; }
    public bool isInAttackRange => distanceToPlayer <= attackRange;    


    private void Awake()
    {
        stats = GetComponent<Enemy_Stats>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.speed = stats.speed; //sets agent speed to current enemy speed
    }
    private void Update()
    {
        UpdateVision();
    }


#region Detection
    private void UpdateVision()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position); //Distance between Enemy & Player

        canSeePlayer = false;

        //Out vision range
        if(distanceToPlayer > detectionRange)
        return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        //Check FOV
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if(angle > viewAngle * 0.5f)
        return;

        //Check Obstacles
        Vector3 eyePos = transform.position + Vector3.up;

        if(!Physics.Raycast(eyePos, directionToPlayer, distanceToPlayer, obstacleLayer))
        {
            canSeePlayer = true;
        }
    }

    private void OnDrawGizmosSelected() //Visual assist
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.y = 0;

        if(direction == Vector3.zero)
        return;
        
        Quaternion targetRot = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5f * Time.deltaTime);
    }
#endregion Detection


#region States Logics
    public abstract void IdleLogic();
    public abstract void ChaseLogic();
    public abstract void AttackLogic();
#endregion State Logics
}