using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia.Arrangement
{
    public class Accident_StoppedTrack : Accident
    {

        public int numberOfInputs = 15;    
        private void Start()
        {
            Type = AccidentType.stopTrack;
        }
        protected override void BeginAccident()
        {
            base.BeginAccident();
            MyTrack.PausePlaying();
            
        }
        public override void ResolveAccident()
        {
            base.ResolveAccident();
            Debug.Log("Resolved Stopped Tack!");
            MyTrack.RestoreNoteIndex();    
        }

        public override void DeclareInputType()
        {         
            inputType = new InputType_RepeatedPress(numberOfInputs);
            //numberOfInputs = (inputType as InputType_RepeatedPress)._numberOfInputs;
            //Debug.Log("Declaring input type" + inputType);
        }
    }
}
