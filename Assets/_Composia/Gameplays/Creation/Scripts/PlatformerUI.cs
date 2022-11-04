using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerUI : Singleton<PlatformerUI>
{
    public GameObject EndUI;

    public float endUIDelayInSeconds = 1.5f;

    private void Start()
    {
        EndUI.SetActive(false);
    }

    public void BeginEndUI()
    {
        StartCoroutine(DisplayEndUICoroutine());
    }
    private IEnumerator DisplayEndUICoroutine()
    {
        yield return new WaitForSeconds(endUIDelayInSeconds);
        DisplayEndUI();
    }

    //Display End Menu
    private void DisplayEndUI()
    {
        EndUI.SetActive(true);
    }
}
