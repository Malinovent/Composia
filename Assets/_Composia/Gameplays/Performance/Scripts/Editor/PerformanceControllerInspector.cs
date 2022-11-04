using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Composia.Arrangement
{
    [CustomEditor(typeof(PerformanceController))]
    public class PerformanceControllerInspector : Editor
    {
        private PerformanceController _myTarget;

        //public string[] trackStrings = new string[4];


        #region STYLES
        private void InitStyles()
        {
            _titleStyle = new GUIStyle();
            _titleStyle.alignment = TextAnchor.MiddleCenter;
            _titleStyle.fontSize = 16;

            Texture2D titleBG = (Texture2D)Resources.Load("Color_Bg");
            Font titleFont = (Font)Resources.Load("Oswald-Regular");
            _titleStyle.normal.background = titleBG;
            _titleStyle.normal.textColor = Color.white;
            _titleStyle.font = titleFont;

            GUISkin skin = (GUISkin)Resources.Load("LevelCreatorSkin");
            _titleStyle = skin.label;
        }

        private GUIStyle _titleStyle;

        #endregion

        #region OVERRIDES

        private void OnEnable()
        {

            _myTarget = (PerformanceController)target;
            InitStyles();
        }

        private void OnDisable()
        {
            
        }

        private void OnDestroy()
        {
            
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawLoadTracks();
        }
        #endregion

        private void LoadTrack(int index)
        { 
        
        }

        #region DRAWERS

        private void DrawLoadTracks()
        {
            for (int i = 0; i < _myTarget.trackNames.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _myTarget.trackNames[i] = EditorGUILayout.TextField(_myTarget.trackNames[i]);
                DrawLoadButton(i);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawLoadButton(int index)
        {
            if (GUILayout.Button("Load Track " + (index + 1)))
            {
                //_myTarget.TracksData[index] = SaveLoad.LoadTrack(saveFile);
                _myTarget.LoadTrack(index);
            }
        }
        #endregion
    }
}