using UnityEngine;

public class State_Idle : State
{
    public State_Chase _Chase;
    private EnemyLogic enemy;


    private void Awake()
    {
        enemy = GetComponentInParent<EnemyLogic>();
    }

    public override State RunCurrentState()
    {
        enemy.IdleLogic();

        if(enemy.canSeePlayer)
        {
            return _Chase;
        }

        return this;
    }
}
