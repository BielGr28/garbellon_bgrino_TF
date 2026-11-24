using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/ Upgrade Assault_Rifles")]
public class U_DMUpAssaultRifles : Upgrades
{
    [SerializeField] ESPECIALEFFECTS especial = ESPECIALEFFECTS.INCREASE_RIFLE;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {   
       weaponController.generalUpgrades.playerEspecials.Add(especial);
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
