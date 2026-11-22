using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/Damage UP When Ammo is Higher50% ")]
public class U_DMUpHigher50AMMO : Upgrades
{
    [SerializeField] ESPECIALEFFECTS especial = ESPECIALEFFECTS.HIGHGER50AMMO;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {   
       weaponController.generalUpgrades.playerEspecials.Add(especial);
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
