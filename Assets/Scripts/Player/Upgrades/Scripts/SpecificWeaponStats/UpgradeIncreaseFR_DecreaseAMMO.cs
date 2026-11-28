using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Fire Rate_Decrease Ammo")]
public class UpgradesIncrease_FR_DecreaseAMMO : Upgrades
{

    [SerializeField] private float FRIncreasePercentage = 0.5f;
    [SerializeField] private float AMMODecreasePercentage = 0.75f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {
        upgradeWeapon.FireRate *= FRIncreasePercentage;

        float totalAmmoValue = (float)upgradeWeapon.TotalAmmo;
        float currentAmmoPercentage = (float)upgradeWeapon.CurrentAmmo / totalAmmoValue; 

        totalAmmoValue *= AMMODecreasePercentage;
        float finalCurrentAmmo = totalAmmoValue * currentAmmoPercentage;

        upgradeWeapon.TotalAmmo = Mathf.RoundToInt(totalAmmoValue);
        upgradeWeapon.CurrentAmmo = Mathf.RoundToInt(finalCurrentAmmo);
        upgradeWeapon.UpdateWeaponUI(upgradeWeapon.CurrentAmmo);

        Debug.Log(upgradeWeapon.FireRate);
    }

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
