using UnityEngine;

//Gives shield (20% max health)
public class Shield : MonoBehaviour
{
    public float CalcultateShieldPoints(float maxPlayerHealth)
    {
        float shieldToGive = maxPlayerHealth * 0.2f;
        return shieldToGive;
    }
}
