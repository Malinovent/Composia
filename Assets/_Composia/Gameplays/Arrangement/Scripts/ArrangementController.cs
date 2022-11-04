using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Composia.Arrangement
{ 
    public class ArrangementController : Singleton<ArrangementController>, IObservable, IRestartable
    {
        public int BPM;
        private float _bps;

        [Range(3,4)]
        public int timeSignature = 4;

        public TrackControllerArrangement[] _trackGroups = new TrackControllerArrangement[4];

        private List<IObserver> _observers = new List<IObserver>();
        public List<IRestartable> _restartables = new List<IRestartable>();

        private float _sixteenthBPSTimer = 0;
        private float _sixteenthBPS = 0;

        private int _currentBeat = 0;
        private int _segmentIndex = 0;
        private int _trackGroupIndex = 0;

        private bool _isPlaying = false;
        private int _maxBeats;
        [SerializeField]
        private int _numOfSegments = 4;

        public string songName;
        public GameObject confirmationUI;

        public Text modeText;

        private Mode _mode;

        #region GETTERS
        public int SegmentIndex { get => _segmentIndex; set => _segmentIndex = value; }
        public int CurrentBeat { get => _currentBeat; set => _currentBeat = value; }
        public float BPS { get => _bps; set => _bps = value; }
        public int NumOfSegments { get => _numOfSegments; set => _numOfSegments = value; }
        public Mode Mode { get => _mode; set => _mode = value; }

        #endregion

        #region OVERRIDES
        private void Awake()
        {
            //AddTrackObserversOnAwake();
            BPS = 60f / BPM;
            _sixteenthBPS = BPS * 0.25f;
            _maxBeats = timeSignature * 16;
            _trackGroups[_trackGroupIndex].ShiftOutlineObject(0);
            UpdateMode(_trackGroupIndex);
            confirmationUI.SetActive(false);
        }

        private void Update()
        {
            if (_isPlaying)
            {
                SixteenthBeatTimer();
            }

            OutlineInput();
        }

        #endregion

        public void AddRestartable(IRestartable r)
        {
            _restartables.Add(r);
        }

        void OutlineInput()
        {
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                ShiftTrackGroup(1);
            }
            else if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                ShiftTrackGroup(-1);
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                _trackGroups[_trackGroupIndex].ShiftOutlineObject(1);
            }
            else if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                _trackGroups[_trackGroupIndex].ShiftOutlineObject(-1);
            }
            /*
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                RetrogradeTrack();
            }

            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                InverseTrack();
            }*/
        }

        private void UpdateMode(int index)
        {
            switch (index)
            {
                case (0):
                    Mode = Mode.Melody;
                    break;
                case (1):
                    Mode = Mode.Harmony;
                    break;
                case (2):
                    Mode = Mode.Bass;
                    break;
                case (3):
                    Mode = Mode.Percussion;
                    break;
            }

            NotifyObservers();
            UpdateText();
        }

        private void UpdateText()
        {
            modeText.text = "Mode " + Mode;
        }
        /*
        void RetrogradeTrack()
        {
            _trackGroups[_trackGroupIndex].Track.Segments[_trackGroups[_trackGroupIndex].SegmentOutlineIndex].Retrograde();
        }

        void InverseTrack()
        {
            _trackGroups[_trackGroupIndex].Track.Segments[_trackGroups[_trackGroupIndex].SegmentOutlineIndex].Inverse();
        }
        */
        void ShiftTrackGroup(int index)
        {
            TrackControllerArrangement currentTrack = _trackGroups[_trackGroupIndex];

            currentTrack.RemoveOutlines();
            int lastOutlineIndex = currentTrack.GetLastOutlineIndex();
            currentTrack.ResetOutlineIndex();
            _trackGroupIndex += index;

            if(_trackGroupIndex >= _trackGroups.Length) 
            { 
                _trackGroupIndex = 0; 
            }
            else if(_trackGroupIndex < 0) 
            {
                _trackGroupIndex = _trackGroups.Length - 1; 
            }

            _trackGroups[_trackGroupIndex].ShiftOutlineObject(lastOutlineIndex);
            UpdateMode(_trackGroupIndex);
        }

        public void AddSegment(Segment track)
        {
            _trackGroups[_trackGroupIndex].AddTrack(track);
        }

        public void TogglePlay()
        {       
            _isPlaying = !_isPlaying;
            //Debug.Log("isPlaying is now " + _isPlaying);
        }

        public void Play()
        {
            _isPlaying = true;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void LockInTrack()
        {
            confirmationUI.SetActive(true);
        }

        public void SetSongName(Text text)
        {
            string name = text.text;

            if (name == "") { songName = "Composia Demo"; }
            else { songName = name; }
        }
        public void SaveAllTracks()
        {
            foreach (TrackControllerArrangement track in _trackGroups)
            {
                track.Track.Save(songName);
            }
        }
        private void SixteenthBeatTimer()
        {
            _sixteenthBPSTimer += Time.deltaTime;

            if (_sixteenthBPSTimer > _sixteenthBPS)
            {
                OnSixteenthBeat();
                _sixteenthBPSTimer -= _sixteenthBPS;
            }
        }

        private void OnSixteenthBeat()
        {
            CurrentBeat++;
            if (CurrentBeat >= _maxBeats)
            {
                
                CurrentBeat = 0;
                SegmentIndex++;
                RestartObservers();
                if (SegmentIndex >= NumOfSegments) { Restart(); }
                //Debug.Log("Now playing segment " + (SegmentIndex + 1));
            }

            NotifyObservers();
        }


        #region RESTARTABLE
        public void RestartObservers()
        {
            foreach(IRestartable r in _restartables)
            {
                r.Restart();
            }
        }

        public void Restart()
        {
            CurrentBeat = 0;
            SegmentIndex = 0;
            _sixteenthBPSTimer = 0;
            _isPlaying = false;
            RestartObservers();
        }
        #endregion


        #region OBSERVABLE
        private void AddTrackObserversOnAwake()
        {
            foreach (TrackControllerArrangement t in _trackGroups)
            {
                //AddObserver(t);
            }
        }

        public void AddObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyTrackObservers()
        {
            foreach (IObserver o in _trackGroups)
            {
                if(o != null) { o.UpdateData(this); }               
            }
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in _observers)
            {
                o.UpdateData(this);
            }
        }
        #endregion
    }
}
