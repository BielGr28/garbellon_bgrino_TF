using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Fire Rate")]
public class UpgradesIncreaseFireRate : Upgrades
{

    [SerializeField] private float FRIncreasePercentage = 0.1f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {
        float value = upgradeWeapon.FireRate * FRIncreasePercentage;

        //Cut fire rate to maximum
        upgradeWeapon.FireRate = Mathf.Max(0.05f, upgradeWeapon.FireRate - value);;


        Debug.Log(upgradeWeapon.FireRate);
    }

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
