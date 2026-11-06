using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject[] WeaponsList;
    public GameObject CurrentWeapon;

    [Header("WEAPONS UI")]
    public TextMeshProUGUI _gunAmmo; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shootAction = InputSystem.actions.FindAction("Shoot");
        _specialAction = InputSystem.actions.FindAction("Special");
        _reloadAction = InputSystem.actions.FindAction("Reload"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(_shootAction.WasPressedThisFrame())
        {
            CurrentWeapon.GetComponent<WeaponGeneral_Script>().Shoot();
            Debug.Log("SHOOOT");//CurrentWeapon.
        }

        if(_reloadAction.WasPerformedThisFrame())
        {
            CurrentWeapon.GetComponent<WeaponGeneral_Script>().Reload();
            Debug.Log("RELOAAAD");
        }
    }

    void ChangeWeapons(GameObject current, GameObject newWeapon)
    {
        current.SetActive(false);
        newWeapon.SetActive(true);
    }


}
