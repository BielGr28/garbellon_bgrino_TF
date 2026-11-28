using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerXP_Script : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction debugAction;
    [Header("PLAYER XP")]
    public float XPMultiplier;
    public float currentXP;

    public float XPToLvlUp;

    public int currentLevel = 0;


    [Header("PLAYER COMPONENTS")]

    [SerializeField] UpgradesManager _upgradesManager;
    [SerializeField] WeaponController _weaponController;
    [SerializeField] PlayerHealth _playerHealth;

    [Header("XP INTERFACE")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceBar;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debugAction = InputSystem.actions.FindAction("Debugger");
        //XPCurve();
        //GainXP(100.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if(debugAction.WasPressedThisFrame())
        {
            GainXP(100.0f);
        }
        //Debug.Log($"CURRENT XP: {CurrentXP} // XP TO LvL UP: {XPToLvlUp}");
       
    }

    public void GainXP(float xpToGet)
    {
        currentXP += xpToGet;
        if(currentXP >= XPToLvlUp)
        {
            LevelUp();
        }
        //UpdateInterface();
    }


    private void LevelUp()
    {
        UpdateXPValues();
        IncreaseHealthStats();

        //_upgradesManager.ShowUpgradesUI();
        _upgradesManager.ShowUpgradesUI();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _weaponController.canShoot = false;
        Time.timeScale = 0.0f;
    }

    private void IncreaseHealthStats()
    {
        float healthMultiplier = 1.05f;

        float currentHealthPercentage = _playerHealth.currentHealth / _playerHealth.maxHealth;

        _playerHealth.maxHealth *= healthMultiplier;
        _playerHealth.currentHealth = _playerHealth.maxHealth * currentHealthPercentage;
        _playerHealth.UpdateUI();
    }

    void UpdateXPValues()
    {
        currentLevel++;
        XPToLvlUp *= XPMultiplier;
        float calculatedXp = XPToLvlUp - currentXP;
        currentXP = calculatedXp;

        //UpdateInterface();
    }

    /*void UpdateInterface()
    {
        levelText.text = $"{currentLevel}";
        experienceText.text =  $"{currentXP} / {XPToLvlUp}";
        experienceBar.fillAmount = currentXP/XPToLvlUp;
    }*/
}
