using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Damage")]
public class UpgradeIncreaseDamage : Upgrades
{
    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void EspecialWeaponEffect(WeaponGeneral_Script upgradeWeapon)
    {
        throw new System.NotImplementedException();
    }
}
