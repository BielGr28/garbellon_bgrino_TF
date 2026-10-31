using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Jump")]
public class UpgradesIncreaseJump : Upgrades
{
    [SerializeField] private float JumpIncrease = 1.15f;
    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        playerController.JumpHeight *= JumpIncrease;
         Debug.Log(playerController.JumpHeight);
    }

    public override void EspecialWeaponEffect(WeaponGeneral_Script upgradeWeapon)
    {
        throw new System.NotImplementedException();
    }
}
