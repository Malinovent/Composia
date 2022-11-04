using System;
using UnityEngine;
using System.Collections.Generic;

namespace Composia
{
    public class InteractiveObject : MonoBehaviour, IInteractable
    {

        public event EventHandler<EventArgs> Interact_Success;
        public event EventHandler<EventArgs> Interact_Stop;
        public event EventHandler<EventArgs> Interact_Fail;
        public event EventHandler<EventArgs> Interact_ReachedMax;

        [SerializeField] private float activationTime = 0;
        [SerializeField][Tooltip("Keep 0 for infinite")][Range(0, 20)] 

        private int maxNumOfInteractions = 0;
        private int m_currentInteractions = 0;

        public List<ActivatableObject> activatables = new List<ActivatableObject>();

        private bool m_canInteract = true;
        public bool CanInteract 
        { 
            get => m_canInteract;
            set 
            { 
                m_canInteract = value;
                if (m_currentInteractions >= maxNumOfInteractions && maxNumOfInteractions != 0)
                {
                    m_canInteract = false;
                    OnReachedMax();
                }
            }
        }    
        public void Interacting(float timePassed = 0)
        {
            if (!CanInteract) { return; }
            
            if (timePassed > activationTime)
            {
                OnInteractSuccess();
            }           
        }
        public virtual void StopInteracting()
        {
            OnInteractStop();
        }

        public void ResetInteractions()
        {
            m_currentInteractions = 0;
        }

        public void OnInteractSuccess()
        {
            if (Interact_Success != null) { Interact_Success(this, new EventArgs()); }

            if (activatables != null)
            {
                foreach (IActivatable activatable in activatables)
                {
                    activatable.TryActivate();
                }
            }

            m_currentInteractions++;
            CanInteract = false;
        }

        public void OnInteractStop()
        {
            if (Interact_Stop != null) { Interact_Stop(this, new EventArgs()); }
        }

        public void OnInteractFail()
        {
            if (Interact_Fail != null) { Interact_Fail(this, new EventArgs()); }
        }

        public void OnReachedMax()
        {
            if (Interact_ReachedMax != null) { Interact_ReachedMax(this, new EventArgs()); }
        }

    }
}