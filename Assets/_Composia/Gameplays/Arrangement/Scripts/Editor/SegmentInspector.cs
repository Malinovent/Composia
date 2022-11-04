using UnityEditor;
using UnityEngine;

namespace Composia.Arrangement
{
    [CustomEditor(typeof(Segment))]
    public class SegmentInspector : Editor
    {
        private Segment _target;

        private void OnEnable()
        {
            _target = (Segment)target;
        }

        private void OnDisable()
        {

        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Validate", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight)))
            {
                _target.ValidateSegment();
            }

            if (GUILayout.Button("Retrograde", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight)))
            {
                _target.Retrograde();
            }

            if (GUILayout.Button("Inverse", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight)))
            {
                _target.Inverse();
            }
        }
    }
}