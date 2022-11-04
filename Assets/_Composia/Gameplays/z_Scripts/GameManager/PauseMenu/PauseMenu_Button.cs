using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu_Button : MonoBehaviour
{
    //Logic for buttons of pause menu 
    private PauseMenu _pauseMenu { get { return GetComponentInParent<PauseMenu>(); } }
    private Audio_SimpleAction _audio_SimpleAction { get { return GameObject.FindObjectOfType<Audio_SimpleAction>(); } }

    //Resume game
    public void PauseMenu_ResumeButton()
    {
        _pauseMenu._ManagePauseMenu();
    }

    //Quit game
    public void PauseMenu_Quit()
    {
        Debug.Log("Quit game!");
        _audio_SimpleAction.PlayWwiseEvent(_audio_SimpleAction._events["clic_menu"]);
        Application.Quit();
    }
}
