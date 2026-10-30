using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerXP_Script : MonoBehaviour
{

    [Header("PLAYER XP")]
    public float CurrentXP = 0.0f;
    public float XPToLvlUp = 100.0f;


    [Header("PLAYER COMPONENTS")]
    private PlayerController _playerController;
    private WeaponController _weaponController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = this.GetComponent<PlayerController>();
        _weaponController = this.GetComponentInChildren<WeaponController>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"CURRENT XP: {CurrentXP} // XP TO LvL UP: {XPToLvlUp}");
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
        //Calculate xp level curve.
        XPToLvlUp = XPToLvlUp * 1.2f;
        
    }

    private void ShowUpgradeMenu()
    {
        
    }

}
