using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFontManager : MonoBehaviour
{
    //Create the fadein object when needed
    public GameObject _fontManager;

    private void Awake()
    {
        if (GameObject.FindObjectOfType<FontManager>() == null)
        {
            Debug.Log("Create font manager");
            DontDestroyOnLoad(Instantiate(_fontManager));
        }
        else
        {
            Debug.Log("FontManager already exist");
        }
    }
}
