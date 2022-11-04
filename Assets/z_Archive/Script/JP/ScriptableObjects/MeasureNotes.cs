using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureNotes : ScriptableObject
{
    public int[] notes;
    public int timeSignature;

    public void SetNotes(int[] someNotes, int timeSignature)
    {
        this.timeSignature = timeSignature;
        this.notes = someNotes;
    }

}
