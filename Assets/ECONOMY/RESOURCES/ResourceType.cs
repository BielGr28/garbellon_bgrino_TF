using UnityEngine;

public class ResourceType : MonoBehaviour
{
    public enum Resource 
    { XP, Coin }
    public Resource currentResource;
    [SerializeReference] private int resourceValue; //delete [Serialize...]

    private ResourceManager resourceManager;
    //private Enemy_Stats stats;


    void Awake()
    {
        //stats = GetComponentInParent<Enemy_Stats>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }
    void Start()
    {
        //SetResourceValue();
    }


    /*void SetResourceValue()
    {
        if(currentResource == Resource.XP)
        {
            resourceValue = stats.XP;
        }
        else if (currentResource == Resource.Coin)
        {
            resourceValue = stats.Coins;
        }
    }*/

    public void InitializeValue(int value)
    {
        resourceValue = value;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(currentResource == Resource.XP)
            {
                Debug.Log("<ADDED XP>: "+resourceValue);
                resourceManager.AddXP(resourceValue);
                Destroy(gameObject, 0.2f);
            }
            else if (currentResource == Resource.Coin)
            {
                Debug.Log("<ADDED COINS>: "+resourceValue);
                resourceManager.AddCoins(resourceValue);
                Destroy(gameObject, 0.2f);
            }  
        }
    }



}
