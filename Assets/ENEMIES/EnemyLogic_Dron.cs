using UnityEngine;

public class EnemyLogic_Dron : EnemyLogic
{
    public enum DroneChargeState
    { Warning, Charging, Stunned }

    [Header ("Dron Attack Setting")]
    [SerializeField]private DroneChargeState chargeState;
    private float stateTimer;
    private Vector3 chargeTargetPosition;
    [SerializeField] private float warningDuration = 3f; //based on animation
    [SerializeField] private float stunDuration = 2f; //based on animation 


#region States Logic
    public override void IdleLogic() //What does while waiting
    {
        agent.ResetPath(); //Delete if chase its continuous

        //Animation
        //animator.SetBool("Move", true);
    }

    public override void ChaseLogic() //What does when enemy detected
    {   
        switch(chargeState)
        {
            case DroneChargeState.Warning:
                WarningState();
                break;

            case DroneChargeState.Charging:
                ChargeState();
                break;

            case DroneChargeState.Stunned:
                StunState();
                break;
        }
    }

    public override void AttackLogic() //What does to damage player
    {
        //Explode();

        Debug.Log("BOOM");
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange); 
        foreach(Collider hit in hits)
        {
            Debug.Log("PLAYER HITTED!");
            ///PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

            ///if(playerHealth != null)
            ///{
                ///playerHealth.TakeDamage(stats.damage);
            ///}
        }
        //vfx explosion?¿?¿
        Destroy(gameObject, 1f);
    }
#endregion States Logic


#region ChaseLogic
    private void WarningState()
    {
        agent.ResetPath();
        FacePlayer();

        stateTimer -= Time.deltaTime;
        if(stateTimer <= 0f)
        {
            chargeTargetPosition = player.position; //gets player pos
            //agent.speed = stats.speed; 

            agent.SetDestination(chargeTargetPosition);

            //chargeState = DroneChargeState.Charging; 
            ChangeChargeState(DroneChargeState.Charging);
        }
    }

    private void ChargeState()
    {
        //FacePlayer();

        float distance = Vector3.Distance(transform.position, chargeTargetPosition);

        if(distance <= 2f) //When enemy reaches target pos
        {
            Debug.Log("A");
            if(isInAttackRange) //Player is in attack range
            {
                Debug.Log("B");
                AttackLogic();
            }
            else
            {
                Debug.Log("C");
                //chargeState = DroneChargeState.Stunned;
                ChangeChargeState(DroneChargeState.Stunned);
                //stateTimer = stunDuration;
            }
        }
    }

    private void StunState()
    {
        agent.ResetPath();

        stateTimer -= Time.deltaTime;
        if(stateTimer <= 0f)
        {
            //chargeState = DroneChargeState.Warning;
            ChangeChargeState(DroneChargeState.Warning);
            //stateTimer = warningDuration;
        }
    }

    private void ChangeChargeState(DroneChargeState newState)
    {
        chargeState = newState;

        switch(chargeState)
        {
            case DroneChargeState.Warning:
                stateTimer = warningDuration;
                //animator.SetTrigger("Warning"); //vibration when detecting player
                break;

            case DroneChargeState.Charging:
                //animator.SetTrigger("Charge");
                break;

            case DroneChargeState.Stunned:
                stateTimer = stunDuration;
                //animator.SetTrigger("Stun");
                break;
        }
    }
#endregion ChaseLogic


#region AttackLogic
    /*private void Explode()
    {
        Debug.Log("BOOM");

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange); 

        foreach(Collider hit in hits)
        {
            ///PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

            ///if(playerHealth != null)
            ///{
                ///playerHealth.TakeDamage(stats.damage);
            ///}
        }

        Destroy(gameObject, 1f);
    }*/

    private void OnDrawGizmos()
    {
        if(chargeState == DroneChargeState.Charging)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
#endregion AttackLogic
}
