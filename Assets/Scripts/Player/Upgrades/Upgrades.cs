using UnityEngine;
public enum UpgradeType
{
    Player,
    CurrentWepon,
    Weapons
}

public enum UpgradeRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary

}

public  abstract class Upgrades : ScriptableObject
{

    //Information shown on the upgrades cards UI
    //Upgrade's name
    public string upgradeName;
    //Upgrade-s icon 
    public Sprite upgradeIcon;
    //Upgrade-s description
    public string upgradeDescription;

    //What stats does the upgrade powers up.
    public UpgradeType upgradeType;

    public UpgradeRarity upgradeRarity;

    //Level necesary to unlock the upgrade.
    public int unlockLevel;

    public int shopCost;

    //(Canviare els noms pq siguin més clars, pero pq veguis com funciona)
    //Method used to upgrade the current weapon stats
    public abstract void UpgradeCurrentWeapon(Weapon upgradeWeapon);
    //Method used to upgrade all weapons stats
    public abstract void UpgradeAllWeapons(WeaponController weaponController);
    //Method used to upgrades the player stats.
    public abstract void UpgradePlayer(PlayerController playerController);

}
