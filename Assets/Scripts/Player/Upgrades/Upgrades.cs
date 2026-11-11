using UnityEngine;

public enum UpgradeType
{
    Damage,
    FireRate,
    Ammo,
    Precision,
    Speed,
    Jump,
    Special 
}

public  abstract class Upgrades : ScriptableObject
{

    public string upgradeName;
    public Sprite upgradeIcon;
    public string upgradeDescription;

    public UpgradeType upgradeType;

    public int unlockLevel = 0;

    public  abstract void EspecialWeaponEffect(Weapon upgradeWeapon);

    public abstract void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades);
    public abstract void EspecialPlayerEffect(PlayerController playerController);

}
