using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace Composia
{
    [CustomEditor(typeof(AnimationManager), true)]
    public class AnimationManagerInspector : Editor
    {
        AnimationManager _target;

        private List<AnimatorState> states = new List<AnimatorState>();
        private void OnEnable()
        {
            _target = (AnimationManager)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();


            DrawFindAnimator();
            DrawFindAnimations();
            DrawAnimationStateNames();
        }

        private void DrawAnimationStateNames()
        {
            if(states.Count <= 0) { return; }
            foreach (AnimatorState state in states)
            {
                EditorGUILayout.LabelField(state.name);
            }
        }
        public void DrawFindAnimations()
        {
            if (_target.Animator)
            {
                if (GUILayout.Button("Refresh Animations"))
                {
                    states = EditorUtils.GetAnimatorStateInfo(_target.Animator);
                }
            }
        }

        public void DrawFindAnimator()
        {
            if (!_target.Animator)
            {
                if (GUILayout.Button("Find Animator"))
                {
                    FindAnimator();
                }
            }
        }

        public void FindAnimator()
        {
            _target.Animator = _target.GetComponentInChildren<Animator>();
        }

    }
}