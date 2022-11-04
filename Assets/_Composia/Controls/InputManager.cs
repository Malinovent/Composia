using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class InputManager : MonoBehaviour
{
    public static ComposiaControls inputActions;

    public static event Action rebindComplete;
    public static event Action rebindCancelled;
    public static event Action<InputAction, int> rebindStarted;
    public static event Action rebindCompositeComplete;

    private void Awake()
    {
        GetControls();

        LoadAllBindingOverrides();
    }

    public static ComposiaControls GetControls()
    {
        if (inputActions == null)
        {
            inputActions = new ComposiaControls();
        }

        return inputActions;
    }

    public static void StartRebind(string actionName, int bindingIndex, TMP_Text statusText, bool excludeMouse)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.LogWarning("Couldn't find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            int firstPartIndex = bindingIndex + 1;
            Debug.Log("Rebinding a Composite! " + action.bindings[firstPartIndex].name + " " + action.bindings.Count + " " + firstPartIndex + "...IsComposite: " + action.bindings[firstPartIndex].isComposite);
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
            {
                Debug.Log("Re-Rebind Composite");
                DoRebind(action, firstPartIndex, statusText, true, excludeMouse);
            }
        }
        else
        {
            DoRebind(action, bindingIndex, statusText, false, excludeMouse);
        }
    }

    private static void DoRebind(InputAction actionToRebind, int bindingIndex, TMP_Text statusText, bool allCompositeParts, bool excludeMouse)
    {
        if (actionToRebind == null || bindingIndex < 0) { return; }
        //Debug.Log("Rebinding " + actionToRebind);
        //if (bindingIndex < actionToRebind.bindings.Count) { Debug.Log("Rebinding " + actionToRebind.bindings[bindingIndex].name + " " + allCompositeParts); }
        
        if (allCompositeParts)
        {
            statusText.text = actionToRebind.bindings[bindingIndex].name.ToUpper() + $" INPUT";
        }
        else
        {
            statusText.text = $"Press a {actionToRebind.expectedControlType}";
        }

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if (allCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
                if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isPartOfComposite)
                {
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts, excludeMouse);
                }
                else
                {
                    rebindCompositeComplete?.Invoke();
                }
            }

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
        }
        );

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCancelled?.Invoke();
        }
        );

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if (excludeMouse)
        {
            rebind.WithControlsExcluding("Mouse");
        }

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start();     //Actually starts the rebinding process
    }

    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if (inputActions == null)
        {
            inputActions = new ComposiaControls();
        }

        InputAction action = inputActions.asset.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    private static void LoadAllBindingOverrides()
    {
        foreach (var action in inputActions)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i, action.bindings[i].overridePath)))
                {
                    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i, action.bindings[i].overridePath));
                }
            }
        }
    }

    public static void LoadBindingOverride(string actionName)
    {
        Debug.Log("Loading Binding Overrides");
        if (inputActions == null)
        {
            inputActions = new ComposiaControls();
        }

        InputAction action = inputActions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i, action.bindings[i].overridePath)))
            {
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i, action.bindings[i].overridePath));
            }
        }
    }

    public static void ResetBinding(string actionName, int bindingIndex)
    {
        InputAction action = inputActions.asset.FindAction(actionName);

        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.LogWarning("Can't find the binding!");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
            {
                action.RemoveBindingOverride(i);
            }
        }
        else 
        {
            action.RemoveBindingOverride(bindingIndex);
        }
    }
}
