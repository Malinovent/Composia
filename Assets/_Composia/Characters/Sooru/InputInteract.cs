using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class InputInteract : MonoBehaviour
    {
        InteractiveObject currentInteractable;

        private bool _isInputHeld = false;
        private float _timeHeld = 0;

        public bool IsInputHeld 
        { 
            get => _isInputHeld; 
            set 
            { 
                _isInputHeld = value;

                if (!IsInputHeld) { Release(); }
            } 
        }

        public InteractiveObject CurrentInteractable 
        { 
            get => currentInteractable;
            set 
            {
                InteractiveObject io = currentInteractable;
                currentInteractable = value;
                if (currentInteractable == null && io != null)
                {
                    io.CanInteract = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentInteractable = other.GetComponent<InteractiveObject>();
            Debug.Log("New interactable " + other.name);
        }

        private void Update()
        {
            if (IsInputHeld)
            {
                _timeHeld += Time.deltaTime;
                Interact(_timeHeld);
            }
        }

        public void GetInput(bool isHeld)
        {
            IsInputHeld = isHeld;
        }

        public void GetInput(InputAction.CallbackContext ctx)
        {
            IsInputHeld = ctx.started;
            if (ctx.canceled)
            {
                CurrentInteractable.CanInteract = true;
            }
        }

        void Interact(float timeHeld)
        {
            if (CurrentInteractable != null)
            {
                CurrentInteractable.Interacting(timeHeld);
            }
        }

        void Release()
        {
            _timeHeld = 0;
        }
    }
}