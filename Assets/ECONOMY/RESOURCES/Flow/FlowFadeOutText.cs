using TMPro;
using UnityEngine;

public class FlowFadeOutText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fadedText;
    [SerializeField] private float fadeoutTimer;
    
    void Update()
    {
        FadeOutText();
    }
    

    private void FadeOutText()
    {
        fadeoutTimer -= Time.deltaTime;
        fadedText.CrossFadeAlpha(0.0f, fadeoutTimer, false);
        if (fadeoutTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
