using UnityEngine;

[CreateAssetMenu(fileName = "Weapon In Shop", menuName ="Weapon/Buy Weapon")]
public class BuyWeapon : Upgrades
{
    public GameObject weaponToGet;
    public override void UpgradeAllWeapons(WeaponController weaponController)
    {
        Transform weponSlot;
        //Firts check if player has 1 or 2 weaopons.
        if(weaponController.SecondWeapon.GetComponentInChildren<Weapon>(true) != null)
        {
            
            //Save current weapon object.
            Transform[] objects = weaponController.WeaponsList[0].GetComponentsInChildren<Transform>();            

            //Instantiate The New Weapon
            weponSlot = weaponController.CurrentWeapon.transform;
            GameObject instant = GameObject.Instantiate(weaponToGet,weponSlot.position,weponSlot.rotation,weponSlot);

            //Remove current weapon
            Destroy(objects[1].gameObject);

            //Set new weapon.
            weaponController.weapon = instant.GetComponentInChildren<Weapon>();
            weaponController.UpdateUI();
            
        }
        else
        {   
            //weaponController.WeaponsList[1] = weaponToGet;
            weponSlot = weaponController.SecondWeapon.transform;
            //If player only has one weapon we spawn the new weapon in the second slot.
            GameObject.Instantiate(weaponToGet,weponSlot.position,weponSlot.rotation,weponSlot);
            weaponController.SecondWeapon.SetActive(true);
            weaponController.UpdateUI();
            weaponController.SecondWeapon.SetActive(false);  
        }
        
    }

    public override void UpgradeCurrentWeapon(Weapon upgradeWeapon)
    {}

    public override void UpgradePlayer(PlayerController playerController)
    {}
}
