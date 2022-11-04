using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previewer : MonoBehaviour
{
    public GameObject[] previewObjects;

    [SerializeField] private bool m_startActive = false;
    [SerializeField] private int m_currentIndex = 0;

    public int CurrentIndex 
    { 
        get => m_currentIndex;
        set
        {
            m_currentIndex = value;
            if(m_currentIndex >= previewObjects.Length) { m_currentIndex = 0; }
            if(m_currentIndex < 0) { m_currentIndex = previewObjects.Length - 1; }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(m_startActive)
        {
            DeactiveObjects(CurrentIndex);
        }
        else 
        { 
            DeactiveObjects(); 
        }
    }

    public void DeactiveObjects(int exceptionIndex = - 1)
    {
        foreach (GameObject go in previewObjects)
        {
            go.SetActive(false);
        }

        if (exceptionIndex != -1)
        { 
            if(exceptionIndex > previewObjects.Length) { Debug.LogWarning("Object index is too high and out of range"); return; }
            previewObjects[exceptionIndex].SetActive(true);
        }
    }

    public void Activate()
    {
        DeactiveObjects(CurrentIndex);
    }

    public void ActivateNext()
    {
        CurrentIndex++;
        Activate();
    }

    public void ActivatePrevious()
    {
        CurrentIndex--;
        Activate();
    }

}
