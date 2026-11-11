using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    Single,
    Automatic
}
public abstract class Weapon : MonoBehaviour
{   
    public abstract WeaponController _weaponController{get;}
    public abstract WeaponType WeaponType {get;}
    public abstract string Name {get;}
    public abstract float Damage {set; get;}
    //public abstract float DistanceDamageMultiplier{set; get;}
    public abstract int TotalAmmo {set; get;}
    public abstract int CurrentAmmo { set; get;}
    public abstract float ReloadSpeed {set; get;}
    public abstract float FireRate {set; get;}
    public abstract float Precision {set; get;}
    protected float Range = 50.0f;
    public abstract float MaxDamageRange {set; get;}

    //Sets weapon's base stats.
    public abstract void SetDefaultStats(float damage, int totalAmmo, float reloadSpeed, float fireRate, float precision, float maxRange);
    public virtual float CalculateDamage(float hitDistance, float baseDamage, float multiplier)
    {
        float damageMultiplied = baseDamage * multiplier;
        if(hitDistance <= MaxDamageRange)
        {
            return damageMultiplied;
        }
        else
        {
            float outputDamage = damageMultiplied * 0.8f;
            return outputDamage;
        }
    }
    public abstract void Shoot();
    public abstract void Reload();
    public abstract IEnumerator ShootingDelay(float delay);
    public abstract IEnumerator ReloadingDelay(float delay);
    public abstract void Especial();
    public abstract void CroshairState(GameObject croshair);
    public abstract void UpdateUI(int ammoUI);
    public abstract void SetLineRenderer(LineRenderer lineRenderer, Transform startPos, Vector3 endPos);

}


