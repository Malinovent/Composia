using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Composia
{
    [CustomEditor(typeof(ActivatableObject_Trigger))]
    public class ActivatableObject_TriggerInspector : Editor
    {
        ActivatableObject_Trigger _target;

        private void OnEnable()
        {
            _target = (ActivatableObject_Trigger)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawButton();
        }

        private void DrawButton()
        {
            if (!_target.triggerObject)
            { 
                if(GUILayout.Button("Find Sooru"))
                {
                    _target.triggerObject = FindObjectOfType<Controls_Platforming>().GetComponent<Collider>();
                }

                EditorGUILayout.HelpBox("This object has no target!", MessageType.Warning);
            }
        }
    }
}