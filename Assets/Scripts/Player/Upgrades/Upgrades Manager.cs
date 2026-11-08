using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] PlayerXP_Script _playerXp;
    public WeaponController weaponController;
    public PlayerController playerController;
    public List<Upgrades> PlayerUpgrades;

    public List<XPSO_Holder> UpgradesHolders;
    [SerializeField] GameObject UpgradesUI;

    void Awake()
    {
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //weaponController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseAbility(XPSO_Holder choosenCard)
    {
        Upgrades choosenUpgrade = choosenCard.upgrade;

        switch(choosenUpgrade.upgradeType)
        {
            case UpgradeType.Damage:
                choosenUpgrade.EspecialWeaponEffect(weaponController.CurrentWeapon.GetComponent<WeaponGeneral_Script>());
            break;

            case UpgradeType.Ammo:
                choosenUpgrade.EspecialWeaponEffect(weaponController.CurrentWeapon.GetComponent<WeaponGeneral_Script>());
            break;

            case UpgradeType.FireRate:
                choosenUpgrade.EspecialWeaponEffect(weaponController.CurrentWeapon.GetComponent<WeaponGeneral_Script>());
            break;

            case UpgradeType.Precision:
                choosenUpgrade.EspecialWeaponEffect(weaponController.CurrentWeapon.GetComponent<WeaponGeneral_Script>());
            break;

            case UpgradeType.Jump:
                choosenUpgrade.EspecialPlayerEffect(playerController);
            break;

            case UpgradeType.Speed:
                choosenUpgrade.EspecialPlayerEffect(playerController);
            break;
        }

        UpgradesUI.SetActive(false); 

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Time.timeScale = 1.0f;
        weaponController.canShoot = true;
    }

    public void ShowUpgradesUI()
    {
        SetRandomUpgrade();
        UpgradesUI.SetActive(true);
    }


    //Choose between all upgrades available without repeating;
    void SetRandomUpgrade()
    {
        //Create an editable List of upgrades.
        List<Upgrades> availableUpgrades = new List<Upgrades>();

        //Fill the list with all the Upgrades with the same or lower level than the player.
        for(int i = 0;  i < PlayerUpgrades.Count; i++)
        {
            Debug.Log($"Current Level: {_playerXp.currentLevel} || Upgrade Level: {PlayerUpgrades[i].unlockLevel}");
            if(PlayerUpgrades[i].unlockLevel <= _playerXp.currentLevel)
            {
                availableUpgrades.Add(PlayerUpgrades[i]);
            }
        }
        
        Debug.Log($"Holders Count: {UpgradesHolders.Count}  || availableUpgrades Count: {availableUpgrades.Count}");
        //Fill the UI elements with a random Upgrade.
        for (var i = 0; i < UpgradesHolders.Count; i++)
        {
            int randomUpgrade = Random.Range(0,availableUpgrades.Count);

            XPSO_Holder currentUpgrade =  UpgradesHolders[i];
            Upgrades upgradeItem = availableUpgrades[randomUpgrade];


            currentUpgrade.upgrade = upgradeItem;
            currentUpgrade.upgradeTitle.text = upgradeItem.upgradeName;
            currentUpgrade.upgradeDescription.text = upgradeItem.upgradeDescription;
            //currentUpgrade.upgradeIcon = upgradeItem.upgradeIcon;

            //Remove the chosen upgrade form teh editable list to avoid repeating them.
            availableUpgrades.RemoveAt(randomUpgrade);
            Debug.Log("Upgrades Count: " + i);
        }
    }    


}
