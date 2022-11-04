using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_MusicManager : MonoBehaviour
{
    //Create the fadein object when needed
    public GameObject _audio_musicManager;

    private void Awake()
    {
        if (GameObject.FindObjectOfType<Audio_MusicManager>() == null)
        {
            Debug.Log("Create music manager");
            DontDestroyOnLoad(Instantiate(_audio_musicManager));
        }
        else
        {
            Debug.Log("Music manager already exist");
        }
    }
}
