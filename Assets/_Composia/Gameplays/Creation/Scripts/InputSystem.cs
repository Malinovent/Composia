using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputSystem : Singleton<InputSystem>, IObservable
{

    //[SerializeField]
    public ComposiaControls controls;

    private bool _isKeyboard = true;
    public bool IsKeyboard { get => _isKeyboard; set => _isKeyboard = value; }

    private List<IObserver> _observers = new List<IObserver>();
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Awake()
    {
        controls = new ComposiaControls();

        InitializeControls();  
    }
    void Update()
    {
        DetectKeyboardAndMouse();
        DetectGamepad();
    }

    public abstract void InitializeControls();

    public void DetectKeyboardAndMouse()
    {
        if (!IsKeyboard && (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
        {
            IsKeyboard = true;
            NotifyObservers();
            Cursor.visible = true;
            Debug.Log("Switching to keyboard and mouse");
        }
    }    
    public void DetectGamepad()
    {
        if (Gamepad.current != null)
        {
            if (IsKeyboard && Gamepad.current.IsActuated())
            {
                Cursor.visible = false;
                IsKeyboard = false;
                NotifyObservers();
                Debug.Log("Switching to gamepad");
            }
        }
    }
    public void AddObserver(IObserver o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver o)
    {
        _observers.Remove(o);
    }
    public void NotifyObservers()
    {
        foreach (IObserver o in _observers)
        {
            o.UpdateData(this);
        }
    }

  
}
