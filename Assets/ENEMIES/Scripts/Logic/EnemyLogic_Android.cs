using UnityEngine;

public class EnemyLogic_Android : EnemyLogic
{
    [Header("References")]
    private Transform bin;
    [Header("Attack")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private float aimPredictionFactor;
    private bool isAttacking = false;
    [Header("Materials")]
    private MeshRenderer[] androidRender;


    protected override void Awake()
    {
        base.Awake();

        bin = GameObject.Find("bin").transform;

        //Set instances for modifiying local mat
        androidRender = GetComponentsInChildren<MeshRenderer>();
    }
    
#region States Logic
    public override void IdleLogic()
    {
        agent.ResetPath();

        foreach(MeshRenderer rend in androidRender)
        {
            foreach(Material mat in rend.materials)
            {
                if(mat.name.Contains(""))
                {
                    mat.SetColor("_EmissionColor", Color.black);
                }
            }
        }
    }
    public override void ChaseLogic()
    {
        if(isAttacking) //Prevents android to walk while shooting
        return;

        FacePlayer();
        agent.SetDestination(player.position);

        //Anim
        animator.SetBool("isWalking",true);
    }
    public override void AttackLogic()
    {
        agent.ResetPath();
        FacePlayer();

        animator.SetBool("isWalking", false);
        
        if(!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("isAttacking");
        }
    }
    public override void OnEnterChase()
    {
        agent.updateRotation = false;
        agent.speed = stats.speed; //sets agent speed to current enemy speed

        animator.SetBool("isDetected", true);

        foreach(MeshRenderer rend in androidRender)
        {
            foreach(Material mat in rend.materials)
            {
                if(mat.name.Contains("reactor"))
                {
                    mat.SetColor("_EmissionColor", Color.yellow);
                }
            }
        }
    }
    public override bool CanTransitToAttack() { return base.CanTransitToAttack(); }
#endregion States Logic


#region Attack 
    public void Shoot() //Called through animation event
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Vector3 predictedPos = Marksmanship();
        Vector3 shootDirection = (predictedPos - firePoint.position).normalized;

        projectile.transform.rotation = Quaternion.LookRotation(shootDirection);
        projectile.GetComponent<ProjectileLogic>().InitializeValues(stats.damage, shootDirection);
        projectile.transform.SetParent(bin);
    }
    private Vector3 Marksmanship()
    {
        Vector3 playerPos = playerCollider.bounds.center;

        PlayerController playerC = player.GetComponent<PlayerController>();

        float random = Random.Range(0.5f, 3.5f);
        aimPredictionFactor = random;

        return playerPos + playerC.CurrentMoveDirection * aimPredictionFactor;
        
    }
    
    public void AttackFinished()
    {
        isAttacking = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position,firePoint.forward * 10f);
    }
#endregion Attack 

}