using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private GameObject owner;

    void Awake()
    {
        owner = transform.parent.gameObject;
    }

    public void AndroidAttackEvent() //Called from Android shoot animation event
    {
        if(owner != null)
        {
            owner.GetComponent<EnemyLogic_Android>().Shoot();
        }
    }

    public void AndroidAttackFinished()
    {
        owner.GetComponent<EnemyLogic_Android>().AttackFinished();
    }
}
