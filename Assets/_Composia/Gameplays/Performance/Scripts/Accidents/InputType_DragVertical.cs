using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class InputType_DragVertical : IInputType
    {
        private bool _isValidated = false;

        private float _deltaValueY = 0;
        private float _previousValueY = 0;


        private float minValue = 0;
        private float maxValue = 0;
        //private float sensitivity = 50;            //The higher the this number, the more the mouse will need to move


        public InputType_DragVertical(float min, float max)
        {
            minValue = min;
            maxValue = max;
        }

        public void DetectInput()
        {
            ResolutionInputGamepad();
            ResolutionInputKeyboard();
        }
        public void Restart()
        {
            _isValidated = false;
            _deltaValueY = 0;
            _previousValueY = 0;
        }

        public bool GetValidation()
        {          
            return _isValidated;
        }

        public float GetValue()
        {
            //Debug.Log("delta Y is " + _deltaValueY);
            return _deltaValueY;
        }

        public void ResolutionInputGamepad()
        {
            if (Gamepad.current != null)
            {
                _deltaValueY = Gamepad.current.rightStick.ReadValue().y - _previousValueY;

                if (_deltaValueY > minValue && _deltaValueY < maxValue)
                {
                    _isValidated = true;
                }
            }
        }

        public void ResolutionInputKeyboard()
        {
            if (Mouse.current.leftButton.IsPressed(0))
            {
                if (Mouse.current.leftButton.wasPressedThisFrame) { _previousValueY = Mouse.current.position.ReadValue().y; }

                if (Mouse.current.leftButton.isPressed)
                {
                    _deltaValueY = Mouse.current.position.ReadValue().y - _previousValueY;
                }
                else if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    _deltaValueY = 0;
                    _previousValueY = 0;
                }

                if (_deltaValueY > minValue && _deltaValueY < maxValue)
                {
                    _isValidated = true;
                }
            }
        }

        public void Stop()
        {
            
        }
    }
}