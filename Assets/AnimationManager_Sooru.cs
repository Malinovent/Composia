using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class AnimationManager_Sooru : AnimationManager
    {
        /*
        public void OnEnable()
        {
            SooruEvents.Jump += ChangeAnimationStateOverride;
            SooruEvents.JumpApex += ChangeAnimationStateOverride;
            SooruEvents.JumpFall += ChangeAnimationStateOverride;
            SooruEvents.JumpLand += ChangeAnimationStateOverride;
            SooruEvents.Pow += ChangeAnimationStateOverride;
            SooruEvents.Interact += ChangeAnimationState;
            SooruEvents.Walk += PlayWalkAnim;
            SooruEvents.Idle += ChangeAnimationState;
        }

        public void OnDisable()
        {
            SooruEvents.Jump -= ChangeAnimationStateOverride;
            SooruEvents.JumpApex -= ChangeAnimationStateOverride;
            SooruEvents.JumpFall -= ChangeAnimationStateOverride;
            SooruEvents.JumpLand -= ChangeAnimationStateOverride;
            SooruEvents.Pow -= ChangeAnimationStateOverride;
            SooruEvents.Interact -= ChangeAnimationState;
            SooruEvents.Walk -= PlayWalkAnim;
            SooruEvents.Idle -= ChangeAnimationState;
        }
        */

        public void PlayWalkAnim(float speed, string stateName)
        {
            ChangeAnimationState(stateName, speed);
        }
        public void PlayIdleAnim(string stateName)
        {
            ChangeAnimationState(stateName, 0.5f, 0);
        }


    }
}