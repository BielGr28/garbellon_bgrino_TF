using UnityEngine;

//Heals 5% total health.
public class Medkit : MonoBehaviour
{
    public float CalcultateHealingPoints(float maxPlayerHealth)
    {
        float healthToGive = maxPlayerHealth * 0.05f;
        return healthToGive;
    }
}
