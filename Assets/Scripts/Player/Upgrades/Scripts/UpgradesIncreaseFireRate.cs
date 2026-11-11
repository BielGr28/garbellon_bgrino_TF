using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Fire Rate")]
public class UpgradesIncreaseFireRate : Upgrades
{

    [SerializeField] private float FRIncrease = 0.1f;

    public override void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades)
    {
        throw new System.NotImplementedException();
    }

    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void EspecialWeaponEffect(Weapon weapon)
    {
        weapon.FireRate -= FRIncrease;
        Debug.Log(weapon.FireRate);
        //throw new System.NotImplementedException();
    }
}
