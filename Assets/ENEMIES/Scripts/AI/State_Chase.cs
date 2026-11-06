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

        if(enemy.CanTransitToAttack())
        {
            return _Attack;
        }

        return this;
    }
}
