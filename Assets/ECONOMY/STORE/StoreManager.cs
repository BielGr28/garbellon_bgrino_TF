using System;
using TMPro;
using UnityEngine;

/// <summary>
/// MANAGES all store realated logic
/// </summary>

public class StoreManager : MonoBehaviour
{
    [Header("General Refs")]
    [SerializeField] private GameObject storeMenu;
    public bool isStoreOpened;

    [Header("Economy")]
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private TextMeshProUGUI storeCoinText; //Coin text specific for store menu
    [SerializeField] private GameObject playerCoinBanner;  //In-game coin display
    [SerializeField] private GameObject playerXPBanner; //In-game xp display bar


    void Awake()
    {
        storeMenu.SetActive(false);
        playerCoinBanner.SetActive(true);
        isStoreOpened = false;
    }

    void Update()
    {
        if(isStoreOpened)
        {
            UpdateCoinAmount();
        }
    }


    public void OpenStore()
    {
        //Visual
        Debug.Log("Store openned");
        storeMenu.SetActive(true);
        isStoreOpened = true;
        playerCoinBanner.SetActive(false); 
        playerXPBanner.SetActive(false);

        //Logic
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ///playerController.enabled = false;
        GameObject.Find("Player_Test").GetComponent<PlayerController>().enabled = false;
    }

    public void CloseStore()
    {
        //Visual
        Debug.Log("Store closed");
        storeMenu.SetActive(false);
        isStoreOpened = false;
        playerCoinBanner.SetActive(true);
        playerXPBanner.SetActive(true);

        //Logic
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ///playerController.enabled = false;
        GameObject.Find("Player_Test").GetComponent<PlayerController>().enabled = true;

    }

    void UpdateCoinAmount()
    {
        storeCoinText.text = resourceManager.TotalRunCoins().ToString();

    }
}
