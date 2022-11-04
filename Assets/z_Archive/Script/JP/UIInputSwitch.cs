using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class UIInputSwitch : MonoBehaviour, IObserver
    {
        public GameObject[] keyboardUI;
        public GameObject[] gamepadUI;

        public IObserver inputObserver;

        void Start()
        {
            CompositionInputSystem.Instance.AddObserver(this);
            SwitchUI(true);
        }
        public void SwitchUI(bool isKeyboard)
        {
            if (isKeyboard)
            {
                TurnOnUI(keyboardUI);
                TurnOffUI(gamepadUI);
            }
            else
            {
                TurnOnUI(gamepadUI);
                TurnOffUI(keyboardUI);
            }
        }

        public void TurnOnUI(GameObject[] uiArray)
        {
            foreach (GameObject go in uiArray)
            {
                go.SetActive(true);
            }
        }

        public void TurnOffUI(GameObject[] uiArray)
        {
            foreach (GameObject go in uiArray)
            {
                go.SetActive(false);
            }
        }

        public void UpdateData(IObservable o)
        {
            if (o is CompositionInputSystem)
            {
                SwitchUI((o as CompositionInputSystem).IsKeyboard);
            }
        }
    }
}