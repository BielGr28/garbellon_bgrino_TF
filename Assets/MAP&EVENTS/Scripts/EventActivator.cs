using UnityEngine;

/// <summary>
/// Add it to each collider set to activate the corresponding area event
/// </summary>

public class EventActivator : MonoBehaviour
{
    [Header("Event which would be activated")]
    [SerializeField] private GameEvent gameEvent;
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if(activated)
        return;

        if(!other.CompareTag("Player") )
        return;

        if(!gameEvent.CanStartEvent)
        return;

        activated = true;
        gameEvent.StartEvent();

        GetComponent<Collider>().enabled = false;
    }
}
