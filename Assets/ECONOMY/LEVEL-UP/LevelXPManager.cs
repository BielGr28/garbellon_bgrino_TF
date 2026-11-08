using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelXPManager : MonoBehaviour
{
    [Header("Level Refs.")]
    [SerializeField] private int currentLevel = 1;
    //public int currentExp -> ResourceManager (playerXP) 
    [SerializeField] private int xpToLevelUp = 10; //Saves the amount of xp needed to lvl-up
    [SerializeField] float xpLvlMultiplier = 1.2f;
    [Header ("Components")]
    [SerializeField] private ResourceManager resourceManager;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentLvlText;
    [SerializeField] private TextMeshProUGUI currentXPText;
    [SerializeField] private Slider gameXPSlider;


    void Awake()
    {
        Debug.Log("increase xp by 3 'L'");
    }
    void Update()
    {
        LevelUp();
        DisplayLvlUI();

        if(Input.GetKeyDown(KeyCode.L))//TEST
        {
            resourceManager.AddXP(3);
        }
        Debug.Log("TotalXP: "+resourceManager.TotalRunXP());
    }

    void LevelUp()
    {
        Debug.Log("A");
        if(resourceManager.TotalRunXP() >= xpToLevelUp)
        {
            currentLevel++; //Lvl increase
            resourceManager.ResetXP(xpToLevelUp); //resets current xp based on lvl
            xpToLevelUp = Mathf.RoundToInt(xpToLevelUp * xpLvlMultiplier); //xp to lvl multiplier
        }
    }

    void DisplayLvlUI()
    {
        currentLvlText.text = "Level "+currentLevel; //Level 9
        currentXPText.text = resourceManager.TotalRunXP()+" / "+xpToLevelUp+"xp"; //15 / 42xp

        gameXPSlider.maxValue = xpToLevelUp; //Gets the bar scaling based on xp needed
        gameXPSlider.value = resourceManager.TotalRunXP(); //Moves slider bar based on xp gained
    }
}