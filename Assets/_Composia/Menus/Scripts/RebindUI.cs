using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference action = null;
    [SerializeField] private bool excludeMouse = true;
    [SerializeField] InputBinding.DisplayStringOptions displayStringOptions;
    [SerializeField] [Range(0, 10)] private int selectedBinding;

    [Header("UI FIELDS")]
    [SerializeField] private Button rebindButton = null;
    [SerializeField] private Button resetButton = null;
    [SerializeField] private TMP_Text actionText = null;
    [SerializeField] private TMP_Text rebindText = null;

    [Header("BINDING INFO - DO NOT EDIT")]
    public InputBinding inputBinding;

    private int bindingIndex;
    private string actionName;
    [SerializeField] private bool isComposite = false;

    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());
        resetButton.onClick.AddListener(() => ResetBinding());

        if (action != null)
        {
            InputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        if (isComposite) { InputManager.rebindCompositeComplete += UpdateUI; }
        else { InputManager.rebindComplete += UpdateUI; }

        InputManager.rebindCancelled += UpdateUI;
    }

    private void OnDisable()
    {
        InputManager.rebindComplete -= UpdateUI;
        InputManager.rebindCancelled -= UpdateUI;
    }

    private void OnValidate()
    {
        if(action == null) { return; }
        GetBindingInfo();
        UpdateUI();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (action.action != null)
        {
            actionName = action.action.name;
           // Debug.Log(actionM)
        }

        if (action.action.bindings.Count > selectedBinding)
        {
            inputBinding = action.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    private void UpdateUI()
    {
        if (actionText != null)
        {
            actionText.text = actionName;
        }
        
        if(rebindText != null)
        {
           
            if(Application.isPlaying)
            {
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
            }
            else
            {
                rebindText.text = action.action.GetBindingDisplayString(bindingIndex);
            }
        }     
    }

    private void DoRebind()
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }

    private void ResetBinding()
    {
        InputManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }


        /*
    public void StartRebinding()
    {
        Debug.Log("Starting rebind");
        startRebingingObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        input.SwitchCurrentActionMap("Menu");

        rebindingOperation = action.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        int bindingIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);

        actionText.text = InputControlPath.ToHumanReadableString(action.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        rebindingOperation.Dispose();

        startRebingingObject.SetActive(true);
        waitingForInputObject.SetActive(false);       
    }*/
}
