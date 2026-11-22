using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/Increase Weapons Ammo")]
public class U_IncreaseWeaponsAmmo : Upgrades
{
    public float AMMOIncrease;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {   

        if(weaponController.generalUpgrades.totalAmmoMultiplier > 0.0f)
        {
            weaponController.generalUpgrades.totalAmmoMultiplier *= AMMOIncrease;
        }
        else
        {
            weaponController.generalUpgrades.totalAmmoMultiplier = AMMOIncrease;
        }

        weaponController.UpdateAllWeaponsStats();
        
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
