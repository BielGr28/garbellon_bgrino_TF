using UnityEngine;

public class DisableObjectOnTIme : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisapear;

    
    void FixedUpdate()
    {
        //Add it to loot 
        timeBeforeDisapear -= Time.deltaTime;
        if (timeBeforeDisapear <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
