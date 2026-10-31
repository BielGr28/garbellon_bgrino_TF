using UnityEngine;

public class Xp_Object : MonoBehaviour
{
    public float XPToGive = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.GetComponent<PlayerXP_Script>() != null)
        {
            Debug.Log("XP GAIN");
            collision.gameObject.GetComponent<PlayerXP_Script>().GainXP(XPToGive);
        }
        else
        {
            Debug.Log("CANT GAIN XP");
        }

    }
}
