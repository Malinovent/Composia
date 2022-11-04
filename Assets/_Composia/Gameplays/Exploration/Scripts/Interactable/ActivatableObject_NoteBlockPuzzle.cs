using System;
using UnityEngine;
public class ActivatableObject_NoteBlockPuzzle : ActivatableObject, ISwitchInput
{
    public NoteObject note;

    [SerializeField] private InventoryItem_Note[] notes;
    private int currentNoteIndex = 0;

    public event EventHandler<EventArgs> CycleNextNote;
    public event EventHandler<EventArgs> CyclePreviousNote;

    private void OnEnable()
    {
        notes = NoteTracker.Instance.ReturnNotes();
    }
    public override bool TryActivate()
    {
        if (notes[currentNoteIndex].GetNoteObject() == note)
        {
            Activate();
            return true;
        }

        return false;
    }

    public void CycleNextInventoryNote()
    {
        currentNoteIndex++;
        if(currentNoteIndex >= notes.Length) { currentNoteIndex = 0; }
    }

    public void CyclePreviousInventoryNote()
    {
        currentNoteIndex--;
        if(currentNoteIndex < 0) { currentNoteIndex = notes.Length - 1; }
    }

    public void Previous()
    {
        CyclePreviousInventoryNote();
        OnCycleNextNote();
        //Debug.Log("Cycling Previous");
    }

    public void Next()
    {
        CycleNextInventoryNote();
        OnCycleNextNote();
        //Debug.Log("Cycling next");
    }

    public void OnCyclePreviousNote()
    {
        if (CyclePreviousNote != null) { CyclePreviousNote(this, new EventArgs()); }
    }

    public void OnCycleNextNote()
    {
        if (CycleNextNote != null) { CycleNextNote(this, new EventArgs()); }
    }
}
