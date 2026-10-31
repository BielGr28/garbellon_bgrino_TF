using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPSO_Holder : MonoBehaviour
{
    public Upgrades upgrade;
    public TextMeshProUGUI upgradeTitle;
    public TextMeshProUGUI upgradeDescription;
    public Sprite upgradeIcon; 

    public void DebugUpgrade()
    {
        Debug.Log(upgrade.upgradeType);
    }

}
