using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia.Arrangement
{
    public class PerformanceControllerAccidents : Singleton<PerformanceControllerAccidents>, IRestartable
    {
        //public int numberSimultaneousAccidents = 1;     //TASK: Update code to support simultaneous accidents 
        [SerializeField]
        private float _minCooldownInSeconds = 20;
        [SerializeField]
        private float _maxCooldownInSeconds = 60;     
        [SerializeField]
        PerformanceController controller;
        [SerializeField]
        List<Accident> accidents = new List<Accident>();

        private Accident _currentAccident;
        private float _cooldownTimer = 0;
        private float _cooldown;
        private bool _isPlaying = false;

        private List<Accident> _accidentsBackup = new List<Accident>();



        #region OVERRIDES
        void Awake()
        {
            _accidentsBackup.AddRange(accidents);
        }
        
        void Start()
        {
            DetermineNextCooldown();
        }

        void Update()
        {
            if (_isPlaying)
            {
                _cooldownTimer += Time.deltaTime;

                if (_cooldownTimer >= _cooldown)
                {
                    StartAccident();
                    _isPlaying = false;
                    //Restart();
                }
            }
        }
        #endregion

        public void StartPlaying()
        {
            _isPlaying = true;
        }

        public void DetermineNextCooldown()
        {
            _cooldown = Random.Range(_minCooldownInSeconds, _maxCooldownInSeconds);
        }

        //Choose a random accident from the list and removes it.
        public void ChooseRandomAccidentFromList()
        {
           
            int randomNum = Random.Range(0, accidents.Count);
            _currentAccident = accidents[randomNum];
            accidents.Remove(_currentAccident);
            if (accidents.Count <= 0) { accidents.AddRange(_accidentsBackup); Debug.Log("Re-adding accidents"); }
        }

        public TrackPerformance ChooseRandomTrack()
        {
            return controller.Tracks[Random.Range(0, controller.TracksData.Length)];
        }

        public void StartAccident()
        {         
            ChooseRandomAccidentFromList();
            _currentAccident.BeginAccident(ChooseRandomTrack());
            controller.CanNuance = false;
            Debug.Log("Starting an accident! " + _currentAccident.Type);
            DetermineNextCooldown();
        }
        public void ResolveAccident()
        {
            DetermineNextCooldown();
            _cooldownTimer = 0;
            _isPlaying = true;
        }
        void ResetAccidents()
        {
            _currentAccident.ResolveAccident();
            _currentAccident = null;
            ResolveAccident();
            accidents.Clear();
            accidents = _accidentsBackup;
        }

        public void Restart()
        {
            _cooldownTimer = 0;
            DetermineNextCooldown();
            ResetAccidents();
        }

        public void Stop()
        {
            Restart();
            _isPlaying = false;

        }
    }

    public enum AccidentType
    {
        bpmChange,
        pitchChange,
        stopTrack
    }
}