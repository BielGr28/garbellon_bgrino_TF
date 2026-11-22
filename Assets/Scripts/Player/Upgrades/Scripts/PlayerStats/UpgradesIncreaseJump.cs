using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Jump")]
public class UpgradesIncreaseJump : Upgrades
{
    [SerializeField] private float JumpIncrease = 1.15f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {
        playerController.JumpHeight *= JumpIncrease;
        Debug.Log(playerController.JumpHeight);
    }
}
