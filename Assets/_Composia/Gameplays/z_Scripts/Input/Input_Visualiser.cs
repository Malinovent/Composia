using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Input_Visualiser : MonoBehaviour
{
    //Visualize input in-engine to check what is working and how
    private InputManager_Archive _inputManager { get { return GameObject.FindObjectOfType<InputManager_Archive>(); } }

    public List<ImageControllerDataTest> imageControllerDataTests = new List<ImageControllerDataTest>();
    public List<ImageControllerDataTest> imageKeyboardDataTests = new List<ImageControllerDataTest>();

    public GameObject _basicController;
    public GameObject _basicKeyboard;

    private void Update()
    {
        if (_inputManager.playerControllerType == PlayerControllerType.keyboard)
        {
            //Bool
            UpdateSpriteWithInput("_southButton", _inputManager._southButton, imageKeyboardDataTests);
            UpdateSpriteWithInput("_eastButton", _inputManager._eastButton, imageKeyboardDataTests);
            UpdateSpriteWithInput("_westButton", _inputManager._westButton, imageKeyboardDataTests);
            UpdateSpriteWithInput("_northButton", _inputManager._northButton, imageKeyboardDataTests);
            UpdateSpriteWithInput("_leftStickPress", _inputManager._leftStickPress, imageKeyboardDataTests);
            UpdateSpriteWithInput("_rightStickPress", _inputManager._rightStickPress, imageKeyboardDataTests);
            UpdateSpriteWithInput("_optionButton", _inputManager._optionButton, imageKeyboardDataTests);
            UpdateSpriteWithInput("_selectButton", _inputManager._selectButton, imageKeyboardDataTests);
            UpdateSpriteWithInput("_rightBumper", _inputManager._rightBumper, imageKeyboardDataTests);
            UpdateSpriteWithInput("_rightTrigger", _inputManager._rightTrigger, imageKeyboardDataTests);
            UpdateSpriteWithInput("_leftBumper", _inputManager._leftBumper, imageKeyboardDataTests);
            UpdateSpriteWithInput("_leftTrigger", _inputManager._leftTrigger, imageKeyboardDataTests);
            UpdateSpriteWithInput("westButton", _inputManager._westButton, imageKeyboardDataTests);

            //Float
            UpdateSpriteWithInputFloat("_leftJoysticX", _inputManager._leftJoystickX, imageKeyboardDataTests);
            UpdateSpriteWithInputFloat("_leftJoysticY", _inputManager._leftJoystickY, imageKeyboardDataTests);
            UpdateSpriteWithInputFloat("_rightJoysticX", _inputManager._rightJoystickX, imageKeyboardDataTests);
            UpdateSpriteWithInputFloat("_rightJoysticY", _inputManager._rightJoystickY, imageKeyboardDataTests);
            UpdateSpriteWithInputFloat("_dpadX", _inputManager._dpadX, imageKeyboardDataTests);
            UpdateSpriteWithInputFloat("_dpadY", _inputManager._dpadY, imageKeyboardDataTests);

            //Hide other type of input
            HideSprite(imageControllerDataTests);
            _basicController.SetActive(false);
            _basicKeyboard.SetActive(true);
        }

        if (_inputManager.playerControllerType == PlayerControllerType.gamepad)
        {

            //Bool
            UpdateSpriteWithInput("_southButton", _inputManager._southButton, imageControllerDataTests);
            UpdateSpriteWithInput("_eastButton", _inputManager._eastButton, imageControllerDataTests);
            UpdateSpriteWithInput("_westButton", _inputManager._westButton, imageControllerDataTests);
            UpdateSpriteWithInput("_northButton", _inputManager._northButton, imageControllerDataTests);
            UpdateSpriteWithInput("_leftStickPress", _inputManager._leftStickPress, imageControllerDataTests);
            UpdateSpriteWithInput("_rightStickPress", _inputManager._rightStickPress, imageControllerDataTests);
            UpdateSpriteWithInput("_optionButton", _inputManager._optionButton, imageControllerDataTests);
            UpdateSpriteWithInput("_selectButton", _inputManager._selectButton, imageControllerDataTests);
            UpdateSpriteWithInput("_rightBumper", _inputManager._rightBumper, imageControllerDataTests);
            UpdateSpriteWithInput("_rightTrigger", _inputManager._rightTrigger, imageControllerDataTests);
            UpdateSpriteWithInput("_leftBumper", _inputManager._leftBumper, imageControllerDataTests);
            UpdateSpriteWithInput("_leftTrigger", _inputManager._leftTrigger, imageControllerDataTests);

            //Float
            UpdateSpriteWithInputFloat("_leftJoysticX", _inputManager._leftJoystickX, imageControllerDataTests);
            UpdateSpriteWithInputFloat("_leftJoysticY", _inputManager._leftJoystickY, imageControllerDataTests);
            UpdateSpriteWithInputFloat("_rightJoysticX", _inputManager._rightJoystickX, imageControllerDataTests);
            UpdateSpriteWithInputFloat("_rightJoysticY", _inputManager._rightJoystickY, imageControllerDataTests);
            UpdateSpriteWithInputFloat("_dpadX", _inputManager._dpadX, imageControllerDataTests);
            UpdateSpriteWithInputFloat("_dpadY", _inputManager._dpadY, imageControllerDataTests);

            //Hide other type of input
            HideSprite(imageKeyboardDataTests);
            _basicController.SetActive(true);
            _basicKeyboard.SetActive(false);
        }
    }

    private void UpdateSpriteWithInput(string _name, bool _active, List<ImageControllerDataTest> _imageControllerData)
    {
        foreach(ImageControllerDataTest data in _imageControllerData)
        {
            if (_name == data.name)
            {
                if (_active)
                    data._image.sprite = data._spriteFeedbackOn;
                if (!_active)
                    data._image.sprite = data._spriteFeedbackOff;
            }
        }
    }

    private void UpdateSpriteWithInputFloat(string _name, float _active, List<ImageControllerDataTest> _imageControllerData)
    {
        foreach (ImageControllerDataTest data in _imageControllerData)
        {
            if (_name == data.name)
            {
                if (_active == 0f)
                    data._image.sprite = data._spriteFeedbackOff;
                if (_active < 0f)
                    data._image.sprite = data._spriteFeedbackOn;
                if (_active > -0f)
                    data._image.sprite = data._spriteFeedbaclOnSecond;
            }
        }
    }

    private void HideSprite(List<ImageControllerDataTest> _imageControllerData)
    {
        foreach(ImageControllerDataTest data in _imageControllerData)
        {
            data._image.sprite = data._spriteFeedbackOff;
        }
    }

}

[System.Serializable]
public class ImageControllerDataTest
{
    public string name;
    public Image _image;
    public Sprite _spriteFeedbackOff;
    public Sprite _spriteFeedbackOn;
    public Sprite _spriteFeedbaclOnSecond;
}
