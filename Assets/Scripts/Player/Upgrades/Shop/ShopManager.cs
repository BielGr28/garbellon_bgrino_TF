using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour
{    
    public UpgradesList upgradesList;
    [SerializeField] PlayerXP_Script playerXP;
    [SerializeField] PlayerController playerController;
    [SerializeField] WeaponController weaponController;

    [Header("SHOP ITEMS COMPOENENTS")]
    public List<XPSO_Holder> ShopHolders;
    public List<Upgrades> WeaponsInShop;
    public Upgrades weaponTierUp;
    [SerializeField] GameObject ShopUI;


    void Awake()
    {
        upgradesList = GameObject.FindGameObjectWithTag("UpgradesContainer").GetComponentInChildren<UpgradesList>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerXP_Script>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
    }

    //For testing == German canvia-ho per la teva funcio que obra la Ui i listos.
    public void ShowShopUI()
    {
        //SetShopItems();
        ShopUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void SetShopItems()
    {
        
        List<Upgrades> availableShopUpgrades = new List<Upgrades>();

        for (int i = 0; i < upgradesList.ShopUpgrades.Count; i++)
        {
            if(upgradesList.ShopUpgrades[i].unlockLevel <= playerXP.currentLevel)
            {
                availableShopUpgrades.Add(upgradesList.ShopUpgrades[i]);
            }
        }

        //ShopHolders.Count = 3
        //int 0, 1, 2
        //Fill only the first 2 items.
        for(int i = 0; i < 2; i++)
        {
            int randomUpgrade = Random.Range(0,availableShopUpgrades.Count);
            Upgrades upgradeItem = availableShopUpgrades[randomUpgrade];
            XPSO_Holder currentItem = ShopHolders[i];

            currentItem.upgrade = upgradeItem;
            currentItem.upgradeTitle.text = upgradeItem.upgradeName;
            currentItem.upgradeDescription.text = upgradeItem.upgradeDescription;

            //cards[i].IntilializeCard(upgradeItem)
            availableShopUpgrades.RemoveAt(randomUpgrade);
        }

        //Sets the last ShopHolder object.
        SetWeaponToGet(ShopHolders[^1]);

    }

    void SetWeaponToGet(XPSO_Holder lastShopItem)
    {
        int randomWeapon = Random.Range(0,WeaponsInShop.Count);
        BuyWeapon selectedWeapon = (BuyWeapon)WeaponsInShop[randomWeapon];
        string selectedWeaponName = selectedWeapon.weaponToGet.GetComponentInChildren<Weapon>().WeaponName;
        Debug.Log($"Shop Weapon {selectedWeaponName}");

        Upgrades finalItem;

        if(selectedWeaponName.Equals(weaponController.weapon.WeaponName))
        {
           finalItem = weaponTierUp;
        } 
        else
        {
            
            if( weaponController.WeaponsList[1].GetComponentInChildren<Weapon>(true) != null &&
                selectedWeaponName.Equals(weaponController.WeaponsList[1].GetComponentInChildren<Weapon>(true).WeaponName))
            {
                finalItem = weaponTierUp;
                //Debug.Log($"Has Weapon ({weaponController.WeaponsList[1].GetComponentInChildren<Weapon>(true).WeaponName})");
                //Debug.Log("Has Weapon");
            }
            else
            {
                finalItem = selectedWeapon;
                //Debug.Log($"Doesnt Have Weapon ({selectedWeaponName}) || Second Weapon Is ({weaponController.WeaponsList[1].GetComponentInChildren<Weapon>(true).WeaponName})");
            }
            
        }

        lastShopItem.upgrade = finalItem;
        lastShopItem.upgradeIcon = finalItem.upgradeIcon;
        lastShopItem.upgradeTitle.text = finalItem.upgradeName;
        lastShopItem.upgradeDescription.text = finalItem.upgradeDescription;
       
    }

    public void ChooseShopItem(XPSO_Holder choosenCard)
    {
        Upgrades choosenUpgrade = choosenCard.upgrade;
        //Upgrades choosenUpgrade = choosenCard.currentUpgrade;

        switch(choosenUpgrade.upgradeType)
        {
            case UpgradeType.CurrentWepon:
                //choosenUpgrade.EspecialWeaponEffect(weaponController.CurrentWeapon.GetComponent<WeaponGeneral_Script>());
                choosenUpgrade.UpgradeCurrentWeapon(weaponController.weapon);
            break;

             case UpgradeType.Player:
                choosenUpgrade.UpgradePlayer(playerController);
            break;

            case UpgradeType.Weapons:
                choosenUpgrade.UpgradeAllWeapons(weaponController);
            break;

            case UpgradeType.WeaponInShop:
                choosenUpgrade.UpgradeAllWeapons(weaponController);
            break;
        }

        //Aixo adapta-ho segons com funcioni la teva UI.
        //En un principi hauria de funcionar de la mateixa manera que la Ui de les millores normals.

        ShopUI.SetActive(false); 

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Time.timeScale = 1.0f;
        weaponController.canShoot = true;
    }

}
