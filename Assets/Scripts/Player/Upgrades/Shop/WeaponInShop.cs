using System.Net;
using UnityEngine;

[CreateAssetMenu(fileName = "Wepon In Shop", menuName ="Weapon/Buy Weapon")]
public class WeaponInShop : ScriptableObject
{
    public GameObject weponToGet;
    public Sprite weaponSprite;
    public int shopCost;

    public virtual void PickUpWeapon(WeaponController weaponController)
    {
        Transform weponSlot;
        //Firts check if player has 1 or 2 weaopons.
        if(weaponController.SecondWeapon.GetComponentInChildren<Weapon>() != null)
        {
            //If player has 2 weapons we change the current weapon.
            weaponController.WeaponsList[0] = weponToGet;
            weaponController.CurrentWeapon = weaponController.WeaponsList[0];
            weaponController.weapon = weaponController.CurrentWeapon.GetComponentInChildren<Weapon>();
            weaponController.UpdateUI();
            weaponController.weapon.UpdateWeaponUI(weaponController.weapon.CurrentAmmo);
            weaponController.canShoot = true;
            weaponController.canReload = true;
        }
        else
        {   
            weponSlot = weaponController.SecondWeapon.transform;
            //If player only has one weapon we spawn the second one in the second slot.
            GameObject.Instantiate(weponToGet,weponSlot.position,weponSlot.rotation,weponSlot);
            weaponController.UpdateUI();
            
        }
        
    }

}
