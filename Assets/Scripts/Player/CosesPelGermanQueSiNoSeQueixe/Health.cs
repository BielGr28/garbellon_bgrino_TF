using System.Xml.Serialization;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentHealth;
    public float maxHealth;

    public void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void Die()
    {
        
    }
}
