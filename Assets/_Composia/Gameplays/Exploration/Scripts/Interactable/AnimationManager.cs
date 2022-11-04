using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private string currentState;
        public Animator Animator { get => _animator; set => _animator = value; }

        private bool _canOverride = true;
        private void Start()
        {
            if (!Animator) { Animator = GetComponent<Animator>(); }
        }

        private void Update()
        {
            CheckOverride();
        }

        private void CheckOverride()
        {
            if (!_canOverride)
            {
                if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    _canOverride = true;
                }
            }
        }

        public void ChangeAnimationState(string newState)
        {
            if (currentState == newState || !_canOverride) { return; }

            Animator.Play(newState);

            currentState = newState;
        }

        public void ChangeAnimationState(string newState, float normalizedTime, int layer)
        {
            if (currentState == newState || !_canOverride) { return; }

            Animator.Play(newState, layer, normalizedTime);

            currentState = newState;
        }

        public void ChangeAnimationStateDontOverride(string newState)
        {
            ChangeAnimationState(newState);
            _canOverride = false;
        }

        public void ChangeAnimationStateOverride(string newState)
        {
            _canOverride = true;
            ChangeAnimationStateDontOverride(newState);
        }
        public void ChangeAnimationState(string newState, float speed)
        {
            Animator.SetFloat("Speed", speed);
            ChangeAnimationState(newState);
        }

    }
}
