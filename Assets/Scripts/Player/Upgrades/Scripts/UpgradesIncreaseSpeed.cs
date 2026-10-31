using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Player Speed")]
public class UpgradesIncreaseSpeed : Upgrades
{
    [SerializeField] private float SpeedIncrease = 1.15f;
    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        playerController.MoveSpeed *= SpeedIncrease;
        playerController.SprintSpeed *= SpeedIncrease;

         Debug.Log(playerController.MoveSpeed + " / " + playerController.SprintSpeed);
    }

    public override void EspecialWeaponEffect(WeaponGeneral_Script upgradeWeapon)
    {
        throw new System.NotImplementedException();
    }
}
