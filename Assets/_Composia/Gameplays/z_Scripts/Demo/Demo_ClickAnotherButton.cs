using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_ClickAnotherButton : MonoBehaviour
{
    public Button button;

    void Awake()
    {
        //if (button == null)
        //    button = GetComponent<Button>();

       
        //Get the button to click on
        if (GameObject.Find("Retour_Arrangement") != null && button == null)
        {
            Debug.Log("DEMO - FOUND THE BUTTON");
            button = GameObject.Find("Retour_Arrangement").GetComponent<Button>();
        }
        else
        {
            Debug.Log("DEMO - WORLD MAP BUTTON NOT FOUND");
        }
    }

    public void Invoke()
    {

    }

    public void ManageDemoExitArrangement()
    {
        if (button != null && button.onClick != null)
        {
            Debug.Log("DEMO - PRESS THE OTHER BUTTON");
            button.onClick.Invoke();
        }
        else if (button != null)
        {
            Debug.Log("DEMO - Hum, what is pressing...");
        }
    }
}

