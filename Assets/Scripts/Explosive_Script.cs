using StarterAssets;
using UnityEngine;

public class Explosive_Script : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        FirstPersonController controller = collision.gameObject.GetComponent<FirstPersonController>();
        if(controller != null)
        {
            controller._verticalVelocity =  Mathf.Sqrt(controller.JumpHeight * -2f * controller.Gravity);
        }
        else
        {
            Debug.LogWarning("no controller detected");
        }
        //collision.gameObject.GetComponent<FirstPersonController>()._verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
    }
}
