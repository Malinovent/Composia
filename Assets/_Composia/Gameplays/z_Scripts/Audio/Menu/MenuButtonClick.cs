using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonClick : MonoBehaviour
{
    private Audio_SimpleAction _audio_SimpleAction { get { return GameObject.FindObjectOfType<Audio_SimpleAction>(); } }

    public string _eventSFX;

    public void ButtonClick(string _sfxName)
    {
        _audio_SimpleAction.PlayWwiseEvent(_audio_SimpleAction._events[_eventSFX]);
    }
}
