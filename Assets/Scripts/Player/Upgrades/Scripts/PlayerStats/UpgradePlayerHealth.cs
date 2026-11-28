using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Max Health")]
public class UpgradePlayerHealth : Upgrades
{
    [SerializeField] private float HealthIncrease = 1.15f;

    public override void UpgradeAllWeapons(WeaponController weaponController)
    {}

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {

        float currentHealthPercentage = playerController._playerHealth.currentHealth / playerController._playerHealth.maxHealth;
        playerController._playerHealth.maxHealth *= HealthIncrease;
        playerController._playerHealth.currentHealth = playerController._playerHealth.maxHealth * currentHealthPercentage;
        playerController._playerHealth.UpdateUI();
        Debug.Log(playerController._playerHealth.maxHealth);
    }
}
