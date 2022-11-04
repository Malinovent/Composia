using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia.Arrangement
{
    public abstract class Accident : MonoBehaviour
    {
        private AccidentType type;
        private bool _isResolved = true;
        private TrackPerformance _myTrack;

        [SerializeField]
        SimpleUIManager _uiManager;

        public IInputType inputType;

        public abstract void DeclareInputType();

        private bool isActive = false;

        #region GETTERS
        public bool IsResolved 
        { 
            get => _isResolved;
            set
            {
                _isResolved = value;
            }
        }

        public TrackPerformance MyTrack { get => _myTrack; set => _myTrack = value; }
        public AccidentType Type { get => type; protected set => type = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        #endregion

        private void Awake()
        {
            DeclareInputType();

            if (_uiManager == null) { _uiManager = GetComponent<SimpleUIManager>(); }
            _uiManager.DeActivateAll();
        }
        public void Update()
        {
            if (isActive)
            {
                ResolutionInput();
            }
        }
        public virtual void BeginAccident(TrackPerformance track)
        {
            PerformanceController.Instance.IsAccident = true;
            MyTrack = track;
            BeginAccident();
        }

        protected virtual void BeginAccident()
        {
            IsResolved = false;
            IsActive = true;
            DisplayUI();
        }

        public virtual void ResolveAccident()
        {
            IsResolved = true;
            IsActive = false;
            PerformanceController.Instance.IsAccident = false;
            PerformanceController.Instance.CanNuance = true;
            PerformanceControllerAccidents.Instance.ResolveAccident();
            StopDisplayUI();
            //Debug.Log("Resolving accident");
            inputType.Restart();
        }

        public virtual void DisplayUI()
        {
            _uiManager.ActivateElement(MyTrack.TrackIndex);
        }

        public virtual void StopDisplayUI()
        {
            _uiManager.DeActivateElement(MyTrack.TrackIndex);
        }

        public void ResolutionInput()
        {
            if (!IsResolved)
            {
                inputType.DetectInput();

                if (inputType.GetValidation())
                {
                    Debug.Log("Validated!");
                    ResolveAccident();
                }   
            }
        }

    }
}