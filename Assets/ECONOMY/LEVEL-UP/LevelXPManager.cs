using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelXPManager : MonoBehaviour
{
    [Header("Level References")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int xpToLevelUp = 10; //Saves the amount of xp needed to lvl-up
    [SerializeField] float xpLvlMultiplier = 1.2f;
    [Header ("Components")]
    [SerializeField] private ResourceManager resourceManager;
    [Header("UI")]
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject playerCoinBanner;  //In-game coin display
    [SerializeField] private GameObject playerXPBanner; //In-game xp display bar
    [SerializeField] private TextMeshProUGUI currentLvlText;
    [SerializeField] private TextMeshProUGUI currentXPText;
    [SerializeField] private Slider gameXPSlider;


    void Awake()
    {
        levelMenu.SetActive(false);
        //isMenuOpened = false;
    }
    void Update()
    {
        LevelUp();
        DisplayLvlUI();
    }

    void LevelUp()
    {
        if(resourceManager.TotalRunXP() >= xpToLevelUp)
        {
            currentLevel++; //Lvl increase
            resourceManager.ResetXP(xpToLevelUp); //resets current xp based on lvl
            xpToLevelUp = Mathf.RoundToInt(xpToLevelUp * xpLvlMultiplier); //xp to lvl multiplier
            
            StartCoroutine(OpenLvlUpMenu());
        }
    }

    void DisplayLvlUI()
    {
        currentLvlText.text = "Level "+currentLevel; //Level 9
        currentXPText.text = resourceManager.TotalRunXP()+" / "+xpToLevelUp+"xp"; //15 / 42xp

        gameXPSlider.maxValue = xpToLevelUp; //Gets the bar scaling based on xp needed
        gameXPSlider.value = resourceManager.TotalRunXP(); //Moves slider bar based on xp gained
    }

    private IEnumerator OpenLvlUpMenu() //Called on level-up
    {
        yield return new WaitForSeconds(2f);
        
        //Visual
        Debug.Log("Menu Opened!");
        levelMenu.SetActive(true);
        playerCoinBanner.SetActive(false); 
        playerXPBanner.SetActive(false);

        //Logic
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("Player_Test").GetComponent<PlayerController>().enabled = false;
    }

    public void CloseLvlUpMenu() //Called from itemCard button comp.
    {
        //Visual
        Debug.Log("Menu Closed!");
        levelMenu.SetActive(false);
        playerCoinBanner.SetActive(true);
        playerXPBanner.SetActive(true);

        //Logic
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.Find("Player_Test").GetComponent<PlayerController>().enabled = true;
    }
}