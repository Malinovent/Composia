using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private float delay = 0;

    [SerializeField]
    private GameEvent Event;

    [SerializeField]
    private UnityEvent response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised()
    {
        if(delay == 0) { response.Invoke(); }
        else { StartCoroutine(EventRaised()); }
    }

    IEnumerator EventRaised()
    {
        yield return new WaitForSeconds(delay);
        response.Invoke();
    }
}
