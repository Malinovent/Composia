using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAdditive : MonoBehaviour
{
    public string _sceneName;

    //private void Awake()
    //{
    //    Debug.Log("Load additive scene: " + _sceneName);
    //    SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
    //}

    private void OnEnable()
    {
        Debug.Log("Load additive scene: " + _sceneName);
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync(_sceneName);
    }
}
