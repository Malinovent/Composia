using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventFeedback : MonoBehaviour
{
    public UnityEvent myEvent;

    public void Activate()
    {
        myEvent?.Invoke();
    }

}
