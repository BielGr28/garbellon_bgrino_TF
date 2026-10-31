using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


/*public enum AmmoType
{
    Normal
}*/
public class WeaponGeneral_Script : MonoBehaviour
{
    public  float Damage = 5.0f;
    private float CritChance = .05f;
    public  int MagazineAmmo= 20;
    private int CurrentAmmo = 20;
    public  float ReloadSpeed = 1.0f;
    public  float Precision = 10f;

    public  float FireRate = .7f;

    public  string Type = "Single";


    public LayerMask ignoreLayer;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _reloadClip;

    [SerializeField] private AudioClip _shootClip;

    [SerializeField] private GameObject _shootingPS;

    public Transform PSPoint;

    private WeaponController _weaponController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _weaponController = GetComponentInParent<WeaponController>();
       //_audioSource.GetComponent<AudioSource>();
    }


    public void CroshairState(GameObject croshair)
    {
        throw new System.NotImplementedException();
    }   

    /*public  void Especial() 
    {
        throw new System.NotImplementedException();
    }*/

    public  void Reload()
    {
       //_audioSource.clip = _reloadClip;
       //_audioSource.Play();
       CurrentAmmo = MagazineAmmo;
       UpdateAmmoUI(CurrentAmmo);
       IEnumerator coroutine = ShootingDelay(ReloadSpeed);
       StartCoroutine("ShootingDelay");
       
    }

    public void Shoot()
    {
        if(CurrentAmmo > 0)
        {
            RaycastHit hit;
            //_shootingPS.GetComponent<ParticleSystem>().Play();
            //_audioSource.clip = _shootClip;
            //_audioSource.Play();

            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f)),out hit, 100.0f))
            {
                SetLineRendererComponents(_shootingPS.GetComponent<LineRenderer>(),PSPoint,hit.point);
                GameObject bulletTrail;
                bulletTrail = GameObject.Instantiate(_shootingPS,PSPoint.position,Quaternion.identity);
                GameObject.Destroy(bulletTrail,0.2f);
                

                if(hit.collider.CompareTag("Enemy"))
                {
                    //hit.collider.GetComponent<Enemy_Health>().EnemyDamaged(Damage);
                }
            }

            CurrentAmmo -= 1;
            UpdateAmmoUI(CurrentAmmo);
            IEnumerator coroutine = ShootingDelay(FireRate);
            StartCoroutine(coroutine);
        }
        else
        {
            Debug.Log("OUT OF AMMO");
            Reload();
        }    
    }

    public IEnumerator ShootingDelay(float delay)
    {
       GetComponentInParent<WeaponController>().canShoot = false;
       yield return new WaitForSeconds(delay);
       GetComponentInParent<WeaponController>().canShoot = true;
    }

    private void UpdateAmmoUI(int currentAmmoUI)
    {
        string UiString = $"{currentAmmoUI} / {MagazineAmmo}";
        _weaponController._gunAmmo.text = UiString; 
    }

    private void SetLineRendererComponents(LineRenderer lineRenderer, Transform startPos, Vector3 endPos)
    {
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.lightBlue;

        lineRenderer.SetPosition(0,startPos.position);
        lineRenderer.SetPosition(1,endPos);
        
    }
}
