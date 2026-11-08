using UnityEngine;

/// <summary>
/// MANAGES LOGIC for both resources (XP & Coins)
/// </summary>

public class ResourceManager : MonoBehaviour
{
    private int playerXP; //saves player run xp amount
    private int playerCoins; //saves player run coins amount.


#region TEST
    void Awake()
    {
        Debug.Log("add coin test key: 'K'");
    }
    void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddCoins(1);
        }
    }
#endregion TEST

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
    


}
