using UnityEngine;

public class ResourceType : MonoBehaviour
{
    public enum Resource 
    { XP, Coin }
    public Resource currentResource;
    private int resourceValue;
    private ResourceManager resourceManager;


    void Awake()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

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
                resourceManager.XpFlowDisplayer(resourceValue);

                Destroy(gameObject, 0.2f);
            }
            else if (currentResource == Resource.Coin)
            {
                Debug.Log("<ADDED COINS>: "+resourceValue);

                resourceManager.AddCoins(resourceValue);
                resourceManager.CoinFlowDisplayer(resourceValue);
                
                Destroy(gameObject, 0.2f);
            }  
        }
    }
}
