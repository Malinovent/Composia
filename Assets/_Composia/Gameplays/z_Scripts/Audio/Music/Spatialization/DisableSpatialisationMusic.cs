using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpatialisationMusic : MonoBehaviour
{
    private AkAmbient _akAmbient { get { return GetComponent<AkAmbient>(); } }

    private void OnDisable()
    {
        if (Application.isPlaying)
            _akAmbient.Stop(0);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
            _akAmbient.Stop(0);
    }
}
