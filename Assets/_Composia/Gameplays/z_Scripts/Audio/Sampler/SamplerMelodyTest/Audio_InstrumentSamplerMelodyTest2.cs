using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_InstrumentSamplerMelodyTest2 : MonoBehaviour
{
    [Header("Tempo")]
    public float _BPM;
    private float _BPS;
    public bool _canPlay;

    [Header("Melody")]
    public List<Audio_InstrumentMelodyTestEvent> _melody = new List<Audio_InstrumentMelodyTestEvent>();

    private void Update()
    {
        //Manage each instrument in update when needed
        if (_canPlay)
        {
            foreach (Audio_InstrumentMelodyTestEvent _instrument in _melody)
            {
                WaitToPlayEvent(_instrument);
            }
        }
    }

    //Play function
    public void PlayAllMelody()
    {
        //Get BPS
        ManageBPS();

        //Calculate actual events
        foreach (Audio_InstrumentMelodyTestEvent _instrument in _melody)
        {
            _instrument._actualEventsForSong = new List<Audio_MelodyTestEvent>();
            _instrument._actualEventsForSong.AddRange(CalculateActualEventsForMelody(_instrument));
        }

        //Calculate time
        float timeAtStartPlay = Time.time;
        foreach (Audio_InstrumentMelodyTestEvent _instrument in _melody)
        {
            CalculateAllTimeForEvents(_instrument, _instrument._actualEventsForSong, timeAtStartPlay);
        }

        //Play
        _canPlay = true;
    }


    public void StopMelody()
    {
        _canPlay = false;
        StopAllCoroutines();
    }

    //Logic function
    private List<Audio_MelodyTestEvent> CalculateActualEventsForMelody(Audio_InstrumentMelodyTestEvent _instrument)
    {
        if (_instrument._isRepeat)
        {
            List<Audio_MelodyTestEvent> _repeatedList = new List<Audio_MelodyTestEvent>();

            for (int i = 0; i < _instrument._repeatNumber; i++)
            {
                _repeatedList.AddRange(_instrument._allNoteForSongs);
            }

            return _repeatedList;
        }

        return _instrument._allNoteForSongs;
    }

    private void CalculateAllTimeForEvents(Audio_InstrumentMelodyTestEvent _instrument,
        List<Audio_MelodyTestEvent> _allNotes, 
        float _timeStart)
    {
        //Values
        float _timeAt = 0;
        _timeAt += _timeStart;

        //Reset
        _instrument._noteStartTime = new List<float>();
        _instrument._canPlay = new List<bool>();

        foreach (Audio_MelodyTestEvent _note in _allNotes)
        {
            //Get the time the note should play
            float noteTimeToPlay = _timeAt;
            //Debug.Log(_instrument.name + " " + noteTimeToPlay);

            //Add values to their respective list
            _instrument._noteStartTime.Add(noteTimeToPlay);
            _instrument._canPlay.Add(false);

            //Add timer value to timer
            _note._GetNoteDuration();
            _timeAt += ManageNoteDuration(_note._noteDuration);          
        }
    }

    private void WaitToPlayEvent(Audio_InstrumentMelodyTestEvent _instrument)
    {
        for (int i = 0; i < _instrument._actualEventsForSong.Count; i++)
        {
            if (Time.time >= _instrument._noteStartTime[i] && !_instrument._canPlay[i])
            {
                PlayActualEvent(_instrument, _instrument._actualEventsForSong[i]);
                _instrument._canPlay[i] = true;
            }
        }
    }

    private void PlayActualEvent(Audio_InstrumentMelodyTestEvent _instrument, Audio_MelodyTestEvent _event)
    {
        //Update duration
        _event._GetNoteDuration();

        //Get list with original note (quick fix to not lose data)
        List<string> _allNotes = new List<string>(_event._notes);
        _allNotes.Add(_event._note);

        //Play note
        _instrument._instrument.PlayWwiseMIDIEventWithStop(_allNotes, Time.time + ManageNoteDuration(_event._noteDuration));
    }

    //Other function
    public void ManageBPS()
    {
        _BPS = 60 / _BPM;
        Debug.Log("Current BPS is: " + _BPS);
    }

    public float ManageNoteDuration(float _noteDuration)
    {
        return _BPS * _noteDuration;
    }
}
