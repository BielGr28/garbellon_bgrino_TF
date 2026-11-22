using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Upgrade Weapon")]
public class UpgradesWeapon : Upgrades
{   
    //Upgrade all weapon stats by 25%
    
    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {
        upgradeWeapon.Damage *= 1.25f;
        
        float frValue = upgradeWeapon.FireRate * 0.25f;
        upgradeWeapon.FireRate = Mathf.Max(0.05f,upgradeWeapon.FireRate - frValue);

        float currentAmmoPercentage = (float)upgradeWeapon.CurrentAmmo / (float)upgradeWeapon.TotalAmmo;
        upgradeWeapon.TotalAmmo = Mathf.RoundToInt((float)upgradeWeapon.TotalAmmo * 1.25f);
        upgradeWeapon.CurrentAmmo = Mathf.RoundToInt((float)upgradeWeapon.TotalAmmo * currentAmmoPercentage);
        upgradeWeapon.UpdateWeaponUI(upgradeWeapon.CurrentAmmo);
    }

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
