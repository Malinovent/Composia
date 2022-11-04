using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCreator : MonoBehaviour
{
    //Create the fadein object when needed
    public GameObject _fadeInManager;

    private void Awake()
    {
        if (GameObject.FindObjectOfType<FadeInManager>() == null)
        {
            Debug.Log("Create fade in manager");
            DontDestroyOnLoad(Instantiate(_fadeInManager));
        }
        else
        {
            Debug.Log("Fade in manager already exist");
        }
    }
}
