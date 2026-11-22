using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Gain Shield_LoseHealth")]
public class UpgradeGainShield_LoseHealth : Upgrades
{
    [SerializeField] private float ShieldToGain = 50.0f;
    [SerializeField] private float LoseHealth = 0.85f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {
        playerController._playerHealth.GainShield(ShieldToGain);
        playerController._playerHealth.currentHealth = playerController._playerHealth.maxHealth * LoseHealth;
    }
}
