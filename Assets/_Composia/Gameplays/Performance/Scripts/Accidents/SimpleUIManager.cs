using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SimpleUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiObjects;
    public void SetTextUI(string text, int index = 0)
    {
        if (index > uiObjects.Length) { return; }
        TMP_Text textObject = uiObjects[index].GetComponent<TMP_Text>();
        if (textObject == null) { Debug.LogWarning("There is no text component on this object!"); return; }

        textObject.text = text;
    }

    public void SetTextUI(int intValue)
    {
        if (uiObjects.Length <= 0) { return; }
        TMP_Text textObject = uiObjects[0].GetComponent<TMP_Text>();
        if (textObject == null) { Debug.LogWarning("There is no text component on this object!"); return; }

        textObject.text = intValue.ToString();
    }
    public void ActivateAll()
    {
        if(uiObjects == null) { return; }
        foreach (GameObject go in uiObjects)
        {
            go.SetActive(true);
        }
    }

    public void DeActivateAll()
    {
        if (uiObjects == null) { return; }
        foreach (GameObject go in uiObjects)
        {
            go.SetActive(false);
        }
    }

    public void ActivateElement()
    {
        if (uiObjects[0] == null) { return; }
        uiObjects[0].SetActive(true);
    }

    public void ActivateElement(int index)
    {
        if(index > uiObjects.Length) { return; }
        uiObjects[index].SetActive(true);
    }

    public void DeActivateElement(int index)
    {
        if (index > uiObjects.Length) { return; }
        uiObjects[index].SetActive(false);
    }

    public void DeActivateElement()
    {
        if (uiObjects[0] == null) { return; }
        uiObjects[0].SetActive(false);
    }
}
