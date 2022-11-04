using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Notepad))]
public class NotepadInspector : Editor
{
    Notepad _notepad;

    private const int AREA_MIN_HEIGHT = 30;
    private const int FONT_SIZE = 18;

    private GUIStyle textAreaStyle;
    private GUIStyle buttonStyle;

    List<bool> editBool = new List<bool>();

    private void OnEnable()
    {
        _notepad = (Notepad)target;

        textAreaStyle = new GUIStyle();
        textAreaStyle.alignment = TextAnchor.MiddleCenter;
        textAreaStyle.normal.textColor = Color.white;
        textAreaStyle.fontStyle = FontStyle.Italic;
        textAreaStyle.fontSize = FONT_SIZE;
        textAreaStyle.wordWrap = true;

        //buttonStyle = new GUIStyle();
        //buttonStyle.fixedWidth = 20;


        if (_notepad.notes.Count == 0)
        {
            _notepad.notes.Add("Add Note Here");
            AddBoolToList();
        }
        else 
        {
            for (int i = 0; i < _notepad.notes.Count; i++)
            {
                AddBoolToList();
            }
        }
    }

    private void AddBoolToList()
    {
        bool newBool = false;
        editBool.Add(newBool);
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        using (var h = new EditorGUILayout.VerticalScope())
        {
            DrawNotes();

            using (var i = new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("+", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                {
                    
                    _notepad.notes.Add("Insert New Note");
                    AddBoolToList();
                }
                GUILayout.Space(20);

            }
        }
        
    }

    private bool DrawNote(int index)
    {

        using (var j = new EditorGUILayout.VerticalScope())
        {
            using (var h = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("▼", EditorStyles.miniButtonLeft, GUILayout.MaxWidth(20)))
                {
                    editBool[index] = !editBool[index];
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.MaxWidth(20)))
                {
                    _notepad.notes.Remove(_notepad.notes[index]);
                    return true;
                }

                GUILayout.Space(20);
            }

            if (editBool[index] == true)
            {
                _notepad.notes[index] = GUILayout.TextArea(_notepad.notes[index], EditorStyles.textArea);                
            }
            else
            {
                GUILayout.Label(_notepad.notes[index], textAreaStyle);              
            }
        }

        return false;
                       
    }

    private void DrawNotes()
    {
        for (int i = 0; i < _notepad.notes.Count; i++)
        {
            if (DrawNote(i)) { break; }
            if (_notepad.notes.Count >= 2) { GUILine(); }
        }
    }

    void GUILine(int i_height = 1)
    {
        GUILayout.Space(25);
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

    }
}
