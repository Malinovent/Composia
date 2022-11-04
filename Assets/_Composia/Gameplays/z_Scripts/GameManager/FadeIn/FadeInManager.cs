using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FadeInManager : MonoBehaviour
{
    //Manage fade in fade out between scene
    public CanvasGroup _canvasGroup;
    public TextMeshProUGUI _loadingProgressionTxt;

    public float _fadeSpeed;

    //Manage fade in
    public IEnumerator ManageFadeIn(bool _isFadeOut)
    {
        if (_isFadeOut) //Fade out
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
                Mathf.Clamp01(_canvasGroup.alpha);
                yield return null;
            }
        }
        else //Fade in
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += _fadeSpeed * Time.deltaTime;
                Mathf.Clamp01(_canvasGroup.alpha);
                yield return null;
            }
        }
    }

    //Manage loading progression
    public void LoadingProgression(AsyncOperation _operation)
    {
        float _progressVal = Mathf.Clamp01(_operation.progress/0.9f);
        _loadingProgressionTxt.text = "Loading " + Mathf.Round(_progressVal * 100) + "%";
    }
}
