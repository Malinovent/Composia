using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio_InstrumentMelodyTestEvent
{
    public string name;
    public Audio_InstrumentSampler _instrument;
    public List<Audio_MelodyTestEvent> _allNoteForSongs = new List<Audio_MelodyTestEvent>();
    public bool _isRepeat;
    public int _repeatNumber;
    [HideInInspector] public List<float> _noteStartTime = new List<float>();
    [HideInInspector] public List<bool> _canPlay = new List<bool>();
    [HideInInspector] public List<Audio_MelodyTestEvent> _actualEventsForSong = new List<Audio_MelodyTestEvent>();
}
