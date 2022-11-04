using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Audio_InstrumentSampler))]
public class Audio_InstrumentSamplerMelodyTest : MonoBehaviour
{
    [Header("Tempo")]
    public float _BPM;
    private float _BPS;

    [Header("Melody")]
    public List<Audio_InstrumentMelodyTestEvent> _melody = new List<Audio_InstrumentMelodyTestEvent>();

    private void Start()
    {
        ManageBPS();
    }

    public void PlayAllMelody()
    {
        ManageBPS();

        for (int i = 0; i < _melody.Count; i++)
        {
            StartCoroutine(PlayInstrumentMelody(_melody[i]));
        }
    }

    public IEnumerator PlayInstrumentMelody(Audio_InstrumentMelodyTestEvent _instrument)
    {
        if (_instrument._isRepeat)
        {
            List<Audio_MelodyTestEvent> _repeatedList = new List<Audio_MelodyTestEvent>();

            for (int i = 0; i < _instrument._repeatNumber; i++)
            {
                _repeatedList.AddRange(_instrument._allNoteForSongs);
            }

            foreach (Audio_MelodyTestEvent _event in _repeatedList)
            {
                //Update duration
                _event._GetNoteDuration();

                //Get list with original note (quick fix to not lose data)
                List<string> _allNotes = new List<string>(_event._notes);
                _allNotes.Add(_event._note);

                //Play note
                float maxTime = Time.time + ManageNoteDuration(_event._noteDuration);
                _instrument._instrument.PlayWwiseMIDIEventWithStop(_allNotes, maxTime);

                //Wait for next note
                while (Time.time <= maxTime)
                {
                    yield return null;
                }
            }
        }
        else
        {
            foreach (Audio_MelodyTestEvent _event in _instrument._allNoteForSongs)
            {
                //Update duration
                _event._GetNoteDuration();

                //Get list with original note (quick fix to not lose data)
                List<string> _allNotes = new List<string>(_event._notes);
                _allNotes.Add(_event._note);

                //Play note
                float maxTime = Time.time + ManageNoteDuration(_event._noteDuration);
                _instrument._instrument.PlayWwiseMIDIEventWithStop(_allNotes, maxTime);

                //Wait for next note
                while (Time.time <= maxTime)
                {
                    yield return null;
                }
            }
        }
    }

    public void StopMelody()
    {
        StopAllCoroutines();
    }

    public void ManageBPS()
    {
        _BPS = 60 / _BPM;
        Debug.Log("Current BPS is: " + _BPS);
    }

    public float ManageNoteDuration(float _noteDuration)
    {
        //Debug.Log(_BPS * _noteDuration);
        return _BPS * _noteDuration;
    }
}