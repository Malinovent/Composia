using UnityEngine;
using UnityEditor;

namespace Composia.Arrangement
{
    [CustomEditor(typeof(SegmentReelsController))]
    public class SegmentReelsControllerInspector : Editor
    {
        private SegmentReelsController _myTarget;

        private void OnEnable()
        {

            _myTarget = (SegmentReelsController)target;
            _myTarget.FindAllSegmentPaths();
            //InitStyles();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            using (new EditorGUILayout.HorizontalScope())
            {

                if (GUILayout.Button("Find Segment Paths"))
                {
                    _myTarget.FindAllSegmentPaths();
                }

                if (GUILayout.Button("Load Segments"))
                {
                    _myTarget.LoadAllSegments();
                }
            }
        }
    }
}