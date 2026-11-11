using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyLogic))]
public class Enemy_Health : MonoBehaviour
{
    private Enemy_Stats stats;
    private EnemyLogic enemy;

    [Header("Drops")]
    [SerializeField] private GameObject prefabXPOrb;
    [SerializeField] private GameObject prefabCoin;
    [Header("Audio")]
    [SerializeField] private AudioClip audioExplosion;
    [Header("Animation")]
    private bool hasDied;
    private float animTimer;


    void Awake()
    {
        stats = GetComponent<Enemy_Stats>();
        enemy = GetComponent<EnemyLogic>();

        hasDied = false;
        animTimer = 0f;
    }

    void Update()
    {
        Die();
    }


#region Recieve damage
    public void EnemyDamaged(int damage, RaycastHit hit)
    {
        if (stats.currentHealth > 0) //DAMAGED
        {
            Debug.Log($"{gameObject.name} recibió {damage} de daño");
            stats.currentHealth -= damage;

            //VFX sparks
            ParticleSystem sparks = Instantiate(enemy.effects[1], hit.point, Quaternion.LookRotation(hit.normal));//transform.position, enemy.effects[1].transform.rotation);
            sparks.Play(); sparks.transform.SetParent(GameObject.Find("bin").transform);
            Destroy(sparks.gameObject, 2f);
        }
    }
#endregion Recieve damage


#region Die
    private void Die()
    {
        if(hasDied)
        return;

        if(stats.currentHealth <= 0)
        {
            hasDied = true;

            //Animation 
            if(CheckForExistingAnimation("isDead"))
            {
                enemy.animator.SetTrigger("isDead");
                animTimer = GetAnimationLength("Tanc_Attack_v3");
            }
            else { animTimer = 0f; }

            StartCoroutine(DieRoutine(animTimer));
        }
    }

    private IEnumerator DieRoutine(float timer)
    {
        //Drops
            Loot();
        
        //Disable
            foreach (var component in gameObject.GetComponentsInChildren<Behaviour>())
            {
                if(component != gameObject.GetComponentInChildren<AudioSource>() 
                && component != gameObject.GetComponentInChildren<Animator>())
                {
                    component.enabled = false;
                }
            }
        
        yield return new WaitForSeconds(timer);
        //VFX
            Vector3 vfxPos = transform.GetComponentInChildren<MeshCollider>().bounds.center;

            ParticleSystem explosion = Instantiate(enemy.effects[0], vfxPos, Quaternion.identity);
            explosion.Play(); explosion.transform.localScale *= 1.4f;
            explosion.transform.SetParent(GameObject.Find("bin").transform);

        //SFX
            enemy.audioSource.PlayOneShot(audioExplosion);

        //Destroy
            enemy.mesh.SetActive(false);
            Destroy(gameObject, audioExplosion.length);
            Destroy(explosion.gameObject, 2f);
    }
#endregion Die


#region Animation Data
    private bool CheckForExistingAnimation(string clipname)
    {
        //Used to find specific animation in current enemy
        foreach(AnimatorControllerParameter param in enemy.animator.parameters)
        {
            if(param.name == clipname)
            {
                return true;
            }
        }
        return false;
    }
    private float GetAnimationLength(string clipname)
    {
        //Used to get the duration of specific animation
        RuntimeAnimatorController controller = enemy.animator.runtimeAnimatorController;

        foreach(AnimationClip clip in controller.animationClips)
        {
            if(clip.name == clipname)
            {
                if(clip.name == clipname)
                {
                    return clip.length;
                }
            }
        }
        return 0f;
    }
#endregion Animation Data


#region Drops
    private void Loot()
    {
        //xpOrb.GetComponent<DisableObjectOnTIme>().enabled=true; 
        //coin.GetComponent<DisableObjectOnTIme>().enabled=true; 

        Vector3 landingPosXP = GetLootLandingPosition() + new Vector3(Random.Range(-0.75f, 0.75f),0,Random.Range(-0.75f,0.75f));
        Vector3 landingPosCoin = GetLootLandingPosition() + new Vector3(Random.Range(-0.75f, 0.75f),0,Random.Range(-0.75f,0.75f));

        //foreach per escalar-lo
        GameObject xp = Instantiate(prefabXPOrb, transform.position, Quaternion.identity);
        GameObject coin = Instantiate(prefabCoin, transform.position, prefabCoin.transform.rotation);

        xp.GetComponent<DropLootLogic>().Initialize(landingPosXP);
        coin.GetComponent<DropLootLogic>().Initialize(landingPosCoin);

        xp.GetComponent<ResourceType>().InitializeValue(stats.XP);
        coin.GetComponent<ResourceType>().InitializeValue(stats.Coins);

        xp.transform.SetParent(GameObject.Find("bin").transform);
        coin.transform.SetParent(GameObject.Find("bin").transform);

        ///[Rare drops]
    }

    private Vector3 GetLootLandingPosition()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point + Vector3.up;
        }
        return transform.position;
    }
#endregion Drops
}
