using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Composia
{
    //[CustomEditor(typeof(PlayerController_Creation))]
    public class PlayerCreationInspector : Editor
    {

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


        #region STYLES
        private GUIStyle _titleStyle;
        private PlayerController_Creation _myTarget;

        #endregion

        #region OVERRIDES

        private void OnEnable()
        {

            _myTarget = (PlayerController_Creation)target;
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
        }
        #endregion

        #region DRAWERS
        private void DrawInventory()
        {
           
        }

        #endregion
    }
}