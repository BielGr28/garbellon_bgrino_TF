using TMPro;
using UnityEngine;

/// <summary>
/// MANAGES LOGIC for both resources (XP & Coins)
/// </summary>

public class ResourceManager : MonoBehaviour
{
    private int playerXP; //saves player run xp amount
    private int playerCoins; //saves player run coins amount.

    [Header("Text Display")]
    [SerializeField] private TextMeshProUGUI[] resourceFlowPrefab; //XP-Coin
    private Transform flowXPSlot, flowCoinSlot;


    void Awake()
    {
        flowXPSlot = GameObject.Find("FlowXPSlot").transform;
        flowCoinSlot = GameObject.Find("FlowCoinSlot").transform;
    }

    public void AddXP(int amount) //Called when player gain xp
    {
        playerXP += amount;
    }
    public void AddCoins(int amount) //Called when player loot coins
    {
        playerCoins += amount;
    }


    public int TotalRunXP() //Returns total XP value from current run
    {
        return playerXP;
    }
    public int TotalRunCoins() //Returns total coins value from current run
    {
        return playerCoins;
    }


    public void ResetXP(int amount) //Decreases xp total (can be used in lvl-up)
    {
        playerXP -= amount;
    }
    public void ResetCoins(int amount) //decreases coin total (can be used for buying)
    {
        playerCoins -= amount;
    }
    

    public void XpFlowDisplayer(int income) //Displays xp gained by player in text
    {
        TextMeshProUGUI incomeDisplay1 = Instantiate(resourceFlowPrefab[0]);
        incomeDisplay1.transform.SetParent(flowXPSlot, false);
        incomeDisplay1.text = "+" + income.ToString();
    }
    public void CoinFlowDisplayer(int income) //Displays coins gained by player in text
    {
        TextMeshProUGUI incomeDisplay1 = Instantiate(resourceFlowPrefab[1]);
        incomeDisplay1.transform.SetParent(flowCoinSlot, false);
        incomeDisplay1.text = "+" + income.ToString();
    }
}
