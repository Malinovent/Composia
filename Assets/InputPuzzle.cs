using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class InputPuzzle : MonoBehaviour
    {
        [ReadOnly]
        private static Puzzle m_currentPuzzle;
        private void ActivateSelected()
        {
            if (m_currentPuzzle != null)
            {
                m_currentPuzzle.Interacting(0);
            }
            else
            {
                Debug.LogWarning("No item is selected!");
            }
        }

        public void GetConfirmationButton(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) { ActivateSelected(); }
        }

        public void GetCancelButton(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) { StopInteracting(); }
        }

        public void GetNextButton(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) { m_currentPuzzle.Next(); }
        }

        public void GetPreviousButton(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) { m_currentPuzzle.Previous(); }
        }

        public static void GetPuzzle(Puzzle puzzle)
        {
            m_currentPuzzle = puzzle;
        }

        public static void StopInteracting()
        {
            m_currentPuzzle = null;
            GameModeManager.ToggleMode(GameMode.Exploration);
        }

    }
}