using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Composia/Create Game Event", order = 0)]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();
    public void Raise()
    {
        Debug.Log(this.name + " event triggered!");
        foreach (GameEventListener l in listeners)
        {
            l.OnEventRaised();
        }
    }
    public void RegisterListener(GameEventListener gameEvent)
    {
        listeners.Add(gameEvent);
    }

    public void UnRegisterListener(GameEventListener gameEvent)
    {
        listeners.Remove(gameEvent);
    }
}
