using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class EnemyLogic_Dron : EnemyLogic
{
    public enum DroneChargeState
    { Warning, Charging, Stunned }
    [SerializeField]private DroneChargeState chargeState;

    [Header ("Dron Settings")]
    [SerializeField] private int maxCharges = 3; //Charges needed for dron to self-destruct
    [SerializeField] private int currentCharges; 
    private Vector3 chargeTargetPosition;
    private Vector3 chargeDirection;
    private float stateTimer = 3f; //based on warningDuration
    [SerializeField] private float warningDuration = 3f; //based on animation
    [SerializeField] private float stunDuration = 2f; //based on animation 
    [SerializeField] private float hoverOffset; //dron flying offset relative to player
    

    #region States Logic
    public override void IdleLogic() //What does while waiting
    {
        animator.SetBool("isIdle", true);
        //cambiar material effecte desactivat 
    }

    public override void ChaseLogic() //What does when enemy detected
    {   
        switch(chargeState)
        {
            case DroneChargeState.Warning:
                WarningState();
                break;

            case DroneChargeState.Charging:
                ChargingState();
                break;

            case DroneChargeState.Stunned:
                StunState();
                break;
        }
    }

    public override void AttackLogic() //What does to damage player
    {
        Explode();
    }

    public override void OnEnterChase()
    {
        currentCharges = 0;
        ChangeChargeState(DroneChargeState.Warning);
    }
    public override bool CanTransitToAttack() { return false; }
#endregion States Logic


#region ChaseLogic
    private void WarningState()
    {
        HoverAbovePlayer();
        FacePlayer();

        stateTimer -= Time.deltaTime;
        if(stateTimer <= 0f)
        {
            Vector3 playerCenter = playerCollider.bounds.center;
            
            chargeTargetPosition = playerCenter; // Gets player position

            chargeDirection = (chargeTargetPosition - transform.position).normalized; // Calculates charge target position on exact charge start 

            ChangeChargeState(DroneChargeState.Charging);
        }
    }

    private void ChargingState()
    {        
        //transform.position += stats.speed * Time.deltaTime * chargeDirection; //Moves to target position
        transform.position = Vector3.MoveTowards(transform.position, chargeTargetPosition, stats.speed * Time.deltaTime);

        FaceDirection(chargeDirection); //looks towards target

        float distanceToTarget = Vector3.Distance(transform.position, chargeTargetPosition);
        if(distanceToTarget <= 0.25f) //When target reached > attacks or gets stunned
        {
            if(isInAttackRange)
            {
                AttackLogic();
            }
            else 
            {
                currentCharges++;
                ChangeChargeState(DroneChargeState.Stunned);
            }
        }
    }

    private void StunState()
    {
        if(currentCharges >= maxCharges)
        {
            Explode();
        }
        else 
        {
            HoverAbovePlayer();
            stateTimer -= Time.deltaTime;
            if(stateTimer <= 0f)
            {
                ChangeChargeState(DroneChargeState.Warning);
            }
        }
    }

    private void ChangeChargeState(DroneChargeState newState)
    {
        chargeState = newState;

        switch(chargeState)
        {
            case DroneChargeState.Warning:
                stateTimer = warningDuration;
                //animator.SetBool("isIdle", false);
                animator.SetTrigger("Warning");
                // [ADD] ¡audio source to alarm player of enemy charge!
                break;

            case DroneChargeState.Charging:
                animator.SetTrigger("Charging");
                break;

            case DroneChargeState.Stunned:
                stateTimer = stunDuration;
                animator.SetTrigger("Recovering");
                break;
        }
    }

    private void HoverAbovePlayer() //Moves dron back to flying position
    {
        Vector3 targetPos = playerCollider.bounds.center + Vector3.up * hoverOffset;

        Vector3 pos = transform.position;

        pos.x = Mathf.MoveTowards(pos.x, targetPos.x, stats.speed/9 * Time.deltaTime); //speed chasing player
        pos.z = Mathf.MoveTowards(pos.z, targetPos.z, stats.speed/9 * Time.deltaTime); 
        pos.y = Mathf.MoveTowards(pos.y, targetPos.y, stats.speed/5 * Time.deltaTime); //speed recovering height

        transform.position = pos;
    }
#endregion ChaseLogic


#region AttackLogic
    private void Explode()
    {
        Debug.Log("BOOM");

        Collider[] hits = Physics.OverlapSphere(transform.position, stats.attackRange); 
        foreach(Collider hit in hits)
        {
            Debug.Log("PLAYER HITTED!");
            //PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

            //if(playerHealth != null)
            //{
                //playerHealth.TakeDamage(stats.damage);
            //}
        }

        //Instantiates explosion, plays it addapting scalen when dron attacks 
        ParticleSystem particle = Instantiate(effects[0], transform.position, Quaternion.identity);
        particle.Play(); particle.transform.localScale *= 1.4f;
        particle.name = "exploding!"; particle.transform.SetParent(GameObject.Find("bin").transform);

        Destroy(gameObject);
        Destroy(particle.gameObject, 2f);
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, stats.attackRange); //display "bomb" radious

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(chargeTargetPosition, 0.25f); //display target position
        }
    }
    #endregion AttackLogic
}
