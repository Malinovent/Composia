using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class TimeManager : Singleton<TimeManager>, IObservable, IRestartable
    {
        [Range(3,4)][SerializeField]
        private int _timeSignature;
        private int _currentMeasure;
        private float _measureLengthInSeconds;
        private float _measureLengthInSecondsTotal;
        private float _measureLengthInSecondsTimer;
        private bool _isTiming = false;
        private float _currentBeatTime = 0;
        private int _sixteenthBeatCount = 0;

        [SerializeField]
        private int _bpm;
        private float _bps;

        private float _sixteenthBPS;
        private float _sixteenthBPSTimer;

        public bool isDebug = false;
        public List<float> _expectedTimes = new List<float>();

        [SerializeField]
        public List<IObserver> observers = new List<IObserver>();
        public List<IRestartable> restartables = new List<IRestartable>();


        #region GETTERS

        public int BPM { get => _bpm; set => _bpm = value; }
        public float BPS { get => _bps; set => _bps = value; }
        public float SixteenthBPS { get => _sixteenthBPS; set => _sixteenthBPS = value; }
        public int CurrentMeasure { get => _currentMeasure; set => _currentMeasure = value; }
        public int TimeSignature { get => _timeSignature; set => _timeSignature = value; }

        #endregion

        #region OVERRIDES

        private void Awake()
        {
            BPS = BPM / 60f;
            SixteenthBPS = BPS * 0.25f;

            _measureLengthInSeconds = TimeSignature * BPS;
            _measureLengthInSecondsTotal = _measureLengthInSeconds;
            CalculateExpectedTimes();
        }

        private void Update()
        {
            if (_isTiming)
            {
                SixteenthBeat();              
                CalculateMeasureInTime();
            }
        }

        #endregion

        private void CalculateExpectedTimes()
        {
            if (isDebug)
            {
                for (int i = 0; i < Level.Instance.TotalColumns; i++)
                {
                    _expectedTimes.Add(i * SixteenthBPS);
                }
            }
        }

        private void NextBeatTime()
        {
            if(_sixteenthBeatCount > _expectedTimes.Count) { return; }
            if (isDebug) { _currentBeatTime = _expectedTimes[_sixteenthBeatCount]; }
            _sixteenthBeatCount++;
        }

        public List<float> ReturnExpectedTimes()
        {
            return _expectedTimes;
        }
        private void CalculateMeasureInTime()
        {
            _measureLengthInSecondsTimer += Time.deltaTime;

            if (_measureLengthInSecondsTimer > _measureLengthInSecondsTotal)
            {
                CurrentMeasure++;
                _measureLengthInSecondsTotal += _measureLengthInSeconds;               
            }
        }

        private void SixteenthBeat()
        {
            _sixteenthBPSTimer += Time.deltaTime;

            if (_sixteenthBPSTimer > SixteenthBPS)
            {
                OnSixteenthBeat();
                _sixteenthBPSTimer -= SixteenthBPS;
            }
        }

        private void OnSixteenthBeat()
        {
            NextBeatTime();
            NotifyObservers();
        }

        public void StartTimer()
        {
            _isTiming = true;
            OnSixteenthBeat();
        }

        public void Restart()
        {
            _currentBeatTime = 0;
            _sixteenthBeatCount = 0;

            foreach (IRestartable r in restartables)
            {
                r.Restart();
            }
        }




        #region OBSERVER IMPLEMENTATION
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
            foreach (IObserver observer in observers)
            {
                observer.UpdateData(this);
            }
        }
        #endregion

        #region Restart
        public void AddRestartable(IRestartable r)
        {
            restartables.Add(r);
        }
        #endregion
    }
}