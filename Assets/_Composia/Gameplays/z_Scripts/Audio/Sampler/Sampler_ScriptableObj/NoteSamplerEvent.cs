using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Audio/Sampler/NoteSamplerEvent", order = 0)]
public class NoteSamplerEvent : ScriptableObject
{
    public string _name;
    public byte _MIDIeventByte;
}
