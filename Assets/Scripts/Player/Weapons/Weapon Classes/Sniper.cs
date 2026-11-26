using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Sniper : Weapon
{
    public override WeaponType WeaponType => WeaponType.SNIPER_RIFLE;

    public override string WeaponName => "Sniper Rifle";

    public override float Damage { get; set; }
    public override int TotalAmmo { get; set; }
    public override int CurrentAmmo { get; set ; }
    public override float ReloadSpeed { get; set; }
    public override float FireRate { get; set; }
    public override float Precision { get; set; }
    public override float MaxDamageRange { get; set; }
    public override WeaponController _weaponController => GetComponentInParent<WeaponController>();
    public override GameObject Croshair => GameObject.FindGameObjectWithTag("CrossHair");
    public override Animator _animator => GetComponent<Animator>();

    public override Sprite Icon {set;get;}

    public Transform PSPoint;
    //[SerializeField] private GameObject _sniperCroshair;
    [SerializeField] CinemachineCamera _camera;
    public float zoomValue = 35.0f;
    public float normalFOV = 70.0f;

    [Header("VFX ELEMENTS")]
    [SerializeField] private Sprite _sniperIcon;
    [SerializeField] private GameObject _sniperCroshair;
    [SerializeField] private Animator _pistolAnimator;
    [SerializeField] private GameObject _shootingPS;
    [SerializeField] private GameObject _impactPS;

    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _shootingClip;
    [SerializeField] private AudioClip _reloadingClip;

    /*public override void SetDefaultStats(float damage,int totalAmmo, float reloadSpeed, float fireRate, float precision, float maxRange)
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
        SetDefaultStats(20.0f,5,1.5f,1.0f,10f, Range);
        Icon = _sniperIcon;
        Debug.Log("ICOOON")
;    }

    public override void Shoot()
    {
        if(CurrentAmmo > 0)
        {
            RaycastHit hit;

           // _audioSource.clip = _shootingClip;
           // _audioSource.Play();

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

                if(hit.collider.gameObject.CompareTag("Enemy"))
                {
                    float distanceToTarget = Vector3.Distance(PSPoint.position,hit.point);
                    //float multiplier = _weaponController.generalUpgrades.damageMultiplier;
                    Debug.Log($"DISTANCE: {distanceToTarget} || DAMAGE: {CalculateDamage(distanceToTarget,_weaponController.generalUpgrades)}");
                    
                    //hit.collider.GetComponent<Enemy_Health>().EnemyDamaged(Damage);
                }
                //Debug.Log(Vector3.Distance(PSPoint.position,hit.point));
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
    public override void Special()
    {
        
       _sniperCroshair.SetActive(!_sniperCroshair.activeSelf);
       MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
       foreach (var render in renderers)
       {
         render.enabled = !render.enabled;
       }
       if(_camera.Lens.FieldOfView > zoomValue)
       {
           _camera.Lens.FieldOfView = zoomValue;
       }
       else
        {
            _camera.Lens.FieldOfView = normalFOV;
        }
       
    }


}
