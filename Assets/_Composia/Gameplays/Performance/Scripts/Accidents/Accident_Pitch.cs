using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia.Arrangement
{
    public class Accident_Pitch : Accident
    {
        private int _desiredPitch;
        private int _startActualPitch;

        [SerializeField]
        private int _minPitchChange = 2;
        [SerializeField]
        private int _maxPitchChange = 4;
        [SerializeField]
        private int _marginOfAcceptableError = 1;

        private void Start()
        {
            Type = AccidentType.pitchChange;    
        }


        // Update is called once per frame
        public new void Update()
        {
            if (IsActive)
            {
                AdjustPitch(inputType.GetValue());
            }
        }

        private void AdjustPitch(float value)
        {
            MyTrack.Pitch = (int)(_startActualPitch + value);

            if (Mathf.Abs(MyTrack.Pitch - _desiredPitch) <= _marginOfAcceptableError)
            {
                ResolveAccident();
            }
        }

        private void DetermineNewPitch()
        {
            MyTrack.Pitch += Random.Range(_minPitchChange, _maxPitchChange) * (Random.Range(0,2)*2 - 1);
            _startActualPitch = MyTrack.Pitch;
        }

        protected override void BeginAccident()
        {
            base.BeginAccident();
            _desiredPitch = MyTrack.Pitch;
            Debug.Log("Desired pitch is " + _desiredPitch);
            DetermineNewPitch();   
        }

        public override void ResolveAccident()
        {
            base.ResolveAccident();
            MyTrack.Pitch = _desiredPitch;
            Debug.Log("Resolved! PitchChange");
            
        }

        public override void DeclareInputType()
        {
            inputType = new InputType_DragVertical(_minPitchChange, _maxPitchChange);
            Debug.Log("Input type is:" + inputType);
        }
    }
}
