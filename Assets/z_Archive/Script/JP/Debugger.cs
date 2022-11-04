using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    float deltaTime = 0;
    int frameCount = 0;
    int fps = 0;

    public Text fpsText;
    public Text delayText;

    private List<float> timesInOrder = new List<float>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        frameCount++;

        if (deltaTime >= 1)
        {
            deltaTime = 0;
            fps = frameCount;
            UpdateText();
            frameCount = 0;
        }
    }

    void UpdateText()
    {
        fpsText.text = fps.ToString();
    }

    void UpdateDelayText()
    { 
    
    }
}
