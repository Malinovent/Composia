using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia.Arrangement
{
    public class PerformanceController : MusicTimer, IObservable
    {
        [SerializeField]
        private TrackData[] _tracksData = new TrackData[4];
        [SerializeField]
        private TrackPerformance[] _tracks;

        private bool _canNuance = true;

        PerformanceControllerUI ui;

        public string[] trackNames = new string[4];
        public TrackData[] TracksData { get => _tracksData; set => _tracksData = value; }
        public TrackPerformance[] Tracks { get => _tracks; set => _tracks = value; }
        public bool CanNuance { get => _canNuance; set => _canNuance = value; }
        public bool IsAccident { get => _isAccident; set => _isAccident = value; }
        public float HealthCurrent {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                if (_currentHealth < 0) { _currentHealth = 0; Lose(); }
                else if (_currentHealth > healthMax) { _currentHealth = healthMax; }
                ui.UpdateHealthSlider(_currentHealth);
            }
        }

        public static PerformanceController Instance;

        private List<IObserver> observers = new List<IObserver>();

        public float healthMax = 100;
        public float healthRecoveryRate = 5;
        public float healthLossRate = 10;

        private float _currentHealth = 50;
        private bool _isAccident = false;

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this.gameObject); }

            for (int i = 0; i < TracksData.Length; i++)
            {
                if (Tracks[i] == null) { continue; }
                Tracks[i].TrackData = _tracksData[i];
            }

            ui = GetComponent<PerformanceControllerUI>();
            ui.UpdateBPMText();
        }

        void Start()
        {
            LoadTracks();
            SetBPM();
           
        }

        void Update()
        {
            if (IsPlaying)
            {
                if (IsAccident)
                {
                    HealthCurrent -= Time.deltaTime * healthLossRate;
                }
                else
                {
                    HealthCurrent += Time.deltaTime * healthRecoveryRate;
                }
            }
        }

        void Lose()
        {
            ui.DisplayLoseUI();
            Stop();
        }

        void LoadTracks()
        {
            for (int i = 0; i < TracksData.Length; i++)
            {
                LoadTrack(i);
            }
        }

        public void LoadTrack(int i)
        {
            TracksData[i] = SaveLoad.LoadTrack(trackNames[i]);
        }

        public void Play()
        {
            if (!IsPlaying)
            {
                ui.HidePlayUI();
                StartPlaying();
                PerformanceControllerAccidents.Instance.StartPlaying();
                foreach (TrackPerformance t in Tracks)
                {
                    t.StartPlaying();
                }
            }
        }

        public void Stop()
        {
            ui.DisplayLoseUI();
            Restart();
            PerformanceControllerAccidents.Instance.Stop();
            foreach (TrackPerformance t in Tracks)
            {
                t.Restart();
            }
        }

        public void RestartFromBeginning()
        {
            ui.HideLoseUI();
            ui.DisplayPlayUI();
            foreach (TrackPerformance t in Tracks)
            {
                t.Restart();
            }
        }
        protected override void OnSixteenthBeat()
        {
            if (IsLastBeat()) { ui.DisplayWinTextUI(TracksData[0].myName); }
            NotifyObservers();
        }

        public void SetBPM()
        {
            if (CanNuance)
            {
                int newBPM = ui.UpdateBPMText();
                AdjustBPM(newBPM);
                SetBPMAll();
            }
        }

        public void SetBPM(float value)
        {
            if (CanNuance)
            {                
                int newBPM = BPM;
                newBPM += (int)value;
                AdjustBPM(newBPM);
                ui.UpdateBPMSlider(BPM);
                SetBPMAll();
            }
        }

        private void SetBPMAll()
        {
            foreach (TrackPerformance t in Tracks)
            {
                t.AdjustBPM(BPM);
                t.UpdateBPM();
            }
        }

        #region Observer
        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.UpdateData(this);
            }
        }

        #endregion


        
    }
}
