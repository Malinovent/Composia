using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Audio_InstrumentSamplerTestingFunciton))]
public class Audio_InstrumentSamplerFunctionTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Get default editor
        DrawDefaultInspector();

        Audio_InstrumentSamplerTestingFunciton myScript = (Audio_InstrumentSamplerTestingFunciton)target;

        EditorGUILayout.Space();

        //One sounds normal
        EditorGUILayout.LabelField("One sound function", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Play " + myScript._nameEvent + " Sound"))
        {
            myScript.PlayTargetSound();
        }

        EditorGUILayout.Space();

        //All sounds normal
        EditorGUILayout.LabelField("All function", EditorStyles.boldLabel);

        if (GUILayout.Button("Play" + " all Sound"))
        {
            if (myScript._currentCoroutine == null)
                myScript._currentCoroutine = myScript.StartCoroutine(myScript.PlayAllSounds());
        }

        if (GUILayout.Button("Play" + " all Sound Reverse"))
        {
            if (myScript._currentCoroutine == null)
                myScript._currentCoroutine = myScript.StartCoroutine(myScript.PlayAllSoundsReverse());
        }

        if (GUILayout.Button("Stop All Sounds"))
        {
            myScript.StopSounds();
        }

        EditorGUILayout.Space();
    }
}
#endif
