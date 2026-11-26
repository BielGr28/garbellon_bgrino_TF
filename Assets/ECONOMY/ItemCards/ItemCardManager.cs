using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardManager : MonoBehaviour
{
    [Header("ItemCard Components")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private RawImage icon;
    [SerializeField] private RawImage rarityFrame;
    [Header("References")]
    private Button interactButton;
    [SerializeField] private Sprite[] rarityPrefabs; //sprite container
    private UpgradesTEST currentUpgrade;
    private LevelXPManager levelXP;


    void Awake()
    {
        interactButton = GetComponent<Button>();
        levelXP = GameObject.Find("LevelManager").GetComponent<LevelXPManager>();
    }

    void Start()
    {
        interactButton.onClick.AddListener(levelXP.CloseLvlUpMenu);
    }

    public void Initialize(UpgradesTEST data)
    {
        currentUpgrade = data;

        title.text = data.upgradeName;
        description.text = data.upgradeDescription;
        icon.texture = data.upgradeIcon.texture;

        UpdateRarityFrame();
    }

    private void UpdateRarityFrame()
    {
        switch(currentUpgrade.upgradeRarityTEST)
        {
            case UpgradeRarityTEST.Common:
            rarityFrame.texture = rarityPrefabs[0].texture;
            break;

            case UpgradeRarityTEST.Uncommon:
            rarityFrame.texture = rarityPrefabs[1].texture;
            break;

            case UpgradeRarityTEST.Rare:
            rarityFrame.texture = rarityPrefabs[2].texture;
            break;

            case UpgradeRarityTEST.Epic:
            rarityFrame.texture = rarityPrefabs[3].texture;
            break;

            case UpgradeRarityTEST.Legendary:
            rarityFrame.texture = rarityPrefabs[4].texture;
            break;
        }
    }


}
