using UnityEngine;

public class State_Chase : State
{
    public State_Idle _Idle;
    public State_Attack _Attack;

    private EnemyLogic enemy;


    private void Awake()
    {
        enemy = GetComponentInParent<EnemyLogic>();
    }

    public override State RunCurrentState()
    {
        enemy.ChaseLogic();

        if(enemy.isInAttackRange)
        {
            return _Attack;
        }
        
        /*if (!enemy.canSeePlayer) //POSSIBLEMENT S'HAGI DE BORRAR! SINO VULL QUE TORNI A IDLE
        {
            return _Idle;
        }*/

        return this;
        
    }
}
