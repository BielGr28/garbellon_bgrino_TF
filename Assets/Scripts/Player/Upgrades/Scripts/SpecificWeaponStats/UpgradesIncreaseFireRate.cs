using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Fire Rate")]
public class UpgradesIncreaseFireRate : Upgrades
{

    [SerializeField] private float FRIncreasePercentage = 0.1f;

    public override void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades)
    {}

    public override void EspecialPlayerEffect(PlayerController playerController)
    {}

    public override void EspecialWeaponEffect(Weapon weapon)
    {   
        //Calculate percentage and apply it to the weapon.
        float value = weapon.FireRate * FRIncreasePercentage;

        //Cut fire rate to maximum
        weapon.FireRate = Mathf.Max(0.05f, weapon.FireRate - value);;


        Debug.Log(weapon.FireRate);
    }
}
