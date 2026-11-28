using System.Collections.Generic;
using UnityEngine;

public enum ESPECIALEFFECTS
{
    HIGHGER50AMMO,
    BELOW50AMMO,
    CLOSER5METERS,
    FURTHERMAXDISTANCE,
    CRITCHANCE,
    LOWHEALTH,
    MAXHEALTH,
    WITHSHIELD,
    INCREASE_SHOTGUN,
    INCREASE_SNIPER,
    INCREASE_RIFLE,
    INCREASE_PISTOL
}
//Manages the players upgrades that effect all weapons.
public class WeaponGeneralUpgrades : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public List<ESPECIALEFFECTS> playerEspecials;
    public float damageMultiplier;
    public float totalAmmoMultiplier;
    public float fireRateMultiplier;
    public float critChance;
    //public float precisionMultiplier;

    public float outputDamage;

    void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    //Calculate weapon damage based on the player current upgrades.
    public float ApplyEspecialEffects(Weapon weapon,float hitDistance)
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
                        Debug.Log("Higher 50 Ammo");
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
                    case ESPECIALEFFECTS.CRITCHANCE:
                        outputDamage = Damage_CriticalChance(outputDamage);
                        Debug.Log("Crit Chance");
                    break;

                    case ESPECIALEFFECTS.MAXHEALTH:
                        outputDamage = Damage_OnMaxHealth(outputDamage,playerHealth.currentHealth,playerHealth.maxHealth);
                        Debug.Log("Max Health");
                    break;

                    case ESPECIALEFFECTS.LOWHEALTH:
                        outputDamage = Damage_OnLowHealth(outputDamage,playerHealth.currentHealth,playerHealth.maxHealth);
                        Debug.Log("Low Health");
                    break;

                    case ESPECIALEFFECTS.WITHSHIELD:
                        outputDamage = Damage_WithShield(outputDamage,playerHealth.currentShield);
                        Debug.Log("With Shield");
                    break;


                    case ESPECIALEFFECTS.INCREASE_PISTOL:
                        outputDamage = Damage_Pistols(outputDamage,weapon.WeaponType);
                        Debug.Log("Pistols Multiplier");
                    break;

                    case ESPECIALEFFECTS.INCREASE_SHOTGUN:
                        outputDamage = Damage_Shotguns(outputDamage,weapon.WeaponType);
                        Debug.Log("Shotguns Multiplier");
                    break;

                    case ESPECIALEFFECTS.INCREASE_RIFLE:
                        outputDamage = Damage_Rifles(outputDamage,weapon.WeaponType);
                        Debug.Log("Rifles Multiplier");
                    break;

                    case ESPECIALEFFECTS.INCREASE_SNIPER:
                        outputDamage = Damage_Snipers(outputDamage,weapon.WeaponType);
                        Debug.Log("Snipers Multiplier");
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


#region ESPECIAL EFFECTS

    #region DAMAGE EFFECTS
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
            float value = baseDamage * 1.5f;
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

    public float Damage_CriticalChance(float baseDamage)
    {
        float random = Random.Range(0.1f, 1.0f);

        if(random <= critChance)
        {
            float outdamage = baseDamage * 1.5f;
            return outdamage;
        }
        else
        {
          return baseDamage;       
        }
        
    }

    public float Damage_OnLowHealth(float baseDamage, float currentHealth, float maxHealth)
    {

        float limit = maxHealth * 0.2f;
        if(currentHealth >= limit)
        {
            float outdamage = baseDamage * 2.0f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
    }

    public float Damage_OnMaxHealth(float baseDamage, float currentHealth ,float maxHealth)
    {
        if(currentHealth == maxHealth)
        {
            float outdamage = baseDamage * 1.75f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
    }

    public float Damage_WithShield(float baseDamage, float shield)
    {
        if(shield > 0.0f)
        {
            float outdamage = baseDamage * 1.25f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
    }



    public float Damage_Shotguns(float baseDamage, WeaponType weaponType)
    {
        if(weaponType.Equals("Shotgun"))
        {
            float outdamage = baseDamage * 1.15f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
        
    }

    public float Damage_Snipers(float baseDamage, WeaponType weaponType)
    {
        if(weaponType.Equals("Sniper"))
        {
            float outdamage = baseDamage * 1.15f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
    }

    public float Damage_Pistols(float baseDamage, WeaponType weaponType)
    {
        if(weaponType.Equals("Pistol"))
        {
            float outdamage = baseDamage * 1.15f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
    }

    public float Damage_Rifles(float baseDamage, WeaponType weaponType)
    {
        if(weaponType.Equals("Rifle"))
        {
            float outdamage = baseDamage * 1.15f;
            return outdamage;
        }
        else
        {
            return baseDamage;
        }
    }
    #endregion


#endregion

}
