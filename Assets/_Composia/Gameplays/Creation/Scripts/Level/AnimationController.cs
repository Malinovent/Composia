using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{ 
    public class AnimationController : MonoBehaviour
    {

        public Animator animator;
        public float animationSpeedFactor = 1;

        void Awake()
        {
            if (animator == null) { animator.GetComponentInChildren<Animator>(); }
        }

        void Start()
        {
            SetAnimationSpeed();
        }

        public void SetAnimation(string anim)
        {
            animator.Play(anim);
        }

        public void SetAnimationSpeed()
        {
            animator.speed *= TimeManager.Instance.BPS * animationSpeedFactor;
            //Debug.Log(animator.speed);
        }
    }
}