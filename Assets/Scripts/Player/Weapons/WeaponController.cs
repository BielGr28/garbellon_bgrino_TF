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
    public WeaponGeneralUpgrades generalUpgrades;

    public GameObject[] WeaponsList = new GameObject[2];
    public GameObject CurrentWeapon;
    public GameObject SecondWeapon;

    [Header("WEAPONS UI")]
    public TextMeshProUGUI _gunAmmo; 
    public GameObject gunCroshair;
    public Image currentIcon;
    public Image secondIcon;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shootAction = InputSystem.actions.FindAction("Shoot");
        _specialAction = InputSystem.actions.FindAction("Special");
        _reloadAction = InputSystem.actions.FindAction("Reload"); 
        _switchAction = InputSystem.actions.FindAction("Switch");
        generalUpgrades = GetComponent<WeaponGeneralUpgrades>();

        CurrentWeapon = WeaponsList[0];
        SecondWeapon = WeaponsList[1];

        //Goofy sequence and must surley not optimal at all. But does do job.
        CurrentWeapon.SetActive(true);
        SecondWeapon.SetActive(true);

        weapon = CurrentWeapon.GetComponentInChildren<Weapon>();
        gunCroshair = weapon.Croshair;

        UpdateAllWeaponsStats();

        UpdateUI();

        SecondWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_shootAction.IsPressed() && canShoot)
        {
            weapon.Shoot();   
        }
        
        if(_reloadAction.WasPressedThisFrame() && canReload)
        {
            weapon.Reload();
        }

        if(_specialAction.WasPressedThisFrame())
        {
           weapon.Special();
        }

        if(_switchAction.WasPressedThisFrame() && WeaponsList[1].GetComponentInChildren<Weapon>() != null)
        {
            ChangeWeapons(CurrentWeapon,SecondWeapon);
        }
    }

    void ChangeWeapons(GameObject current, GameObject newWeapon)
    {
        current.SetActive(false);
        newWeapon.SetActive(true);

        WeaponsList[0] = newWeapon;
        WeaponsList[1] = current;

        CurrentWeapon = WeaponsList[0];
        SecondWeapon = WeaponsList[1];

        weapon = newWeapon.GetComponentInChildren<Weapon>();

        UpdateUI();
        weapon.UpdateWeaponUI(weapon.CurrentAmmo);
        canShoot = true;
        canReload = true;
    }

    public void UpdateUI()
    {
        currentIcon.sprite = weapon.Icon;
        if(WeaponsList[1].GetComponentInChildren<Weapon>() != null)
        { 
            secondIcon.GetComponentInParent<RawImage>().enabled = true;
            secondIcon.enabled = true;
            secondIcon.sprite = WeaponsList[1].GetComponentInChildren<Weapon>().Icon;
        }
        else
        {
            secondIcon.GetComponentInParent<RawImage>().enabled = false;
            secondIcon.enabled = false;
        }

    }

    public void UpdateAllWeaponsStats()
    {
        for (var i = 0; i < WeaponsList.Length; i++)
        {
            if(WeaponsList[i].GetComponentInChildren<Weapon>() != null)
            {
                Debug.Log("Enter Upgrade");
                Weapon currentWeapon = WeaponsList[i].GetComponentInChildren<Weapon>();
                generalUpgrades.AppplyMultipliers(currentWeapon);
            }
            else
            {
                Debug.Log("Cant Upgrade");
            }
           
        }
        weapon.UpdateWeaponUI(weapon.CurrentAmmo);
    }

}
