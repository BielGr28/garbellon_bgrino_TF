using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/Higer Damage When Enemy is Close")]
public class U_DMCloser : Upgrades
{
    [SerializeField] ESPECIALEFFECTS especial = ESPECIALEFFECTS.CLOSER5METERS;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {   
       weaponController.generalUpgrades.playerEspecials.Add(especial);
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
