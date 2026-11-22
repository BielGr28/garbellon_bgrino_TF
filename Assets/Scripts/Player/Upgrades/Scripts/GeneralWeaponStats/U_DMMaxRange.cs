using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/Higer Damage When Enemy at Max Range")]
public class U_DMMaxRange : Upgrades
{
    [SerializeField] ESPECIALEFFECTS especial = ESPECIALEFFECTS.FURTHERMAXDISTANCE;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {   
       weaponController.generalUpgrades.playerEspecials.Add(especial);
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
