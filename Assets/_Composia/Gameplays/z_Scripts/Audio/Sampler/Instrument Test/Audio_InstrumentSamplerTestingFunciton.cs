using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Audio_InstrumentSampler))]
public class Audio_InstrumentSamplerTestingFunciton : MonoBehaviour
{
    private Audio_InstrumentSampler instrumentSampler { get { return GetComponent<Audio_InstrumentSampler>(); } }

    [Header("One Event")]
    public string _nameEvent = "A3";
    public float _timeDuration = 0.5f;

    [Header("All Events")]
    public float _timeSteps = 0.6f;
    [HideInInspector] public Coroutine _currentCoroutine;

    public void PlayTargetSound()
    {
        //Get one sound from list
        List<string> _name = new List<string>();
        _name.Add(_nameEvent);

        //Play 
        instrumentSampler.PlayWwiseMIDIEventWithStop(_name, Time.time + _timeDuration);
    }

    public IEnumerator PlayAllSounds()
    {
        foreach (NoteSamplerEvent _event in instrumentSampler._allNote._allNoteSamplerEvent)
        {
            //Get one sound from list
            List<string> _name = new List<string>();
            _name.Add(_event._name);

            //Play
            instrumentSampler.PlayWwiseMIDIEventWithStop(_name, Time.time + _timeSteps);

            //Wait
            yield return new WaitForSeconds(_timeSteps);
        }

        //Make sure sounds stop and can restart after
        StopSounds();
    }

    public IEnumerator PlayAllSoundsReverse()
    {
        //Reverse
        List<NoteSamplerEvent> _allNoteReverse = new List<NoteSamplerEvent>(instrumentSampler._allNote._allNoteSamplerEvent);
        _allNoteReverse.Reverse();

        foreach (NoteSamplerEvent _event in _allNoteReverse)
        {
            //Get one sound from list
            List<string> _name = new List<string>();
            _name.Add(_event._name);

            //Play
            instrumentSampler.PlayWwiseMIDIEventWithStop(_name, Time.time + _timeSteps);

            //Wait
            yield return new WaitForSeconds(_timeSteps);
        }

        //Make sure sounds stop and can restart after
        StopSounds();
    }

    public void StopSounds()
    {
        //Stop coroutine if needed
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        //Empty coroutine
        _currentCoroutine = null;

        //Stop sampler
        instrumentSampler.StopCurrentEvent();
    }
}
