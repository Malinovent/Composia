using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn_NewSceneFadeAway : MonoBehaviour
{
    private FadeInManager _fadeInManager { get { return GameObject.FindObjectOfType<FadeInManager>(); } }

    private void Start()
    {
        if (_fadeInManager._canvasGroup.alpha > 0f)
        {
            Debug.Log("Fade away new Scene");
            StartCoroutine(_fadeInManager.ManageFadeIn(true));
        }
    }
}
