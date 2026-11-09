using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyLogic : MonoBehaviour
{
    [Header("References")]
    protected Transform player;
    protected CapsuleCollider playerCollider;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Enemy_Stats stats;
    protected Animator animator;
    public AudioSource audioSource;
    public GameObject mesh;
    [Header ("Detection")]
    public float viewAngle = 120f;
    public LayerMask obstacleLayer;
    [Header("Visuals")]
    public ParticleSystem[] effects;
    
    public float distanceToPlayer { get; private set; }
    public bool canSeePlayer  { get; private set; }
    public bool isInAttackRange => distanceToPlayer <= stats.attackRange;    


    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerCollider = player.GetComponentInChildren<CapsuleCollider>();
        
        stats = GetComponent<Enemy_Stats>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
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
        if(distanceToPlayer > stats.detectionRange)
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
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stats.detectionRange);
    
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        }
    }

    protected void FaceDirection(Vector3 direction) //Enemy look towards any target type
    {
        direction.y = 0;

        if(direction == Vector3.zero)
        return;
        
        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5f * Time.deltaTime);
    }

    public void FacePlayer() //Enemy looks towards player
    {
        FaceDirection(player.position - transform.position);
    }
#endregion Detection


#region States Logics
    public abstract void IdleLogic();
    public abstract void ChaseLogic();
    public abstract void AttackLogic();

    public virtual void OnEnterChase(){} //Set basic stats for chase as charges left, state, animation, etc
    public virtual bool CanTransitToAttack() { return isInAttackRange; }
#endregion State Logics
}