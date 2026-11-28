using System.Collections.Generic;
using UnityEngine;


public class ActivateShop : MonoBehaviour
{
    public ShopManager shopManager;

    public void SetShopItems()
    {
        shopManager.SetShopItems();
        shopManager.ShowShopUI();        
        Destroy(this.gameObject);
    }    
}
