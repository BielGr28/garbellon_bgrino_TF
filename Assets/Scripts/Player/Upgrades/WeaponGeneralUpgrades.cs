using System.Collections.Generic;
using UnityEngine;

public enum ESPECIALEFFECTS
{
    HIGHGER50AMMO,
    BELOW50AMMO,
    CLOSER5METERS,
    FURTHERMAXDISTANCE
}
//Used to hold upgrades though for all player weapons.
public class WeaponGeneralUpgrades : MonoBehaviour
{
    public List<ESPECIALEFFECTS> playerEspecials;
    public float damageMultiplier;
    public float totalAmmoMultiplier;
    public float fireRateMultiplier;
    //public float precisionMultiplier;

    public float outputDamage;

    public float ApplyEspcialEffects(Weapon weapon,float hitDistance)
    {
        if(playerEspecials != null)
        {
            outputDamage = weapon.Damage;
            
            foreach (ESPECIALEFFECTS especial in playerEspecials)
            {
                switch(especial)
                {
                    case ESPECIALEFFECTS.HIGHGER50AMMO:
                        outputDamage = Damage_Higher50Ammo(outputDamage,weapon.CurrentAmmo,weapon.TotalAmmo);
                        Debug.Log($"Higher 50 Ammo");
                        break;

                    case ESPECIALEFFECTS.BELOW50AMMO:
                        outputDamage = Damage_Below50Ammo(outputDamage,weapon.CurrentAmmo,weapon.TotalAmmo);
                        Debug.Log("Below 50 Ammo");
                        break;

                    case ESPECIALEFFECTS.CLOSER5METERS:
                        outputDamage = Damage_Closer5Meters(outputDamage,hitDistance);
                        Debug.Log("Closer 5 ");
                        break;

                    case ESPECIALEFFECTS.FURTHERMAXDISTANCE:
                        outputDamage = Damage_FurtherMaxRange(outputDamage,hitDistance,weapon.MaxDamageRange);
                        Debug.Log("Furtehr Max");
                        break;
                }
                Debug.Log(outputDamage);
            }
            
            return outputDamage;
        }
        else
        {
            return weapon.Damage;
        }
    }

    public void AppplyMultipliers(Weapon weapon)
    {
        if(damageMultiplier > 0.0f)
            weapon.Damage *= damageMultiplier;

        if(fireRateMultiplier > 0.0f)
        {
            float value = weapon.FireRate * fireRateMultiplier; 
            weapon.FireRate = Mathf.Max(0.05f, weapon.FireRate - value);;
        }

        if(totalAmmoMultiplier > 0.0f)
        {
            float totalAmmoValue = (float)weapon.TotalAmmo;
            float currentAmmoPercentage = (float)weapon.CurrentAmmo/totalAmmoValue;

            totalAmmoValue *= totalAmmoMultiplier;
            float currentAmmoOutput = totalAmmoValue * currentAmmoPercentage;

            weapon.TotalAmmo = Mathf.RoundToInt(totalAmmoValue);
            weapon.CurrentAmmo = Mathf.RoundToInt(currentAmmoOutput);
        }           
    }


    public float Damage_Higher50Ammo(float baseDamage, int currentAmmo, int totalAmmo)
    {
        if(currentAmmo > totalAmmo/2)
        {
            float value = baseDamage * 1.5f;
            return value;
        }
        else
        {
            return baseDamage;
        }
    }

    public float Damage_Below50Ammo(float baseDamage, int currentAmmo, int totalAmmo)
    {
        if(currentAmmo <= totalAmmo/2)
        {
            float value = baseDamage * 2.0f;
            return value;
        }
        else
        {
            return baseDamage;
        }
    }
    public float Damage_Closer5Meters(float baseDamage, float hitDistance)
    {
        if(hitDistance <= 5.5f)
        {
            float value = baseDamage *1.5f;
            return value;
        }
        else
        {
            return baseDamage;
        }
    }

    public float Damage_FurtherMaxRange(float baseDamage,float hitDistance, float maxRange)
    {
        if(hitDistance >= maxRange)
        {
            float value;
            value = baseDamage * 1.5f;
            return value;
        }
        else
        {
            return baseDamage;
        }
 
    }

}
