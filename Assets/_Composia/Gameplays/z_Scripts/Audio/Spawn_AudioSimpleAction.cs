using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_AudioSimpleAction : MonoBehaviour
{
    //Create the fadein object when needed
    public GameObject _audio_SimpleAction;

    private void Awake()
    {
        if (GameObject.FindObjectOfType<Audio_SimpleAction>() == null)
        {
            Debug.Log("Create audioSimpleAction");
            DontDestroyOnLoad(Instantiate(_audio_SimpleAction));
        }
        else
        {
            Debug.Log("Audio simple action already exist");
        }
    }
}
