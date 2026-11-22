using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Player Speed")]
public class UpgradesIncreaseSpeed : Upgrades
{
    [SerializeField] private float SpeedIncrease = 1.15f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {
        playerController.MoveSpeed *= SpeedIncrease;
        playerController.SprintSpeed *= SpeedIncrease;

        Debug.Log(playerController.MoveSpeed + " / " + playerController.SprintSpeed);
    }
}
