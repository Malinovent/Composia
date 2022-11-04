using UnityEngine;
using UnityEngine.Events;

public class CollisionEventManager : MonoBehaviour
{
    public UnityEvent eventAction;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On Collision detected with " + collision.collider.name);
        InvokeAction();
    }

    public void InvokeAction()
    {
        eventAction.Invoke();
    }
}
