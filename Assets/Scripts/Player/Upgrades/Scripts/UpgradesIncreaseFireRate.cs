using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Increase Fire Rate")]
public class UpgradesIncreaseFireRate : Upgrades
{

    [SerializeField] private float FRIncrease = 0.1f;
    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void EspecialWeaponEffect(WeaponGeneral_Script upgradeWeapon)
    {
       upgradeWeapon.FireRate -= FRIncrease;
       Debug.Log(upgradeWeapon.FireRate);
    }
}
