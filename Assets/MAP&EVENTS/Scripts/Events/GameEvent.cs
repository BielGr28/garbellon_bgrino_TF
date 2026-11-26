using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public string eventName;
    [SerializeField] private TextMeshProUGUI currentEventText;
    public bool CanStartEvent {get; set;}
    public bool IsCompleted { get; protected set; }
    public event Action<GameEvent> OnEventCompleted;
    [SerializeField] protected EventArea eventArea;


    public virtual void StartEvent()
    {
        if (!CanStartEvent)
        return;

        CanStartEvent = false;
        IsCompleted = false;

        eventArea?.LockArea();

        currentEventText.gameObject.SetActive(true);
        currentEventText.text = eventName;
    }

    protected virtual void CompleteEvent()
    {
        if (IsCompleted)
        return;

        eventArea?.UnlockArea();

        IsCompleted = true;
        OnEventCompleted?.Invoke(this);
    }
}
