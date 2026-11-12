using UnityEngine;

public class U_IncreaseWeaponsAmmo : Upgrades
{
    public float AMMOIncrease = 1.15f;
    public override void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades)
    {
        generalUpgrades.totalAmmoMultiplier *= AMMOIncrease;
    }

    public override void EspecialPlayerEffect(PlayerController playerController)
    {}

    public override void EspecialWeaponEffect(Weapon upgradeWeapon)
    {}
}
