using UnityEngine;

public class ResourceType : MonoBehaviour
{
    public enum Resource 
    { XP, Coin }
    public Resource currentResource;
    private int resourceValue;

    private ResourceManager resourceManager;
    private Enemy_Stats stats;


    void Awake()
    {
        stats = GetComponentInParent<Enemy_Stats>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }
    void Start()
    {
        SetResourceValue();
    }

    //ASIGNAR EL VALOR INDICADO AL RESOURCE SOBRE el qual estamos

    //DETECTION

    void SetResourceValue()
    {
        if(currentResource == Resource.XP)
        {
            //resourceValue = stats.XP;
            resourceValue = 4; //TEST
        }
        else if (currentResource == Resource.Coin)
        {
            //resourceValue = stats.Coins;
            resourceValue = 5; //TEST
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(currentResource == Resource.XP)
            {
                resourceManager.AddXP(resourceValue);
                Destroy(gameObject, 0.2f);
            }
            else if (currentResource == Resource.Coin)
            {
                resourceManager.AddCoins(resourceValue);
                Destroy(gameObject, 0.2f);
            }  
        }
    }



}
