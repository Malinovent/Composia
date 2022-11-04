using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackPerformanceUI : MonoBehaviour
{
    //public GameObject uiObject;

    public Text bpmText;
    public Text pitchText;
    public Text noteIndexText;

    public void UpdateBPMText(int bpm)
    {
        bpmText.text = "BPM: " + bpm.ToString();
    }

    public void UpdatePitchText(int pitch)
    {
        pitchText.text = "Pitch: " + pitch.ToString();
    }

    public void UpdateNoteIndexText(int notedIndex)
    {
        noteIndexText.text = "Note: " + notedIndex.ToString();
    }

}
