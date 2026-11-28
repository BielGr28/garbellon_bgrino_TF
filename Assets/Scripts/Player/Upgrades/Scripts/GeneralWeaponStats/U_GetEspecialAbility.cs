using UnityEngine;
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/AllWeapons/Special Abilities")]
public class U_GetEspecialAbility : Upgrades
{

    [SerializeField] ESPECIALEFFECTS specialAbility;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {
       weaponController.generalUpgrades.playerEspecials.Add(specialAbility);
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
