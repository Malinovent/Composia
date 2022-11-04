using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventResponder : GenericResponder
{
    /// Listen to only this game object. If its null listen to all.
    public GameObject sender;

    /// The name of the type to listen to (for example Enemy or Character).
    public string typeName;

    /// The name of the event to listen to.
    public string eventName;

    public EventResponse[] actions;

    public AnimationState animationStateFilter;

    public string stringFilter;


    /// If the event is an integer event, or an item event with an amount.
    public int intFilter;

    protected System.Reflection.EventInfo eventInfo;

    protected System.Delegate handler;

    protected Component sendingComponent;

    protected float handleTimer;

    public AnimatorControllerParameter parameter1;

    #region Unity hooks
    void OnEnable()
    {
        AddHandler();
    }

    void OnDisable()
    {
        RemoveHandler();
    }

    #endregion

    virtual protected void AddHandler()
    {
        // Used cached info
        if (eventInfo != null && handler != null && sendingComponent != null)
        {
            eventInfo.AddEventHandler(sendingComponent, handler);
        }
        else
        {
            // Dynamically add event listener
            sendingComponent = sender.GetComponent(typeName);
            System.Type type = typeof(EventManager).Assembly.GetType(typeName);
            if (type == null) type = typeof(EventManager).Assembly.GetTypes().Where(t => t.Name == typeName).FirstOrDefault();

            if (type != null && sendingComponent != null)
            {
                eventInfo = type.GetEvent(eventName);

                if (eventInfo != null)
                {
                    System.Reflection.MethodInfo handleMethod = this.GetType().GetMethod("HandleEvent", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    handler = System.Delegate.CreateDelegate(eventInfo.EventHandlerType, this, handleMethod);
                    eventInfo.AddEventHandler(sendingComponent, handler);
                }
            }
        }
    }
    virtual protected void RemoveHandler()
    {
        // Remove listeners
        if (eventInfo != null && handler != null && sendingComponent != null)
        {
            eventInfo.RemoveEventHandler(sendingComponent, handler);
        }
    }
    virtual protected void HandleEvent(object sender, System.EventArgs args)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            //if (ApplyFilters(actions[i], args))
            //{
            if (actions[i].delay == 0.0f) { DoImmediateAction(actions[i], args); }
            else { StartCoroutine(DoDelayedAction(actions[i], args)); }
            //}
        }
    }

    /// <summary>
    /// Do the action
    /// </summary>
    /// <param name="args">Event arguments.</param>
    /// <param name="action">Action.</param>
    override protected void DoImmediateAction(EventResponse action, System.EventArgs args)
    {
        base.DoImmediateAction(action, args);
    }

}

