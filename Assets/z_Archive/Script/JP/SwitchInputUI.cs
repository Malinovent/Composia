using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class SwitchInputUI : MonoBehaviour, IObserver
    {
        public GameObject[] keyboardUI;
        public GameObject[] gamepadUI;

        //public InputSystem inputs;

        void OnEnable()
        {
            SwitchUI(InputSystem.Instance.IsKeyboard);
        }

        void Awake()
        {
            InputSystem.Instance.AddObserver(this);
            SwitchUI(true);
        }
        public void FindInputSystem()
        {
            //inputs = FindObjectOfType<InputSystem>();
        }
        public void SwitchUI(bool isKeyboard)
        {
            //Debug.Log("Switching to " + isKeyboard);
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
            Debug.Log(gameObject.name + " has Receved Data!");
            if (o is InputSystem)
            {
                SwitchUI((o as InputSystem).IsKeyboard);
            }
        }
    }
}