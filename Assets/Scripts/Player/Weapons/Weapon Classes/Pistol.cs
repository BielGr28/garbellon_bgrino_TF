using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    public override string Name => "Pistol";

    public override float Damage {set;  get;}
    public override int TotalAmmo {set;  get;}

    public override int CurrentAmmo { set;  get;}

    public override float ReloadSpeed { set;  get;}
    public override float FireRate { set; get;}

    public override float Precision { set; get;}
    public override float MaxDamageRange { set; get; }
    public override GameObject Croshair => GameObject.FindGameObjectWithTag("CrossHair");


    public override WeaponType WeaponType => WeaponType.Single;

    public override WeaponController _weaponController => GetComponentInParent<WeaponController>();


    public Transform PSPoint;

    [Header("VFX ELEMENTS")]
    [SerializeField] private Animator _pistolAnimator;
    [SerializeField] private GameObject _shootingPS;
    [SerializeField] private GameObject _impactPS;

    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _shootingClip;
    [SerializeField] private AudioClip _reloadingClip;

    public override void SetDefaultStats(float damage, int totalAmmo, float reloadSpeed, float fireRate, float precision, float maxRange)
    {
        Damage = damage;
        TotalAmmo = totalAmmo;
        CurrentAmmo = TotalAmmo;
        ReloadSpeed = reloadSpeed;
        FireRate = fireRate;
        Precision = precision;
        MaxDamageRange = maxRange;
    }

    void Awake()
    {
        SetDefaultStats(4.0f,20,1.0f,0.5f,10f,20.0f);
    }
    
    public override void CroshairState(GameObject croshair)
    {
        throw new System.NotImplementedException();
    }

    //Not used in this weapon
    public override void Especial()
    {
        return;
    }

    public override void Reload()
    {
        IEnumerator coroutine = ReloadingDelay(ReloadSpeed);
        StartCoroutine(coroutine);
    }

    public override void Shoot()
    {
      if(CurrentAmmo > 0)
        {
            RaycastHit hit;

           // _audioSource.clip = _shootingClip;
           // _audioSource.Play();

            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f)),out hit, Range))
            {

                SetLineRenderer(_shootingPS.GetComponent<LineRenderer>(),PSPoint,hit.point);
                GameObject bulletTrail;
                bulletTrail = GameObject.Instantiate(_shootingPS,PSPoint.position,Quaternion.identity);
                GameObject.Destroy(bulletTrail,0.2f);
                
                GameObject bulletImpact;
                bulletImpact = GameObject.Instantiate(_impactPS,hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward,-hit.normal));
                GameObject.Destroy(bulletImpact,0.15f);

                if(hit.collider.gameObject.CompareTag("Enemy"))
                {
                    float distanceToTarget = Vector3.Distance(PSPoint.position,hit.point);
                    float multiplier = _weaponController.generalUpgrades.damageMultiplier;
                    Debug.Log($"DISTANCE: {distanceToTarget} || DAMAGE: {CalculateDamage(distanceToTarget, Damage,multiplier)}");
                    
                    //hit.collider.GetComponent<Enemy_Health>().EnemyDamaged(Damage);
                }
            }
            //Spawn ps even if we dont hit.

            CurrentAmmo -= 1;
            UpdateUI(CurrentAmmo);
            IEnumerator coroutine = ShootingDelay(FireRate);
            StartCoroutine(coroutine);
        }
        else
        {
            Debug.Log("OUT OF AMMO");
            Reload();
        }    
    }

    public override IEnumerator ShootingDelay(float delay)
    {
        _weaponController.canShoot = false;
        yield return new WaitForSeconds(delay);
        _weaponController.canShoot = true;
    }    
    
    public override IEnumerator ReloadingDelay(float delay)
    {
       _weaponController.canShoot = false;
       _weaponController.canReload = false;
       yield return new WaitForSeconds(delay);
       CurrentAmmo = TotalAmmo;
       UpdateUI(CurrentAmmo);
       _weaponController.canShoot = true;
       _weaponController.canReload = true;
    }

    public override void UpdateUI(int ammoUI)
    {
        string UiString = $"{ammoUI} / {TotalAmmo}";
        _weaponController._gunAmmo.text = UiString; 
    }

    public override void SetLineRenderer(LineRenderer lineRenderer, Transform startPos, Vector3 endPos)
    {
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.lightBlue;

        lineRenderer.SetPosition(0,startPos.position);
        lineRenderer.SetPosition(1,endPos);
    }

}
