using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Ammo")]
public class UpgradeIncreaseAmmo : Upgrades
{
    [SerializeField] private float AMMOIncrease;
    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {
        float totalAmmo = (float)upgradeWeapon.TotalAmmo * AMMOIncrease;
        float currentAmmoPercentage = (float)upgradeWeapon.CurrentAmmo / upgradeWeapon.TotalAmmo;

        upgradeWeapon.TotalAmmo = Mathf.RoundToInt(totalAmmo);
        upgradeWeapon.CurrentAmmo = Mathf.RoundToInt(totalAmmo * currentAmmoPercentage);
        upgradeWeapon.UpdateWeaponUI(upgradeWeapon.CurrentAmmo);
    }
        

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
