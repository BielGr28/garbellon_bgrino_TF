using UnityEngine;

public class State_Attack : State
{
    public State_Chase _Chase;
    private EnemyLogic enemy;


    void Awake()
    {
        enemy = GetComponentInParent<EnemyLogic>();
    }

    public override State RunCurrentState()
    {
        enemy.AttackLogic();

        if(!enemy.isInAttackRange)
        {
            return _Chase;
        }
        
        return this;
    }
}
