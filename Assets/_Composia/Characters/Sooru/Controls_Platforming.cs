using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class Controls_Platforming : Controls
    {

        [SerializeField] private InputMovement moveComponent;
        [SerializeField] private InputJump jumpComponent;
        [SerializeField] private InputPow powComponent;
        [SerializeField] private InputInteract interactComponent;


        #region Overrides


        private void Start()
        {
            if (!moveComponent) { moveComponent = GetComponent<InputMovement>(); }
            if (!jumpComponent) { jumpComponent = GetComponent<InputJump>(); }
            if (!interactComponent) { interactComponent = GetComponent<InputInteract>(); }
            if (!powComponent) { powComponent = GetComponent<InputPow>(); }  
        }
        #endregion

        public override void InitializeControls()
        {        
            controls.Exploration.Jump.started += Jump;
            controls.Exploration.Jump.canceled += Jump;
            controls.Exploration.Pow.performed += Pow;
            controls.Exploration.Interact.started += Interact;
            controls.Exploration.Interact.canceled += Interact;
            controls.Exploration.Move.performed += Move;
            controls.Exploration.Move.canceled += Move;
            controls.Exploration.Cancel.performed += Cancel;
        }

        public override void UnInitializeControls()
        {
            controls.Exploration.Jump.started -= Jump;
            controls.Exploration.Jump.canceled -= Jump;
            controls.Exploration.Pow.performed -= Pow;
            controls.Exploration.Interact.started -= Interact;
            controls.Exploration.Interact.canceled -= Interact;
            controls.Exploration.Move.performed -= Move;
            controls.Exploration.Move.canceled -= Move;
            controls.Exploration.Cancel.performed -= Cancel;
        }

        public override void EnableControls()
        {
            Debug.Log("Enabling Exploration Controls");
            controls.Exploration.Enable();
        }

        public override void DisableControls()
        {
            if (controls != null)
            {
                controls.Exploration.Disable();
            }
        }

        public void Pow(InputAction.CallbackContext ctx)
        {
            if (!powComponent) { Debug.LogWarning("There is no POW component!"); return; }
            if (!ctx.performed) { return; }

            powComponent.GetInput();
        }

        public void Jump(InputAction.CallbackContext ctx)
        {
            if (!jumpComponent) { Debug.LogWarning("There is no JUMP component!"); return; }

            jumpComponent.GetInput(ctx.started);
        }

        public void Move(InputAction.CallbackContext ctx)
        {            
            if (!moveComponent) { Debug.LogWarning("There is no MOVE component!"); return; }

            Vector2 inputValue = ctx.ReadValue<Vector2>();
            moveComponent.GetInput(inputValue);
        }

        public void Interact(InputAction.CallbackContext ctx)
        {
            if (!interactComponent) { Debug.LogWarning("There is no INTERACT component!"); return; }

            interactComponent.GetInput(ctx);
        }

        //Leave and return to exploration mode
        public void Cancel(InputAction.CallbackContext ctx)
        { 
            
        }

        //Pause the game and open the menu
        public void Pause(InputAction.CallbackContext ctx)
        { 
            
        }


        #region Events

        #endregion
    }
}