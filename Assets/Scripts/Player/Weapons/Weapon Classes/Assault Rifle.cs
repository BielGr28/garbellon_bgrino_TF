using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AssaultRifle : Weapon
{

    //public override WeaponType WeaponType => WeaponType.Automatic;

    public override string Name => "Assault Rifle";

    public override float Damage { get; set; }
    public override int TotalAmmo { get; set; }
    public override int CurrentAmmo { get; set ; }
    public override float ReloadSpeed { get; set; }
    public override float FireRate { get; set; }
    public override float Precision { get; set; }
    public override WeaponController _weaponController => GetComponentInParent<WeaponController>();

    public override float MaxDamageRange { get; set; }
    public override GameObject Croshair => GameObject.FindGameObjectWithTag("CrossHair");
    public override Animator _animator => GetComponent<Animator>();

    public override Sprite Icon {set;get;}

    public Transform PSPoint;
    
    [Header("VFX ELEMENTS")]
    [SerializeField] private Sprite _rifleIcon;
    [SerializeField] private Animator _pistolAnimator;
    [SerializeField] private GameObject _shootingPS;
    [SerializeField] private GameObject _impactPS;

    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _shootingClip;
    [SerializeField] private AudioClip _reloadingClip;

   /* public override void SetDefaultStats(float damage, int totalAmmo, float reloadSpeed, float fireRate, float precision, float maxRange)
    {
        Damage = damage;
        TotalAmmo = totalAmmo;
        CurrentAmmo = TotalAmmo;
        ReloadSpeed = reloadSpeed;
        FireRate = fireRate;
        Precision = precision;
        MaxDamageRange = maxRange;
    }*/

    void Awake()
    {
        //SetDefaultStats(float damage, int totalAmmo, float reloadSpeed, float fireRate, float precision, float maxRange)
        SetDefaultStats(6.5f,40,1.5f,0.2f,10f, Range);
        Icon = _rifleIcon;
    }

    public override void Especial()
    {
        return;
    }    
    
    public override void Shoot()
    {
        if(CurrentAmmo > 0)
        {
            RaycastHit hit;
           // _audioSource.clip = _shootingClip;
            //_audioSource.Play();

            _animator.SetTrigger(AN_SHOOT);
            CroshairState(Croshair);


            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f)),out hit, Range))
            {

                SetLineRenderer(_shootingPS.GetComponent<LineRenderer>(),PSPoint,hit.point);
                GameObject bulletTrail;
                bulletTrail = GameObject.Instantiate(_shootingPS,PSPoint.position,Quaternion.identity);
                GameObject.Destroy(bulletTrail,0.2f);
                
                GameObject bulletImpact;
                bulletImpact = GameObject.Instantiate(_impactPS,hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward,-hit.normal));
                GameObject.Destroy(bulletImpact,0.15f);

                if(hit.collider.CompareTag("Enemy"))
                {
                    float distanceToTarget = Vector3.Distance(PSPoint.position,hit.point);
                    //float multiplier = _weaponController.generalUpgrades.damageMultiplier;
                    Debug.Log($"DISTANCE: {distanceToTarget} || DAMAGE: {CalculateDamage(distanceToTarget,_weaponController.generalUpgrades)}");
                    //hit.collider.GetComponent<Enemy_Health>().EnemyDamaged(Damage);
                }
            }

            CurrentAmmo -= 1;
            UpdateWeaponUI(CurrentAmmo);
            IEnumerator coroutine = ShootingDelay(FireRate);
            StartCoroutine(coroutine);
        }
        else
        {
            Debug.Log("OUT OF AMMO");
            Reload();
        }    
    }


}

