using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class InputType_RepeatedPress : IInputType
    {
        public int _numberOfInputs = 15;
        private float _inputCounter = 0;

        private bool _isValidated = false;
      
        public InputType_RepeatedPress(int numOfInputs)
        {
            _numberOfInputs = numOfInputs;
            //Debug.Log("Number of inputs required is " + _numberOfInputs);
        }

        public void DetectInput()
        {
            ResolutionInputGamepad();
            ResolutionInputKeyboard();
        }
        public void ResolutionInputGamepad()
        {
            if (Gamepad.current != null)
            {
                //Input is south button -> Change this is input is being changed
                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    _inputCounter++;
                    if (_inputCounter > _numberOfInputs)
                    {
                        //Debug.Log("Resolved input on gamepad at input count " + _inputCounter + " / " + _numberOfInputs);
                        _isValidated = true;
                    }
                }
            }    
        }

        public void ResolutionInputKeyboard()
        {
            if (Mouse.current != null)
            {
                //Input is left mouse button -> Change this is input is being changed
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    _inputCounter++;
                    if (_inputCounter > _numberOfInputs)
                    {
                        _isValidated = true;
                    }
                }
            }
        }

        public void Restart()
        {
            _isValidated = false;
            _inputCounter = 0;
        }
        public bool GetValidation()
        {
            Debug.Log("Returning validating: " + _isValidated);
            return _isValidated;
        }

        public float GetValue()
        {
            return _numberOfInputs;
        }
    }
}