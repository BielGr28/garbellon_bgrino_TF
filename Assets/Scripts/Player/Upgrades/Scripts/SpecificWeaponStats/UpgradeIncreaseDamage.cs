using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Damage")]
public class UpgradeIncreaseDamage : Upgrades
{

    [SerializeField] float DamageIncrease = 1.10f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {
        upgradeWeapon.Damage = upgradeWeapon.Damage * DamageIncrease;
        Debug.Log(upgradeWeapon.Damage);
    }

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
