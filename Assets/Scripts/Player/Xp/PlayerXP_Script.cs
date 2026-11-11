using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerXP_Script : MonoBehaviour
{

    [Header("PLAYER XP")]
    [SerializeField] AnimationCurve experienceCurve;
    public float XPMultiplier;
    public float CurrentXP;

    public float XPToLvlUp;

    public int currentLevel = 0;


    [Header("PLAYER COMPONENTS")]

    [SerializeField] UpgradesManager _upgradesManager;
    [SerializeField] WeaponController _weaponController;

    [Header("XP INTERFACE")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;

    //[SerializeField] Image experienceFill;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //XPCurve();
        //GainXP(100.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"CURRENT XP: {CurrentXP} // XP TO LvL UP: {XPToLvlUp}");
       
    }

    public void GainXP(float xpToGet)
    {
        CurrentXP += xpToGet;
        if(CurrentXP >= XPToLvlUp)
        {
            LevelUp();
        }
    }


    private void LevelUp()
    {
        currentLevel++;

        XPToLvlUp *= XPMultiplier;
        _upgradesManager.ShowUpgradesUI();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _weaponController.canShoot = false;
        Time.timeScale = 0.0f;
    }

    void XPCurve()
    {
        for (var i = 0; i <= 10; i++)
        {
            Debug.Log($"{XPToLvlUp} || {i}");
            XPToLvlUp *= XPMultiplier;
        }
    }

    void UpdateInterface()
    {
        
    }

    private void ShowUpgradeMenu()
    {
        
    }

}
