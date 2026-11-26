using System;
using UnityEngine;

public enum UpgradeTypeTEST
{
    Player, CurrentWeapon, Weapons 
}
public enum UpgradeRarityTEST
{
    Common, Uncommon, Rare, Epic, Legendary
}

[CreateAssetMenu(fileName = "UpgradesTEST", menuName = "Scriptable Objects/UpgradesTEST")]
public abstract class UpgradesTEST : ScriptableObject
{
    public string upgradeName;
    public Sprite upgradeIcon;
    public string upgradeDescription;
    public UpgradeTypeTEST upgradeType;
    public UpgradeRarityTEST upgradeRarityTEST;
}
