using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia.Arrangement
{
    public class Accident_BPM : Accident
    {
        private int _desiredBPM;
        private int _startActualBPM;

        [SerializeField]
        private int _minBPMChange = 2;
        [SerializeField]
        private int _maxBPMChange = 4;
        [SerializeField]
        private int _marginOfAcceptableError = 1;

        [SerializeField][Tooltip("The higher the this number, the more the mouse will need to move")]
        private float _bpmChangeSensitivity = 50;            //The higher the this number, the more the mouse will need to move

        private void Start()
        {
            Type = AccidentType.bpmChange;
        }


        // Update is called once per frame
        public new void Update()
        {
            base.Update();
            if (IsActive)
            {
                AdjustBPM(inputType.GetValue() / _bpmChangeSensitivity);
            }
        }

        private void AdjustBPM(float value)
        {
            MyTrack.ChangeBPM((int)value);
            MyTrack.UpdateBPM();
            _startActualBPM = MyTrack.BPM;

            if (Mathf.Abs(MyTrack.BPM - _desiredBPM) <= _marginOfAcceptableError)
            {
                //Restart BPM to start with next note (Improve stopped track)
                ResolveAccident();
            }
        }

        private void DetermineNewBPM()
        {
            int newBPM = MyTrack.BPM + Random.Range(_minBPMChange, _maxBPMChange) * (Random.Range(0, 2) * 2 - 1);
            MyTrack.AdjustBPM(newBPM);
            _startActualBPM = MyTrack.BPM;
        }

        protected override void BeginAccident()
        {
            base.BeginAccident();
            _desiredBPM = MyTrack.BPM;          
            DetermineNewBPM();   
        }

        public override void ResolveAccident()
        {
            base.ResolveAccident();
            MyTrack.AdjustBPM(_desiredBPM);
            MyTrack.RestoreNoteIndex();           
        }

        public override void DeclareInputType()
        {
            inputType = new InputType_DragVertical(_minBPMChange, _maxBPMChange);
        }
    }
}
