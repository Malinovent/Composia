using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Audio_InstrumentSamplerMelodyTest))]
public class Audio_InstrumentMelodyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Get default editor
        DrawDefaultInspector();

        Audio_InstrumentSamplerMelodyTest myScript = (Audio_InstrumentSamplerMelodyTest)target;

        EditorGUILayout.Space();

        //One sounds normal
        EditorGUILayout.LabelField("Melody Function", EditorStyles.boldLabel);

        if (GUILayout.Button("Play Melody"))
        {
            myScript.PlayAllMelody();
        }

        if (GUILayout.Button("Stop Melody"))
        {
            myScript.StopMelody();
        }

        EditorGUILayout.Space();
    }
}
#endif