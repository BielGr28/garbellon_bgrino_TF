using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Jump")]
public class UpgradesIncreaseJump : Upgrades
{
    [SerializeField] private float JumpIncrease = 1.15f;

    public override void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades)
    {}

    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        playerController.JumpHeight *= JumpIncrease;
        Debug.Log(playerController.JumpHeight);
    }

    public override void EspecialWeaponEffect(Weapon weapon)
    {}
}
