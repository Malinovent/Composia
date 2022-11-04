using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SegmentPlayer : MusicTimer
{
    SegmentData _segmentData;

    public static SegmentPlayer Instance;

    private int _noteIndexCounter = 0;
    private int _currentNoteIndex;
    private Audio_InstrumentSampler _sampler;
    private bool _canPlay = false;

    #region GETTERS
    public SegmentData SegmentData 
    { 
        get => _segmentData; 
        set => _segmentData = value;
    }
    public bool CanPlay { get => _canPlay; set => _canPlay = value; }
    #endregion

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }

        _sampler = GetComponent<Audio_InstrumentSampler>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Play();
        }
    }

    public void Play()
    {       
        if (!IsPlaying && CanPlay)
        {
            StartPlaying();
        }
        else 
        {
            PausePlaying();
        }
    }

    public override void Restart()
    {
        base.Restart();
        _noteIndexCounter = 0;
        _currentNoteIndex = 0;
    }

    protected override void OnSixteenthBeat()
    {
        TryPlayNote();
    }


    public void TryPlayNote()
    {
        if (CurrentSixteenthBeat >= _noteIndexCounter && IsPlaying)
        {
            PlayNextNote();
        }
    }

    public void PlayNextNote()
    {
        if (_currentNoteIndex >= _segmentData.notesInOrder.Count)
        {
            Restart();
            return;
        }

        NoteData data = _segmentData.notesInOrder[_currentNoteIndex];

        int size = NoteUtils.ConvertNoteToSize(data.type);
        string noteString = NoteUtils.ConvertYToNote(data.y).ToString();

        List<string> stringNotes = new List<string>();
        stringNotes.Add(noteString);

        _sampler.PlayWwiseMIDIEventWithStop(stringNotes, ((size / 4) * BPS) + Time.time);
        _noteIndexCounter += size;
        _currentNoteIndex++;
    }
}
