using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio_MelodyTestEvent
{
    public string _note;
    public List<string> _notes;
    public Audio_FigureRythmique _figureRythmique;
    public float _noteDuration;

    public void _GetNoteDuration()
    {
        switch (_figureRythmique)
        {
            case Audio_FigureRythmique._Croche:
                _noteDuration = 0.5f;
                break;
            case Audio_FigureRythmique._CrochePointe:
                _noteDuration = 0.75f;
                break;
            case Audio_FigureRythmique._Noir:
                _noteDuration = 1f;
                break;
            case Audio_FigureRythmique._NoirPointe:
                _noteDuration = 1.5f;
                break;
            case Audio_FigureRythmique._Blanche:
                _noteDuration = 2;
                break;
            case Audio_FigureRythmique._BlanchePointe:
                _noteDuration = 3f;
                break;
            case Audio_FigureRythmique._Ronde:
                _noteDuration = 4f;
                break;
            case Audio_FigureRythmique._RondePointe:
                _noteDuration = 6f;
                break;
            case Audio_FigureRythmique._Autre:
                //_noteDuration = _noteDuration;
                break;
            default:
                //_noteDuration = _noteDuration;
                break;
        }
    }
}
