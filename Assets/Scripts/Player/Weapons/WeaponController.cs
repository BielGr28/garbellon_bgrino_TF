//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [Header("PLAYER INPUTS")]
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _shootAction;
    private InputAction _specialAction;
    private InputAction _reloadAction;
    private InputAction _switchAction;

    public bool isShooting;
    public bool canShoot;
    public bool canReload;

    public Weapon weapon;
    public WeaponsGeneralUpgrades generalUpgrades;

    public GameObject[] WeaponsList = new GameObject[2];
    public GameObject CurrentWeapon;
    public GameObject SecondWeapon;

    [Header("WEAPONS UI")]
    public TextMeshProUGUI _gunAmmo; 
    public GameObject gunCroshair;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shootAction = InputSystem.actions.FindAction("Shoot");
        _specialAction = InputSystem.actions.FindAction("Special");
        _reloadAction = InputSystem.actions.FindAction("Reload"); 
        _switchAction = InputSystem.actions.FindAction("Switch");

        CurrentWeapon = WeaponsList[0];
        SecondWeapon = WeaponsList[1];
        CurrentWeapon.SetActive(true);
        weapon = CurrentWeapon.GetComponentInChildren<Weapon>();
        gunCroshair = weapon.Croshair;
        generalUpgrades = GetComponentInParent<WeaponsGeneralUpgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon.WeaponType == WeaponType.Single)
        {
            if(_shootAction.WasPressedThisFrame() && canShoot)
            {
                weapon.Shoot();   
            }
        }
        else
        {
            if(_shootAction.IsPressed() && canShoot)
            {
                weapon.Shoot();   
            }
        }
        
        if(_reloadAction.WasPressedThisFrame() && canReload)
        {
            weapon.Reload();
        }

        if(_specialAction.WasPressedThisFrame())
        {
           weapon.Especial();
        }

        if(_switchAction.WasPressedThisFrame())
        {
            ChangeWeapons(CurrentWeapon,SecondWeapon);
        }
    }

    void FixedUpdate()
    {
        RaycastHit detectEnemy;
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f)),out detectEnemy, 100.0f))
        {
            Image[] images = gunCroshair.GetComponentsInChildren<Image>();
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


    void ChangeWeapons(GameObject current, GameObject newWeapon)
    {
        current.SetActive(false);
        newWeapon.SetActive(true);
        WeaponsList[1] = current;
        WeaponsList[0] = newWeapon;
        weapon = newWeapon.GetComponentInChildren<Weapon>();
    }


}
