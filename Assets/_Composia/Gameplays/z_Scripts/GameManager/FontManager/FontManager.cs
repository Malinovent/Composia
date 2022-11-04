using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FontManager: MonoBehaviour
{
    public TMP_FontAsset _font;
    public Font _fontUI;
    private string _previousScene;

    private void Start()
    {
        UpdateAllTextMeshProInScene();
        UpdateAllTextUIInScene();
    }

    public void Update()
    {
        string _gameScene = SceneManager.GetActiveScene().name;

        //Update txt once
        if (_previousScene != _gameScene)
        {
            Debug.Log("Update font in scene");

            UpdateAllTextMeshProInScene();
            UpdateAllTextUIInScene();
            _previousScene = _gameScene;
        }
    }

    public void UpdateAllTextMeshProInScene()
    {
        TextMeshProUGUI[] _allTxtMeshPro = GameObject.FindObjectsOfType<TextMeshProUGUI>();

        if (_allTxtMeshPro.Length > 0)
        {
            foreach (TextMeshProUGUI _text in _allTxtMeshPro)
            {
                //Update font if needed
                if (_text.font != _font)
                    _text.font = _font;
            }
        }
    }

    public void UpdateAllTextUIInScene()
    {
        Text[] _allUIText = GameObject.FindObjectsOfType<Text>();

        if (_allUIText.Length > 0)
        {
            foreach (Text _text in _allUIText)
            {
                //Update font if needed
                if (_text.font != _fontUI)
                    _text.font = _fontUI;
            }
        }
    }

    //DONT FORGET TO USE FONT MANAGER UPDATE WHENEVER THERE IS SOMETHING THAT CANNOT BE UPDATED
}
