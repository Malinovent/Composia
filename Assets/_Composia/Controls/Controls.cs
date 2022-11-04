using UnityEngine;

public abstract class Controls : MonoBehaviour
{
    protected ComposiaControls controls;

    [SerializeField]
    protected GameMode mode;

    public GameMode Mode { get => mode;}

    private void Awake()
    {
        if (controls == null)
        {
            controls = InputManager.GetControls();
        }
    }

    private void OnEnable()
    {
        GetControls();
        InitializeControls();
    }

    private void OnDisable()
    {
        UnInitializeControls();
    }

    private void GetControls()
    {

    }

    public abstract void DisableControls();

    public abstract void EnableControls();

    public abstract void InitializeControls();

    public abstract void UnInitializeControls();


}
