using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/Damage UP When Ammo is Below50% ")]
public class U_DMUpBelowAMMMO : Upgrades
{
    [SerializeField] ESPECIALEFFECTS especial = ESPECIALEFFECTS.BELOW50AMMO;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {   
       weaponController.generalUpgrades.playerEspecials.Add(especial);
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
