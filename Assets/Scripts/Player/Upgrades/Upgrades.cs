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


    public  abstract void EspecialWeaponEffect(WeaponGeneral_Script upgradeWeapon);


    public abstract void EspecialPlayerEffect(PlayerController playerController);

}
