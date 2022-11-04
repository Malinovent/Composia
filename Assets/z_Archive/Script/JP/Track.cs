using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia.Arrangement
{
    [RequireComponent(typeof(Audio_InstrumentSampler))]
    public class Track : MonoBehaviour, IRestartable
    {
       // [SerializeField]
        //private Segment[] _segments = new Segment[4];

        private Audio_InstrumentSampler _sampler;
        [SerializeField]
        private string _saveName;
        [SerializeField]
        private TrackData _trackData;


        #region GETTERS
        //public Segment[] Segments { get => _segments; set => _segments = value; }
        public SegmentData[] _segmentsData;
        private int _noteIndexCount = 0;
        private int _noteIndex;


        public TrackData TrackData { get => _trackData; set => _trackData = value; }
        #endregion


        #region OVERRIDES
        private void Awake()
        {
            _sampler = GetComponent<Audio_InstrumentSampler>();
            if(_segmentsData.Length < 4) { _segmentsData = new SegmentData[4]; }
        }

        private void Update()
        {
            
        }
        #endregion



        public void Save()
        {
            SetTrackData("Jp's Cool Track");
            _saveName = "Tracks/" + _saveName;
            SaveLoad.SaveTrack(TrackData, _saveName);
        }      

        public void Save(string trackName)
        {
            SetTrackData(trackName);
            _saveName = "Tracks/" + _saveName;
            SaveLoad.SaveTrack(TrackData, _saveName);

        }

        public void LoadTrackOnly()
        {
            TrackData = SaveLoad.LoadTrack(_saveName);
        }

        public void LoadTrackAndSegments()
        {
            TrackData = SaveLoad.LoadTrack(_saveName);
           //_segmentsData = new SegmentData[_trackData.segments.Length];

            for (int i = 0; i < _segmentsData.Length; i++)
            {
                //Segments[i] = _trackData.segments[i];
                _segmentsData[i] = TrackData.segments[i];
                //Segments[i].SetNotes(_trackData.segments[i]);
            }
        }

        private void SetTrackData(string trackName)
        {
            SegmentData[] data = new SegmentData[_segmentsData.Length];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = _segmentsData[i];
            }

            TrackData = new TrackData(data, trackName);
        }

        private void PlaySegments(int bpm)
        { 
            
        }

        //Converts a segment class into the SegmentData data holder
        //IMPORTANT: Is used to have multiple of the same segments without changing the Segment class data
        public SegmentData SegmentToData(Segment segment)
        {
            SegmentData data = new SegmentData(segment.name, segment.TimeSignature, segment.BPM, segment.notesInOrder);

            return data;
        }

        public void AddSegment(int index, Segment segment)
        { 
            if(index > _segmentsData.Length) 
            {
                Debug.LogWarning("No valid spot at index " + index);
                return; 
            }

            _segmentsData[index] = SegmentToData(segment);
            //Segments[index] = segment;
        }

        public void RemoveSegment(int index)
        {
            if (index > _segmentsData.Length)
            {
                Debug.LogWarning("No valid spot at index " + index);
                return;
            }

            _segmentsData[index] = new SegmentData(null, 4, 60, new List<Note>());
            //Segments[index] = null;
        }

        public void TryPlaySound(int segmentIndex, int beatIndex, float bps)
        {
            if (_segmentsData[segmentIndex].notesInOrder.Count <= _noteIndex) { Debug.LogWarning("Missing notes!"); return; }
            NoteData note = GetNote(segmentIndex, beatIndex);
            Debug.Log("playing sound!");

            if (note.x != -1)
            {
                List<string> stringNotes = new List<string>();
                stringNotes.Add(NoteUtils.ConvertYToNoteString(note.y));
                int size = NoteUtils.ConvertNoteToSize(note.type);
                _sampler.PlayWwiseMIDIEventWithStop(stringNotes, ((size / 4) * bps) + Time.time);
                //float startTime = Time.time;
                //float endTime = Time.time + (note.length / 4) * ac.BPS;
                //Debug.Log("Playing " + note.note + "  Start: " + startTime + "  End: " + endTime);
                //Debug.Log("Playing " + NoteUtils.ConvertYToNoteString(note.y));
            }
        }

        //Plays a note at a particular timeIndex (eg. up to 64 if timeSignature is 4)
        public NoteData GetNote(int segmentIndex, int timeIndex)
        {

            //Exits if note is already playing
            if (timeIndex < _noteIndexCount)
            {
                //Debug.Log("I'm already playing a note!");
                return new NoteData(-1,-1,NoteTypeEnum.QuarterNote);
            }
           
            NoteData currentNote = _segmentsData[segmentIndex].notesInOrder[_noteIndex];
            int size = NoteUtils.ConvertNoteToSize(currentNote.type);
            Debug.Log("Playing note " + _noteIndex + ":" + NoteUtils.ConvertYToNote(currentNote.y) + " at beat " + _noteIndexCount + " for [" + size + "] 16th beats long");
            _noteIndexCount += size;
            _noteIndex++;

            return currentNote;
        }

        public void Restart()
        {
            _noteIndexCount = 0;
            _noteIndex = 0;
        }

        /*
        //Shuffles the tracks to the right part of the array
        public void MoveTrackRight()
        {
            Segment tempTrack = Segments[0];
            for (int i = 0; i < Segments.Length - 1; i++)
            {
                Segments[i] = Segments[i + 1];
            }

            Segments[Segments.Length - 1] = tempTrack;
        }

        //Shuffles the tracks to the left part of the array
        public void MoveTrackLeft()
        {
            Segment tempTrack = Segments[Segments.Length];
            for (int i = Segments.Length; i > 0; i--)
            {
                Segments[i] = Segments[i - 1];
            }

            Segments[0] = tempTrack;
        }

        public bool ValidateTracks()
        {
            foreach (Segment t in Segments)
            {
                if (t == null)
                {
                    return false;
                }
            }

            return true;
        }*/
    }
}
