using System.Collections;
using Unity.Android.Gradle;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

using UnityEngine.UI;

public enum WeaponType
{
    PISTOL,
    ASSAULT_RIFLE,
    SHOTGUN,
    SNIPER_RIFLE
}
public abstract class Weapon : MonoBehaviour
{   
    public abstract WeaponController _weaponController{get;}
    public abstract Sprite Icon{set;get;}
    public abstract GameObject Croshair {get;}
    public abstract WeaponType WeaponType {get;}
    public abstract string WeaponName {get;}
    public abstract float Damage {set; get;}
    public abstract int TotalAmmo {set; get;}
    public abstract int CurrentAmmo { set; get;}
    public abstract float ReloadSpeed {set; get;}
    public abstract float FireRate {set; get;}
    public abstract float Precision {set; get;}
    protected float Range = 50.0f;
    public abstract float MaxDamageRange {set; get;}
    private const float MAXDISTANCEDAMAGE = 0.8f;

    public abstract Animator _animator{get;}
    public const string AN_SHOOT = "Shoot";
    public const string AN_RELOAD = "Reload";
    public const string AN_SPRINTING = "IsSprinting";
    public const string AN_GROUNDED = "IsGrounded";

    //Sets weapon's base stats.
    public virtual void SetDefaultStats(float damage, int totalAmmo, float reloadSpeed, float fireRate, float precision, float maxRange)
    {
        Damage = damage;
        TotalAmmo = totalAmmo;
        CurrentAmmo = TotalAmmo;
        ReloadSpeed = reloadSpeed;
        FireRate = fireRate;
        Precision = precision;
        MaxDamageRange = maxRange;
    }
    public virtual float CalculateDamage(float hitDistance, WeaponGeneralUpgrades generalUpgrades )
    {
        //Damage Applying Efects
        float damageWithEffects = generalUpgrades.ApplyEspcialEffects(this,hitDistance);
       
        ESPECIALEFFECTS ignoreDistance = ESPECIALEFFECTS.FURTHERMAXDISTANCE;
        
        if(generalUpgrades.playerEspecials.Contains(ignoreDistance))
        {
            return damageWithEffects;
        }
        else
        {
            if(hitDistance <= MaxDamageRange)
            {
                return damageWithEffects;
            }
            else
            {
                float outputDamage = damageWithEffects * MAXDISTANCEDAMAGE;
                return outputDamage;
            }
        }        
    }

    void FixedUpdate()
    {
        RaycastHit detectEnemy;
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f)),out detectEnemy, 100.0f))
        {
            
            Image[] images =  Croshair.GetComponentsInParent<Image>();
            if(detectEnemy.collider.CompareTag("Enemy"))
            {
                foreach (var img in images)
                {
                    img.color = Color.red;
                }
            }
            else
            {
                foreach (var img in images)
                {
                    img.color = Color.white;
                }
            }
        }

       
    }

    void Update()
    {
        RectTransform rect = Croshair.GetComponent<RectTransform>();
        
        Vector3 currentScale = rect.localScale;
        currentScale.x = Mathf.Lerp(currentScale.x,1.0f,Precision * Time.fixedDeltaTime);
        currentScale.y = Mathf.Lerp(currentScale.y,1.0f,Precision * Time.fixedDeltaTime);

        rect.localScale = currentScale;
    }
    public abstract void Shoot();
    //public abstract void Reload();

    public virtual void Reload()
    {
        if(CurrentAmmo < TotalAmmo)
        {
            _animator.SetTrigger(AN_RELOAD);
            IEnumerator coroutine = ReloadingDelay(ReloadSpeed);
            StartCoroutine(coroutine);
        }
    }
    public virtual IEnumerator ShootingDelay(float delay)
    {
        _weaponController.canShoot = false;
        yield return new WaitForSeconds(delay);
        _weaponController.canShoot = true;
    }
    public virtual IEnumerator ReloadingDelay(float delay)
    {
        _weaponController.canShoot = false;
        _weaponController.canReload = false;
        yield return new WaitForSeconds(delay);
        CurrentAmmo = TotalAmmo;
        UpdateWeaponUI(CurrentAmmo);
        _weaponController.canShoot = true;
        _weaponController.canReload = true;
    }
    public abstract void Special();
    public virtual void CroshairState(GameObject croshair)
    {

        RectTransform rect = croshair.GetComponent<RectTransform>();
        Vector3 currecntScale = rect.localScale;
    
        currecntScale.x += Precision;
        currecntScale.y += Precision;

        rect.localScale = currecntScale;
    }

    public virtual void UpdateWeaponUI(int ammoUI)
    {
        string UiString = $"{ammoUI} / {TotalAmmo}";
        _weaponController._gunAmmo.text = UiString; 
    }
    public virtual void SetLineRenderer(LineRenderer lineRenderer, Transform startPos, Vector3 endPos)
    {
        lineRenderer.startColor = new Color(0.0f,0.0f,1.0f,0.45f);
        lineRenderer.endColor = Color.lightBlue;

        lineRenderer.SetPosition(0,startPos.position);
        lineRenderer.SetPosition(1,endPos);
    }


}



