using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage; //message displayed on interaction

    public void BaseInteract() //!MUST BE CALLED FROM PLAYER!
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //override with specific interaction logic
    }
}
