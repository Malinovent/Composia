using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Composia {
    public class Puzzle : MonoBehaviour, IInteractable
    {
        [SerializeReference]
        public ActivatableObject[] activatables;

        private bool m_isResolved = false;
        private int m_currentIndex = 0;

        public float resolutionDelay = 5;

        public event EventHandler<EventArgs> Resolution;
        public event EventHandler<EventArgs> Cancel;
        public void StartPuzzle()
        {
            InputPuzzle.GetPuzzle(this);
            activatables[m_currentIndex].IsSelected();
        }

        public void TryActivate()
        {
            //Debug.Log("Try activating");
            if (!m_isResolved)
            {
                if (activatables[m_currentIndex].TryActivate())
                {
                    NextActivatable();
                }
            }
        }

        public void Next()
        {
            if (activatables[m_currentIndex] is ISwitchInput)
            {
                ISwitchInput switchInput = (ISwitchInput)activatables[m_currentIndex];
                switchInput.Next();
            }
        }

        public void Previous()
        {
            if (activatables[m_currentIndex] is ISwitchInput)
            {
                ISwitchInput switchInput = (ISwitchInput)activatables[m_currentIndex];
                switchInput.Previous();
            }
        }

        public void NextActivatable()
        {
            m_currentIndex++;

            if (m_currentIndex >= activatables.Length)
            {
                SolvePuzzle();
                return;
            }

            activatables[m_currentIndex].IsSelected();
        }

        public void CancelInteraction()
        {
            Debug.Log("Canceling interaction!");
            foreach (IActivatable i in activatables)
            {
                i.DeActivate();
            }

            OnCancel();
        }

        public void SolvePuzzle()
        {
            m_isResolved = true;
            foreach (IActivatable i in activatables)
            {
                i.StaysActive = true;
            }

            StartCoroutine(Resolve());
        }

        public void Interacting(float timeHeld = 0)
        {
            TryActivate();
        }

        public void StopInteracting()
        {
            CancelInteraction();
            //Switch game modes
        }

        IEnumerator Resolve()
        {
            yield return new WaitForSeconds(resolutionDelay);
            OnResolution();
            InputPuzzle.StopInteracting();
        }

        public void OnResolution()
        {
            if (Resolution != null) { Resolution(this, new EventArgs()); }
        }

        public void OnCancel()
        {
            if (Cancel != null) { Cancel(this, new EventArgs()); }
        }
    }
}