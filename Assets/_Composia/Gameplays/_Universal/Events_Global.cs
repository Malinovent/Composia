using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Events_Global
{
    public static event Action<int> GetCurrency;

    public static event Action<NoteObject, int> GetNote;

    public static void OnGetCurrency(int points)
    {
        GetCurrency?.Invoke(points);
    }

    public static void OnGetNote(NoteObject item, int amount = 1)
    {
        GetNote?.Invoke(item, amount);
    }
}
