using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages current event
/// </summary>

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private List<GameEvent> events;
    private int currentEventIndex = -1;


    private void Start()
    {
        foreach (GameEvent gameEvent in events)
        {
            gameEvent.OnEventCompleted += HandleEventCompleted;
        }

        StartNextEvent();
    }

    private void StartNextEvent()
    {
        currentEventIndex++;

        if (currentEventIndex >= events.Count)
        {
            GameCompleted();
            return;
        }

        events[currentEventIndex].CanStartEvent = true;
    }

    private void HandleEventCompleted(GameEvent completedEvent)
    {
        completedEvent.OnEventCompleted -= HandleEventCompleted;

        StartNextEvent();
    }

    private void GameCompleted()
    {
        Debug.Log("PARTIDA COMPLETADA");
    }
}