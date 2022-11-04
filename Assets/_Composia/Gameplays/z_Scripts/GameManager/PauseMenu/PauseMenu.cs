using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{    
    private Audio_SimpleAction _audio_SimpleAction { get { return GameObject.FindObjectOfType<Audio_SimpleAction>(); } }
    
    private Menu_Option _menuOption;

    //Manage the pause menu from here
    public GameObject _pauseObject;

    public void _ManagePauseMenu()
    {
        //Prevent unpause pause menu if option menu up
        bool _isNotInOption = CheckIfOptionMenuImpactPause();
     
        //Manage pause game
        if (_pauseObject.activeSelf && _isNotInOption)
        {
            //Unpause
            Debug.Log("Unpause Game");
            _pauseObject.SetActive(false);
            Time.timeScale = 1f;

            //Audio
            _audio_SimpleAction.PlayWwiseEvent(_audio_SimpleAction._events["Pause_menu"]);
        }
        else if (!_pauseObject.activeSelf && _isNotInOption)
        {
            //Pause
            Debug.Log("Pause Game");
            _pauseObject.SetActive(true);
            Time.timeScale = 0f;

            //Audio
            _audio_SimpleAction.PlayWwiseEvent(_audio_SimpleAction._events["Pause_menu"]);
        }
    }

    private bool CheckIfOptionMenuImpactPause()
    {
        if (_menuOption == null)
        {
            if (GameObject.FindObjectOfType<Menu_Option>() != null)
            {
                _menuOption = GameObject.FindObjectOfType<Menu_Option>();
                return CheckIfOptionMenuImpactPause();
            }
        }
        else
        {
            GameObject _canvasOption = _menuOption._canvasOption;

            if (_canvasOption.activeSelf)
            {
                //Debug.Log("Do not stop the game, this is a warning...");

                //Cannot unpause the game while still being in option menu
                return false;
            }
        }

        return true;
    }
}
