using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour, IChangeMode
{
    public Controls[] controls;

    void Awake()
    {
        GameModeManager.ChangeMode += ChangeMode;
    }

    public void ChangeMode(GameMode mode)
    {
        foreach (Controls c in controls)
        {
            if (c.Mode == mode)
            {
                c.EnableControls();
            }
            else
            {
                c.DisableControls();
            }
        }
    }
}
