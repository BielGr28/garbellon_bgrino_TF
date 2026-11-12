using System.Collections;
using Mono.Cecil;
using UnityEngine;

[RequireComponent(typeof(EnemyLogic))]
public class Enemy_Health : MonoBehaviour
{
    private Enemy_Stats stats;
    private EnemyLogic enemy;

    [Header("Drops")]
    [SerializeField] private GameObject[] prefabs; //0-xp > 1-coin
    [Header("Rare Drops")]
    [SerializeField] [Range(0f,100f)] private float rareDropChance;
    [SerializeField] private RareDropData[] rareDrops;
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
            RareDrops();
        
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


#region Base Drops
    private Vector3 GetLootLandingPosition()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point + Vector3.up;
        }
        return transform.position;
    }

    void SpawnLoot(GameObject prefab, int value, Vector3 landingPos)
    {
        GameObject loot = Instantiate(prefab, transform.position, prefab.transform.rotation);

        loot.GetComponent<DropLootLogic>().Initialize(landingPos);
        loot.GetComponent<ResourceType>().InitializeValue(value);
        loot.transform.SetParent(GameObject.Find("bin").transform);
    }

    private void Loot()
    {
        Vector3 baseLandingPos = GetLootLandingPosition();

        //XP
        SpawnLoot(prefabs[0], stats.XP, baseLandingPos+new Vector3(Random.Range(-0.75f, 0.75f),0,Random.Range(-0.75f, 0.75f)));
        //Coin
        SpawnLoot(prefabs[1], stats.Coins, baseLandingPos+new Vector3(Random.Range(-0.75f, 0.75f),0,Random.Range(-0.75f, 0.75f)));
    }

    void SpawnRareLoot(GameObject prefab, Vector3 landingPos)
    {
        GameObject rare = Instantiate(prefab, transform.position, prefab.transform.rotation);

        rare.GetComponent<DropLootLogic>().Initialize(landingPos);
        rare.transform.SetParent(GameObject.Find("bin").transform);
    }

    void RareDrops()
    {
        float roll = Random.Range(0f, 100f); //Dice roll
        Debug.Log("[0 - "+"<RareDropChance "+rareDropChance+"> - <ROLL "+roll+"> - 100|");
        if(roll <= rareDropChance) //rareDropChance(15) - roll(13) - global |0-100| >  my chance |0-15| > roll |0-(13)-15| [PREMIO]
        {
            float totalWeight = 0f;

            foreach(var drop in rareDrops)
            {
                totalWeight += drop.dropWeight; //Sums all drops chances
            }

            float randomWeight = Random.Range(0f, totalWeight); //gets 1 chance between 0 and total > |0-5|health & |5-10|shield (ex.)
            Debug.Log("[RandomWeight "+randomWeight);
            float currentWeight = 0f;

            foreach(var drop in rareDrops)
            {
                currentWeight += drop.dropWeight;

                if(randomWeight <= currentWeight) //if random chance |0-10| (2.4) <= total chances / instantiates drop.prefab which chance is (2.4)
                {
                    Debug.Log("[CurrentWeight "+currentWeight);
                    Vector3 baseLandingPos = GetLootLandingPosition();
                    SpawnRareLoot(drop.prefab, baseLandingPos + new Vector3(Random.Range(-0.75f, 0.75f),0f,Random.Range(-0.75f, 0.75f)));
                    return;
                }
            }
        }
    }
#endregion Drops
}


[System.Serializable] 
public class RareDropData
{
    public GameObject prefab;
    [Range(0f,100f)]
    public float dropWeight;
}