using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class Controls_Puzzle : Controls
    {
        [SerializeField] private InputPuzzle puzzleComponent;

        #region Overrides

        private void Start()
        {
            if (!puzzleComponent) { puzzleComponent = GetComponent<InputPuzzle>(); }
        }

        #endregion

        public override void InitializeControls()
        {
            if (puzzleComponent == null) { return; }

            controls.Puzzle.Cancel.performed += Cancel;
            controls.Puzzle.Confirmation.performed += Confirmation;
            controls.Puzzle.Next.performed += Next;
            controls.Puzzle.Previous.performed += Previous;
        }

        public override void UnInitializeControls()
        {
            if (puzzleComponent == null) { return; }

            controls.Puzzle.Cancel.performed -= Cancel;
            controls.Puzzle.Confirmation.performed -= Confirmation;
            controls.Puzzle.Next.performed -= Next;
            controls.Puzzle.Previous.performed -= Previous; 
        }

        public override void EnableControls()
        {
 
            if (!controls.Puzzle.enabled)
            {
                Debug.Log("Enabling Puzzle Controls");
                controls.Puzzle.Enable();
            }
        }

        public override void DisableControls()
        {
            if (controls.Puzzle.enabled)
            {
                controls.Puzzle.Disable();
            }
        }

        public void Cancel(InputAction.CallbackContext ctx)
        {
            puzzleComponent.GetCancelButton(ctx);
        }

        public void Confirmation(InputAction.CallbackContext ctx)
        {
            puzzleComponent.GetConfirmationButton(ctx);
        }

        public void Next(InputAction.CallbackContext ctx)
        {
            puzzleComponent.GetNextButton(ctx);
        }

        public void Previous(InputAction.CallbackContext ctx)
        {
            puzzleComponent.GetPreviousButton(ctx);         
        }

    }
}