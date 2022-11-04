using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivatableObject : MonoBehaviour, IActivatable
{
    protected bool isActivated = false;

    [SerializeField]
    protected bool staysActive = false;

    public bool IsActivated { get => isActivated; set => isActivated = value; }
    public bool StaysActive { get => staysActive; set => staysActive = value; }


    public event EventHandler<EventArgs> Activation;
    public event EventHandler<EventArgs> Deactivation;
    public event EventHandler<EventArgs> Selected;

    public virtual void IsSelected()
    {
        OnSelected();
    }
    public virtual bool TryActivate()
    {
        Activate();
        return IsActivated;
    }

    public virtual void Activate()
    {
        IsActivated = true;
        OnActivation();
    }

    public virtual void DeActivate()
    {
        if (!StaysActive)
        {
            IsActivated = false;
            OnDeactivation();
        }
    }
    public void Restart()
    {
        IsActivated = false;
    }


    #region EVENTS
    public void OnActivation()
    {
        if (Activation != null) { Activation(this, new EventArgs()); }
    }

    public void OnDeactivation()
    {
        if (Deactivation != null) { Deactivation(this, new EventArgs()); }
    }

    public void OnSelected()
    {
        if (Selected != null) { Selected(this, new EventArgs()); }
    }
    #endregion

}
