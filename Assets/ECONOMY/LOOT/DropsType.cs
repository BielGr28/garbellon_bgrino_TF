using UnityEngine;

public class DropsType : MonoBehaviour
{
    public enum Drop 
    { XP, Coin, HealthKit, Shield}
    public Drop currentDrop;
    [SerializeReference] private int resourceValue;
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
            switch(currentDrop)
            {
                case Drop.XP:
                    Debug.Log("<ADDED XP> "+resourceValue);
                    resourceManager.AddXP(resourceValue);
                    resourceManager.XpFlowDisplayer(resourceValue);
                    Destroy(gameObject, 0.2f);
                break;

                case Drop.Coin:
                    Debug.Log("<ADDED COINS> "+resourceValue);
                    resourceManager.AddCoins(resourceValue);
                    resourceManager.CoinFlowDisplayer(resourceValue);
                    Destroy(gameObject, 0.2f);
                break;
                
                case Drop.Shield:
                    Debug.Log("<PICKED> Shield");
                   
                    ///float shield = other.gameObject.GetComponent<Shield>().CalcultateShieldPoints(maxHealth);
                    ///GainShield(shield);

                    Destroy(gameObject, 0.2f);
                break;

                case Drop.HealthKit:
                    Debug.Log("<PICKED> HealthKit");
                    
                    //float heal = other.gameObject.GetComponent<Medkit>().CalcultateHealingPoints(maxHealth);
                    //GainHealth(heal);

                    Destroy(gameObject, 0.2f);
                break;
            }
        }
    }
}
