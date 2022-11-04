using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceControllerUI : MonoBehaviour
{
    public Text textBPM;
    public Slider sliderBPM;
    public Slider sliderHealth;

    public GameObject playUI;
    public GameObject loseUI;
    public GameObject winUI;

    private void Start()
    {
        loseUI.SetActive(false);
        sliderHealth.value = sliderHealth.maxValue;
    }

    public void UpdateBPMSlider(int value)
    {
        sliderBPM.value = value;
        textBPM.text = "BPM: " + value.ToString();
    }

    public void HidePlayUI()
    {
        playUI.SetActive(false);
    }

    public void DisplayPlayUI()
    {
        playUI.SetActive(true);
    }

    public void DisplayWinTextUI(string trackName)
    {
        winUI.GetComponentInChildren<Text>().text = "Congratulations! /n, you have performed " + trackName;
        winUI.SetActive(true);
    }

    public void UpdateHealthSlider(float value)
    {
        sliderHealth.value = (int)value;
    }
    public int UpdateBPMText()
    {
        int bpm = (int)sliderBPM.value;
        textBPM.text = "BPM: " + bpm.ToString();
        return bpm;
    }

    public void DisplayLoseUI()
    {
        loseUI.SetActive(true);
    }

    public void HideLoseUI()
    {
        loseUI.SetActive(false);
    }
}
