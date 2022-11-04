using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Audio_InstrumentSampler : MonoBehaviour
{
    //Manage the instrument sampler event from here
    public AK.Wwise.Event _currentInstrument;
    public byte _channel = 0;
    public AllNoteSamplerEvent _allNote;//Data for events

    //Manage active current event time
    private float _stopEvent;
    private bool _stopEventOnce;

    private void Update()
    {
        //Check when the instrument needs to be stop
        if (Time.time >= _stopEvent && _stopEventOnce)
        {
            _currentInstrument.StopMIDI();
            _stopEventOnce = false;
        }
    }

    public void PlayWwiseMIDIEventWithStop(List<string> _names, float timeToStop)
    {
        //Get the event list
        List<NoteSamplerEvent> _notes = new List<NoteSamplerEvent>();
        _notes = GetCurrentEvents(_names);

        //Play the event
        WwiseMIDIEvent(_currentInstrument, _notes);

        //Debug.Log(_currentInstrument + " Start " + Time.time + " / " + timeToStop);
        //Debug.Log(_currentInstrument + " noteDuration " + (timeToStop - Time.time));

        //Stop event when needed
        _stopEvent = timeToStop;
        _stopEventOnce = true;
    }

    public void WwiseMIDIEvent(AK.Wwise.Event _event, List<NoteSamplerEvent> _offNote)
    {
        //Generate array
        AkMIDIPostArray MIDIPostArrayBuffer = new AkMIDIPostArray(_offNote.Count*2);
        AkMIDIPost midiEvent = new AkMIDIPost();

        int current = 0;
        int off = _offNote.Count;
        foreach (NoteSamplerEvent note in _offNote)
        {
            //Add to list
            midiEvent.byType = AkMIDIEventTypes.NOTE_ON;
            midiEvent.byChan = _channel;
            midiEvent.byOnOffNote = note._MIDIeventByte;
            midiEvent.byVelocity = 127;
            midiEvent.uOffset = 0;
            MIDIPostArrayBuffer[current] = midiEvent;
            current++;

            //Add off to list
            midiEvent.byType = AkMIDIEventTypes.NOTE_OFF;
            midiEvent.byChan = _channel;
            midiEvent.byOnOffNote = note._MIDIeventByte;
            midiEvent.byVelocity = 0;
            midiEvent.uOffset = 48000 * 8;
            MIDIPostArrayBuffer[off] = midiEvent;
            off++;

            //Debug.Log("Play ! " + _event.Name + "Byte" + note._MIDIeventByte + " Array: " + (current-1) + " / " + (off-1));       
            //Debug.Log("Instrument ! " + _event.Name + " " + Time.time);       
        }

        //Do sound
        _event.PostMIDI(this.gameObject, MIDIPostArrayBuffer);
    }

    public List<NoteSamplerEvent> GetCurrentEvents (List<string> _names)
    {
        //Get the event list
        List<NoteSamplerEvent> _notes = new List<NoteSamplerEvent>();

        //Search and add event to list
        foreach (string _name in _names)
        {
            NoteSamplerEvent _note = _allNote._allNoteSamplerEvent.Where(obj => obj._name == _name).SingleOrDefault();
            _notes.Add(_note);
        }

        return _notes;//return data
    }

    //Stop current instrument if needed
    public void StopCurrentEvent()
    {
        _currentInstrument.StopMIDI();
        _stopEventOnce = false;
    }
}
