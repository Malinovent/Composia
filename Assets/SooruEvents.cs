using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum SooruState
{ 
    Jump,
    Pow,
    Interact,
    Walk,
    Idle,
    Land
}

public class SooruEvents : Singleton<SooruEvents>
{
    public static event Action<SooruState> StateChange;
    public static SooruState CurrentState { get; private set; } = SooruState.Idle;

    public static bool IsGrounded = true;

    public static void OnStateChange(SooruState newState)
    {
        if(CurrentState == newState) { /*Debug.Log("This state is already happening");*/ return; }    //Exit out because this state is already playing
        CurrentState = RequestStateChange(newState);
        Debug.Log("State Change:" + CurrentState);
        StateChange?.Invoke(newState);
    }

    public static SooruState RequestStateChange(SooruState newState)
    {
        if(CurrentState == newState) { return CurrentState; } //If the state are the same, exit out and return the current one

        switch (CurrentState)
        {
            case SooruState.Jump:
                if (newState == SooruState.Walk || newState == SooruState.Idle || newState == SooruState.Interact)
                {
                    return CurrentState;
                }
                else
                {
                    return newState;
                }    
            case SooruState.Pow:
                if (IsGrounded) { return newState; }
                else return CurrentState;
            case SooruState.Idle:
            case SooruState.Walk:                
            case SooruState.Interact:
                return newState;
        }

        return newState;
    }

}
