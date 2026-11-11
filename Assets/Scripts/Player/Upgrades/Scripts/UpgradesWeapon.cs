using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade/Upgrade Weapon")]
public class UpgradesWeapon : Upgrades
{
    public override void EspecialGeneralWeaponEffect(WeaponsGeneralUpgrades generalUpgrades)
    {
        throw new System.NotImplementedException();
    }

    //[SerializeField] private float WeaponStatsMultiplier = 1.15f;


    public override void EspecialPlayerEffect(PlayerController playerController)
    {
        return;
    }

    public override void EspecialWeaponEffect(Weapon weapon)
    {
        /*weapon.Damage *= WeaponStatsMultiplier;
        weapon.TotalAmmo *= (int)WeaponStatsMultiplier;
        weapon.ReloadSpeed = 
        weapon.FireRate = 
        weapon.Precision = */
       // Debug.Log(weapon.FireRate);
        //throw new System.NotImplementedException();
    }
}
