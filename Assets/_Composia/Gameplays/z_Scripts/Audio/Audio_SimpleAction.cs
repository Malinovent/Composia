using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_SimpleAction : MonoBehaviour
{
    //Play one audio clip with Wwise
    public List<name_SFXEvent> _nameEvents = new List<name_SFXEvent>();
    public Dictionary<string, AK.Wwise.Event> _events = new Dictionary<string, AK.Wwise.Event>();

    public void Awake()
    {
        foreach (name_SFXEvent _name in _nameEvents)
        {
            _events.Add(_name._name, _name._event);
        }
    }

    public void PlayWwiseEvent(AK.Wwise.Event _event)
    {
        _event.Post(gameObject);
    }
}

[System.Serializable]
public struct name_SFXEvent
{
    public string _name;
    public AK.Wwise.Event _event;
}
