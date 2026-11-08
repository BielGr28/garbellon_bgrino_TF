using TMPro;
using UnityEngine;

public class PLAYER_TEST_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameCoinText;
    private ResourceManager resourceManager;


    void Awake()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    void Update()
    {
        gameCoinText.text = resourceManager.TotalRunCoins().ToString();
    }
}
