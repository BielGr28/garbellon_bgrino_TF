using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class EnemyLogic_Dron : EnemyLogic
{
    public enum DroneChargeState
    { Warning, Charging, Stunned }
    private DroneChargeState chargeState;

    [Header ("Dron Settings")]
    [SerializeField] private int maxCharges = 3; //Charges needed for dron to self-destruct
    [SerializeField] private int currentCharges; 
    private Vector3 chargeTargetPosition, chargeDirection;
    private float stateTimer = 3f; //based on warningDuration
    [SerializeField] private float warningDuration = 3f; //based on animation
    [SerializeField] private float stunDuration = 2f; //based on animation 
    [SerializeField] private float hoverOffset; //dron flying offset relative to player
    [Header("Materials")]
    [SerializeField] private MeshRenderer dronRender;
    private Material eyeMat, reactorMat;
    [Header("Audio")]
    [SerializeField] private AudioClip audioExplosion;
    [SerializeField] private AudioClip audioCharge;



    protected override void Awake()
    {
        base.Awake();

        //Set instances for modifiying local mat
        Material[] mats = dronRender.materials;
        eyeMat = mats[4];
        reactorMat = mats[5];
    }

    #region States Logic
    public override void IdleLogic() //What does while waiting
    {
        //Makes effect of dron disabled
        eyeMat.SetColor("_EmissionColor", Color.black);
        reactorMat.SetColor("_EmissionColor", Color.black);
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
        eyeMat.SetColor("_EmissionColor", Color.red);
        reactorMat.SetColor("_EmissionColor", Color.red);
        ChangeChargeState(DroneChargeState.Warning);
    }
    
    public override bool CanTransitToAttack() { return false; }
#endregion States Logic


#region ChaseLogic
    private void ChangeChargeState(DroneChargeState newState)
    {
        chargeState = newState;

        switch(chargeState)
        {
            case DroneChargeState.Warning:
                stateTimer = warningDuration; 
                animator.SetTrigger("Warning");
                audioSource.PlayOneShot(audioCharge);
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

    private void WarningState()
    {
        HoverAbovePlayer();
        FacePlayer();

        stateTimer -= Time.deltaTime;
        if(stateTimer <= 0f) //WARNING TIME
        {
            Vector3 playerCenter = playerCollider.bounds.center;
            
            chargeTargetPosition = playerCenter; // Gets player position
            chargeDirection = (chargeTargetPosition - transform.position).normalized; // Calculates charge target position on exact charge start 

            ChangeChargeState(DroneChargeState.Charging);
        }
    }

    private void ChargingState()
    {        
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
    public void Explode()
    {
        Debug.Log("BOOM");

        Collider[] hits = Physics.OverlapSphere(transform.position, stats.attackRange); 
        foreach(Collider hit in hits)
        {
            //Debug.Log("Player Hitted!");
            //PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

            //if(playerHealth != null)
            //{
                //playerHealth.TakeDamage(stats.damage);
            //}
        }

        //VFX
        ParticleSystem particle = Instantiate(effects[0], transform.position, Quaternion.identity);
        particle.Play(); particle.transform.localScale *= 1.3f;
        particle.transform.SetParent(GameObject.Find("bin").transform);

        //SFX
        audioSource.PlayOneShot(audioExplosion);

        //Destroy
        mesh.SetActive(false);
        foreach (var component in gameObject.GetComponentsInChildren<Behaviour>())
        {
            if(component != gameObject.GetComponentInChildren<AudioSource>())
            {
                component.enabled = false;
            }
        }

        Destroy(gameObject, audioExplosion.length);
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
