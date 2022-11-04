using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Condition", menuName = "Composia/Create Condition")]
public class Condition : ScriptableObject
{
    EventHandler<EventArgs> ConditionMet;

    private bool m_isConditionMet = false;

    public bool IsConditionMet { get => m_isConditionMet; set => m_isConditionMet = value; }

    public void SetCondition(bool state)
    {
        IsConditionMet = state;
    }

    public void OnConditionMet()
    {
        if (ConditionMet != null) { ConditionMet(this, new EventArgs()); }
    }
}
