using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteTracker : Singleton<NoteTracker>
{
    public event EventHandler<GenericEventArgs> GainNote;

    public event EventHandler<GenericEventArgs> SixteenthNote;
    public event EventHandler<GenericEventArgs> EighthNote;
    public event EventHandler<GenericEventArgs> QuarterNote;
    public event EventHandler<GenericEventArgs> HalfNote;
    public event EventHandler<GenericEventArgs> WholeNote;

    public InventoryItem_Note[] inventoryNotes;
    private void Awake()
    {
        Events_Global.GetNote += GetNote;
    }

    public void GetNote(NoteObject note, int amount)
    {
        bool noteCheck = false;
        for ( int i = 0; i < inventoryNotes.Length; i++)
        {
            if (note == inventoryNotes[i].GetNoteObject())
            {
                inventoryNotes[i].SetAmount(amount);
                noteCheck = true;
                OnGainNotes(inventoryNotes[i].GetNoteObject().noteType, inventoryNotes[i].GetAmount());
                break;
            }
        }

        if (!noteCheck) { Debug.LogWarning("WARNING: This noteType does not exist in the NoteTracker component. Please Set it up");}

        
    }

    public void OnGainNotes(int amount)
    {
        if (GainNote != null) { GainNote(this, new GenericEventArgs(amount)); }
    }

    public void OnGainNotes(NoteTypeEnum noteType, int amount)
    {
        OnGainNotes(amount);
        switch (noteType)
        {
            case NoteTypeEnum.SixteenthNote:
                if (SixteenthNote != null) { SixteenthNote(this, new GenericEventArgs(amount)); }
                break;
            case NoteTypeEnum.EighthNote:
                if (EighthNote != null) { EighthNote(this, new GenericEventArgs(amount)); }
                break;
            case NoteTypeEnum.QuarterNote:
                if (QuarterNote != null) { QuarterNote(this, new GenericEventArgs(amount)); }
                break;
            case NoteTypeEnum.HalfNote:
                if (HalfNote != null) { HalfNote(this, new GenericEventArgs(amount)); }
                break;
            case NoteTypeEnum.WholeNote:
                if (WholeNote != null) { WholeNote(this, new GenericEventArgs(amount)); }
                break;
        }
    }

    public InventoryItem_Note[] ReturnNotes()
    {
        return inventoryNotes;
    }
}
