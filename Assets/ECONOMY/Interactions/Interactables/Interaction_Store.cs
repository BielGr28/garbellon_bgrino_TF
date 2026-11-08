using TMPro;
using UnityEngine;

public class Interaction_Store : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name  + "/ prompt mes: " + promptMessage);
    }
}
