using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia.Arrangement
{
    [RequireComponent(typeof(Audio_InstrumentSampler))]
    public class TrackPerformance : MusicTimer, IObserver
    {
        [SerializeField]
        private TrackData _trackData;
        [SerializeField]
        private Audio_InstrumentSampler _sampler;
        [SerializeField]
        private List<NoteData> _allNotes = new List<NoteData>();

        private int _currentNoteIndex = 0;
        private int _noteIndexCounter = 0;

        private TrackPerformanceUI ui;
        [SerializeField]
        private int _pitch;
        [SerializeField]
        private int _trackIndex;     //This variable is used to identify this particular track

        private bool _willRemoveSelfFromObserverList = false;

        public TrackData TrackData
        {
            get => _trackData;
            set
            {
                _trackData = value;
                GetAllNotes();
            }
        }

        public int Pitch
        {
            get => _pitch;
            set
            {
                _pitch = value;
                UpdatePitch();
            }
        }

        public int NoteIndexCounter
        {
            get => _noteIndexCounter;
            set => _noteIndexCounter = value;
        }
        public int TrackIndex { get => _trackIndex; set => _trackIndex = value; }

        #region OVERRIDES
        void Awake()
        {
            if (_sampler == null) { _sampler = GetComponent<Audio_InstrumentSampler>(); }
            ui = GetComponent<TrackPerformanceUI>();
            // GetAllNotes();
            UpdatePitch();
        }
        #endregion

        protected override void OnSixteenthBeat()
        {
            UpdateBPM();
            TryPlayNote();
            UpdateNoteIndexUI();

            if (_willRemoveSelfFromObserverList)
            {
                _willRemoveSelfFromObserverList = false;
                PerformanceController.Instance.RemoveObserver(this);
            }
        }

        private void GetAllNotes()
        {
            foreach (SegmentData seg in TrackData.segments)
            {
                for (int i = 0; i < seg.notesInOrder.Count; i++)
                {
                    //Debug.Log("Inside segment " + seg.myName + " getting note at index of " + i);
                    _allNotes.Add(seg.notesInOrder[i]);
                }
            }
        }

        public void TryPlayNote()
        {
            if (CurrentSixteenthBeat >= NoteIndexCounter && IsPlaying)
            {
                PlayNextNote();
            }
        }

        public void PlayNextNote()
        {
            if (_currentNoteIndex >= _allNotes.Count)
            {
                PausePlaying();
                return;
            }

            NoteData data = _allNotes[_currentNoteIndex];

            int size = NoteUtils.ConvertNoteToSize(data.type);
            string noteString = NoteUtils.ConvertYToNote(data.y).ToString();

            List<string> stringNotes = new List<string>();
            stringNotes.Add(noteString);

            float time = BPS * size;
            _sampler.PlayWwiseMIDIEventWithStop(stringNotes, time + Time.time);
            NoteIndexCounter += size;
            _currentNoteIndex++;
        }

        public void RestoreNoteIndex()
        {
            PerformanceController.Instance.AddObserver(this);
            Restart();
            int beat = PerformanceController.Instance.CurrentSixteenthBeat;
            CurrentSixteenthBeat = beat;
            NoteIndexCounter = 0;
            FindCurrentNoteIndex();
            UpdateNoteIndexUI();
            //Debug.Log("Stopped playing");
           
        }

        private void FindCurrentNoteIndex()
        {
            _currentNoteIndex = 0;
            for (int i = 0; i <= CurrentSixteenthBeat + 1; i += NoteUtils.ConvertNoteToSize(_allNotes[_currentNoteIndex].type))
            {
                _currentNoteIndex++;
                NoteIndexCounter += NoteUtils.ConvertNoteToSize(_allNotes[_currentNoteIndex].type);
            }
        }

        public void ChangeBPM(int amount)
        {
            AdjustBPM(BPM + amount);
            UpdateBPM();
        }

        public void UpdateBPM()
        {
            ui.UpdateBPMText(BPM);
        }

        public void UpdatePitch()
        {
            ui.UpdatePitchText(Pitch);
        }

        private void UpdateNoteIndexUI()
        {
            if (IsPlaying)
            {
                ui.UpdateNoteIndexText(CurrentSixteenthBeat);
            }
        }

        public override void Restart()
        {
            base.Restart();       
            AdjustBPM(PerformanceController.Instance.BPM);
            //RestoreNoteIndex();
            CurrentSixteenthBeat = 0;
            ui.UpdateNoteIndexText(CurrentSixteenthBeat);
            UpdateBPM();
            UpdatePitch();
            
        }

        #region OBSERVER
        public void UpdateData(IObservable o)
        {
            StartPlaying();
            _willRemoveSelfFromObserverList = true;
        }
        #endregion
    }   
}
