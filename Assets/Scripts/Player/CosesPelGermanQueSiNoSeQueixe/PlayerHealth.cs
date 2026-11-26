using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{   
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _debugAction;
    private PlayerController _playerController;
    private PlayerXP_Script _playerXP;
    private WeaponController _weaponController;
    private CameraController _cameraController;
    [SerializeField] GameObject DeathPanel;

    [Header("PLAYER HEALTH ELEMENTS")]
    public float currentHealth;
    public float maxHealth;
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    [Header("PLAYER SHIELD ELEMENTS")]
    public float currentShield;
    [SerializeField] Image shieldBar;
    [SerializeField] TextMeshProUGUI shieldText;


    private void Awake() 
    {
        Time.timeScale = 1.0f;
        _debugAction = InputSystem.actions.FindAction("Debugger");
        _playerController = GetComponent<PlayerController>();
        _playerXP = GetComponent<PlayerXP_Script>();
        _cameraController = GetComponent<CameraController>();
        _weaponController = GetComponentInChildren<WeaponController>();
        UpdateUI();
    } 
    void Update()
    {
        if(_debugAction.WasPressedThisFrame())
        {
            TakeDamage(maxHealth);
        }

    }
    public void TakeDamage(float damage)
    {   
        if(currentShield > 0.0f)
        {
            currentShield -= damage;
            if(currentShield <= 0.0f)
            {
                currentHealth -= Mathf.Abs(currentShield);
            }
        }
        else
        {
            if(damage >= currentHealth)
            {
                currentHealth = 0.0f;
                Die();
            }
            else
            {
                currentHealth -= damage;
            }
        }
        UpdateUI();
        
    }

    public void GainHealth(float healthToGain)
    {
        float totalHealthSum = currentHealth + healthToGain;
        if(totalHealthSum >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = totalHealthSum;
        }
        UpdateUI();
    }

    public void GainShield(float shieldToGain)
    {
        currentShield += shieldToGain;
        UpdateUI();
    }

    public void UpdateUI()
    {
        string healthString = $"{Mathf.RoundToInt(currentHealth)} / {Mathf.RoundToInt(maxHealth)}";
        healthText.text = healthString;
        healthBar.color = HealthBarColor(currentHealth);
        healthBar.fillAmount = currentHealth/maxHealth;

        if(currentShield > 0.0f)
        {
            string shieldString = $"{Mathf.RoundToInt(currentShield)}";
            shieldBar.enabled = true;
            shieldText.enabled = true;
            shieldText.text = shieldString;
        }
        else
        {
            shieldText.enabled = false;
            shieldBar.enabled = false;
        }
    }

    private Color HealthBarColor(float current)
    {
        //If health is above 75% return Green.
        if(current >= (maxHealth * 0.75f))
        {
            return Color.green;
        }
        //If health is above 15% return Orange.
        else if(current >= (maxHealth * 0.15f))
        {
            return Color.orange;
        }
        //If health is below 15% return Red.
        else
        {
            return Color.red;
        }
    }

    public void Die()
    {
        
        DeathPanel.SetActive(true);
        _cameraController.enabled = false;
        _playerController.enabled = false;
        _playerXP.enabled = false;
        _weaponController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 0.0f;

    }


    void OnTriggerEnter(Collider other)
    {   
        string tag = other.gameObject.tag;

        switch(tag)
        {
            case "Medkit":
                float heal = other.gameObject.GetComponent<Medkit>().CalcultateHealingPoints(maxHealth);
                GainHealth(heal);
                GameObject.Destroy(other.gameObject);
            break;

            case "Shield":
                float shield = other.gameObject.GetComponent<Shield>().CalcultateShieldPoints(maxHealth);
                GainShield(shield);
                GameObject.Destroy(other.gameObject);
            break;
        }
    }
}
