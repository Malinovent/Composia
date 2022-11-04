using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontManagerUpdate : MonoBehaviour
{
    private FontManager _fontManager { get { return GameObject.FindObjectOfType<FontManager>(); } }

    private void OnEnable()
    {
        if (_fontManager != null)
        {
            _fontManager.UpdateAllTextMeshProInScene();
            _fontManager.UpdateAllTextUIInScene();
        }
    }
}
