using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Audio_Sampler : MonoBehaviour
{
    //Play sampleur event correctley and with objective
    //Play one audio clip with Wwise
    public string _noteTest;

    public AK.Wwise.Event _currentInstrument;

    public List<name_SamplerEvent> _nameEvents = new List<name_SamplerEvent>();
    public Dictionary<string, AK.Wwise.Event> _events = new Dictionary<string, AK.Wwise.Event>();
    public Dictionary<string, byte> _offNoteByte = new Dictionary<string, byte>();

    private bool _once;

    public void Awake()
    {
        GenerateDictionnarySampler();
    }

    private void Update()
    {
        if (Keyboard.current.hKey.isPressed && !_once)
        {
            StartCoroutine(PlayAllSounds());

            //StartCoroutine(PlayAllSoundsReverse());

            //Play
            //PlayMIDIEventWwise("55");

            _once = true;
        }
        else if (Keyboard.current.kKey.isPressed && !_once)
        {
            //Play
            PlayMIDIEventWwise(_noteTest);

            _once = true;
        }

        if (!Keyboard.current.hKey.isPressed && !Keyboard.current.kKey.isPressed)
        {
            _once = false;
        }
    }

    private void GenerateDictionnarySampler()
    {
        _events = new Dictionary<string, AK.Wwise.Event>();
        _offNoteByte = new Dictionary<string, byte>();

        ChangeInstrument();

        //Generate dictionnary
        foreach (name_SamplerEvent _name in _nameEvents)
        {
            //Add events to dictionnary
            _events.Add(_name._name, _name._event);
            _offNoteByte.Add(_name._name, _name._eventByte);
        }
    }

    private void ChangeInstrument()
    {
        //Change instrument
        for (int i = 0; i< _nameEvents.Count; i++)
        {
            var _e = _nameEvents[i];
            _e._event = _currentInstrument;
            _nameEvents[i] = _e;
        }
    }

    public void PlayMIDIEventWwise(string _name)
    {
        PlayWwiseEventMIDI(_events[_name], _offNoteByte[_name]);
    }

    public void PlayWwiseEventMIDI(AK.Wwise.Event _event, byte _offNote)
    {
        //Generate array
        AkMIDIPostArray MIDIPostArrayBuffer = new AkMIDIPostArray(2);
        AkMIDIPost midiEvent = new AkMIDIPost();

        midiEvent.byType = AkMIDIEventTypes.NOTE_ON;
        midiEvent.byChan = 0;
        midiEvent.byOnOffNote = _offNote;
        midiEvent.byVelocity = 127;
        midiEvent.uOffset = 0;
        MIDIPostArrayBuffer[0] = midiEvent;

        midiEvent.byType = AkMIDIEventTypes.NOTE_OFF;
        midiEvent.byChan = 0;
        midiEvent.byOnOffNote = _offNote;
        midiEvent.byVelocity = 0;
        midiEvent.uOffset = 48000 * 8;
        MIDIPostArrayBuffer[1] = midiEvent;

        //Do sound
        //_event.PostMIDI(this.gameObject, MIDIPostArrayBuffer, 2);
        _event.PostMIDI(this.gameObject, MIDIPostArrayBuffer);

        Debug.Log("Produce a new sound MIDI! " + _event + " " +_offNote);
    }

    private IEnumerator PlayAllSounds()
    {
        foreach (name_SamplerEvent _name in _nameEvents)
        {
            //Play
            PlayMIDIEventWwise(_name._name);

            //Wait
            yield return new WaitForSeconds(.6f);
        }
    }

    private IEnumerator PlayAllSoundsReverse()
    {
        _nameEvents.Reverse();

        foreach (name_SamplerEvent _name in _nameEvents)
        {
            //Play
            PlayMIDIEventWwise(_name._name);

            //Wait
            yield return new WaitForSeconds(8f);
        }
    }
}

[System.Serializable]
public struct name_SamplerEvent
{
    public string _name;
    public AK.Wwise.Event _event;
    public byte _eventByte;
}
