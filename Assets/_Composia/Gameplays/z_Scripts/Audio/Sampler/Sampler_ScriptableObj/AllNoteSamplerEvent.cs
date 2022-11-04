using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Audio/Sampler/AllNote", order = 1)]
public class AllNoteSamplerEvent : ScriptableObject
{
    public List<NoteSamplerEvent> _allNoteSamplerEvent = new List<NoteSamplerEvent>();
}
