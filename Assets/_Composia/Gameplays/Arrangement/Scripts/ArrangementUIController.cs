using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangementUIController : Singleton<ArrangementUIController>
{
    public Text nameText;
    public Text timeSignature;
    public Image levelImage;

    private void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void SetItems(string name, int timeSignature)
    {
        nameText.text = name;
        this.timeSignature.text = timeSignature.ToString() + " /4";
        
    }
}
