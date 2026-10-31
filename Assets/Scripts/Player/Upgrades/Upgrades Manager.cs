using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{

    public WeaponController weaponController;
    public PlayerController playerController;
    public List<Upgrades> PlayerUpgrades;

    public XPSO_Holder[] UpgradesHolders;
    [SerializeField] GameObject UpgradesUI;

    void Awake()
    {
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //weaponController =  GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
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
    }

    public void ShowUpgradesUI()
    {
        SetRandomUpgrade();
        UpgradesUI.SetActive(true);
    }


    void SetRandomUpgrade()
    {
       for (var i = 0; i < UpgradesHolders.Length; i++)
       {
            int randomUpgrade = Random.Range(0,PlayerUpgrades.Count);
            XPSO_Holder currentUpgrade =  UpgradesHolders[i];
            Upgrades upgradeItem = PlayerUpgrades[randomUpgrade];

            currentUpgrade.upgrade = upgradeItem;
            currentUpgrade.upgradeTitle.text = upgradeItem.upgradeName;
            currentUpgrade.upgradeDescription.text = upgradeItem.upgradeDescription;
            //currentUpgrade.upgradeIcon = upgradeItem.upgradeIcon;
       }
    }    


}
