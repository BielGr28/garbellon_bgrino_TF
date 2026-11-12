using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Damage")]
public class UpgradeIncreaseDamage : Upgrades
{

    public float DamageIncrease = 1.10f;

    public override void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades)
    {}

    public override void EspecialPlayerEffect(PlayerController playerController)
    {}

    /*public override void EspecialWeaponEffect(WeaponGeneral_Script upgradeWeapon)
    {
        upgradeWeapon.Damage = upgradeWeapon.Damage * DamageIncrease;
        Debug.Log(upgradeWeapon.Damage);
    }*/

    public override void EspecialWeaponEffect(Weapon upgradeWeapon)
    {
        upgradeWeapon.Damage = upgradeWeapon.Damage * DamageIncrease;
        Debug.Log(upgradeWeapon.Damage);
    }
}
