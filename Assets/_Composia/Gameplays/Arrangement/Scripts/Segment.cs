using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Composia.Arrangement
{
    public class Segment : MonoBehaviour
    {
        [Range(3, 4)]
        [SerializeField]
        private int _timeSignature = 4;
        [Range(60, 150)]
        [SerializeField]
        private int _initialBPM = 60;

        private Note _currentNote;
        public List<Note> notesInOrder;

        private int _noteIndexCount = 0;
        private int _noteIndex = 0;
        [SerializeField]
        private string _name;

        public string _saveFileName;

        private SegmentData _segmentData;

        [SerializeField]
        private TextAsset jSonData;


        #region GETTERS
        public int TimeSignature
        {
            get => _timeSignature;
            set
            {
                _timeSignature = value;

                if (_timeSignature < 3)
                {
                    _timeSignature = 3;
                }
                else if (_timeSignature > 4)
                {
                    _timeSignature = 4;
                }
            }
        }

        public int BPM
        {
            get => _initialBPM;
            set
            {
                _initialBPM = value;
                if (_initialBPM < 60) { _initialBPM = 60; }
                if (_initialBPM > 150) { _initialBPM = 150; }
            }
        }

        public SegmentData SegmentData {
            get
            {
                SetSegmentData();
                return _segmentData;
            }
            set => _segmentData = value; }

        public string Name { get => _name; set => _name = value; }
        #endregion

        void Awake()
        {
            //if(jSonData != null) { _segmentData = SaveLoad.LoadSegment(jSonData)} 
            //SetSegmentData(_segmentData);
            //_segmentData = SaveLoad.LoadSegment(_saveFileName);
        }

        public void SetSegmentData(SegmentData data)
        {
            notesInOrder.Clear();
            Name = data.myName;
            TimeSignature = data.timeSignature;
            BPM = data.originalBPM;

            foreach (NoteData noteData in data.notesInOrder)
            {
                int size = NoteUtils.ConvertNoteToSize(noteData.type);
                NotesEnum note = NoteUtils.ConvertYToNote(noteData.y);
                Note newNote = new Note(note, size);
                notesInOrder.Add(newNote);
            }
        }

        private void SetSegmentData()
        {
            _segmentData = new SegmentData(Name, TimeSignature, BPM, notesInOrder);
        }

        public void Retrograde()
        {
           notesInOrder.Reverse();     
        }

        public void Inverse()
        {
            foreach (Note note in notesInOrder)
            {
                int noteY = NoteUtils.ConvertNoteToY(note.note);
                int reverseY = (noteY + 10) - (noteY * 2);
                note.note = NoteUtils.ConvertYToNote(reverseY);

                Debug.Log("Reversing " + noteY + " to become " + reverseY);
            }
        }

        //Checks if the track has all the notes it needs
        public void ValidateSegment()
        {
            int validateSize = TimeSignature * 16;
            int count = 0;

            foreach (Note n in notesInOrder)
            {
                count += n.length;
            }

            if (count > validateSize)
            {
                Debug.LogError("This track has TOO MANY notes! It has exactly " + count + " worth of notes. Based on it's timeSignautre[" + TimeSignature + "] it should have " + validateSize);
            }
            else if (count < validateSize)
            {
                Debug.LogError("This track has TOO FEW notes! It has exactly " + count + " worth of notes. Based on it's timeSignautre[" + TimeSignature + "] it should have " + validateSize);
            }
            else
            {
                Debug.Log("Right amount of notes!");
            }
        }



        //Plays a note at a particular timeIndex (eg. up to 64 if timeSignature is 4)
        public Note GetNote(int timeIndex)
        {
            if (timeIndex < _noteIndexCount)
            {
                //Debug.Log("I'm already playing a note!");
                return null;
            }


            _currentNote = notesInOrder[_noteIndex];
            _noteIndexCount += _currentNote.length;
            _noteIndex++;


            return _currentNote;

        }
    }
}
