using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputManager_Archive : MonoBehaviour
{

    public static InputManager_Archive singleton;  //Single variable of itself

    //Find the current controller
    //Manage all pertinent input for the game


    //Which type of controller
    public PlayerControllerType playerControllerType;

    //All input for the controller
    public bool _southButton;       //X button on dualshock4
    public bool _westButton;        //Sqaure button on dualshock4
    public bool _eastButton;        //Circle button on dualshock4
    public bool _northButton;       //North button on dualshock4

    public bool _leftStickPress;    //L3 button on dualshock4
    public bool _rightStickPress;    //R3 button on dualshock4

    public bool _optionButton;      //Options button on dualshock4 (start on xbox)
    public bool _selectButton;      //Select button on Xbox controller (need to check for playstation (Share?))

    public bool _rightBumper;       //R1 button on dualshock4
    public bool _rightTrigger;      //R2 button on dualshock4
    public bool _leftBumper;        //L1 button on dualshock4
    public bool _leftTrigger;       //L2 button on dualshock4

    public float _leftJoystickX;    //Check name
    public float _leftJoystickY;    //Check name
    public float _rightJoystickX;   //Check name
    public float _rightJoystickY;   //Check name

    public float _dpadX;            //Check name
    public float _dpadY;            //Check name

    private void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        updatePlayerControllerType(input.currentControlScheme);

        //Manage Singleton
        if (singleton == null) 
        { 
            singleton = this; 
        }
        else
        { 
            Destroy(this.gameObject); 
        }

    }

    void OnEnable()
    {
        InputUser.onChange += onInputDeviceChange;
    }

    void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    public void Update()
    {
        //Manage player input
        PlayerControllerScheme(playerControllerType);
    }

   

    private void PlayerControllerScheme(PlayerControllerType playerControllerType)
    {
        if (playerControllerType == PlayerControllerType.keyboard)
        {
            //Get the current controller
            var _keyboard = Keyboard.current;
            var _mouse = Mouse.current;

            //South
            _southButton = _keyboard.spaceKey.isPressed;

            //West
            //_westButton = _keyboard.buttonWest.isPressed;

            //East
            _eastButton = _mouse.leftButton.isPressed;

            //North
            //_northButton = _keyboard.buttonNorth.isPressed;

            //Left stick press
            //_leftStickPress = _keyboard.leftStickButton.isPressed;

            //Right stick press
            //_rightStickPress = _keyboard.rightStickButton.isPressed;

            //Options buttons
            _optionButton = _keyboard.escapeKey.isPressed;

            //Select button
            //_selectButton = _keyboard.selectButton.isPressed;

            //Right bumper
            //_rightBumper = _keyboard.rightShoulder.isPressed;

            //Right trigger
            //_rightTrigger = _keyboard.rightTrigger.isPressed;

            //Left bumper
            //_leftBumper = _keyboard.leftShoulder.isPressed;

            //Left trigger
            //_leftTrigger = _keyboard.leftTrigger.isPressed;

            //Right joystick X
            //_rightJoystickX = _keyboard.rightStick.x.ReadValue();
            //Check mouse for this one

            //Right joystick Y
            //_rightJoystickY = _keyboard.rightStick.y.ReadValue();
            //Check mouse for this one

            //Left joystick X
            if (_keyboard.aKey.isPressed)
            {
                _leftJoystickX = -1f;
            }
            else if (_keyboard.dKey.isPressed)
            {
                _leftJoystickX = 1f;
            }
            else
            {
                _leftJoystickX = 0f;
            }


            //Left joystick Y
            if (_keyboard.sKey.isPressed)
            {
                _leftJoystickY = -1f;
            }
            else if (_keyboard.wKey.isPressed)
            {
                _leftJoystickY = 1f;
            }
            else
            {
                _leftJoystickY = 0f;
            }


            //Dpad X
            if (_keyboard.leftArrowKey.isPressed)
            {
                _dpadX = -1f;
            }
            else if (_keyboard.rightArrowKey.isPressed)
            {
                _dpadX = 1f;
            }
            else
            {
                _dpadX = 0f;
            }

            //Dpad Y
            if (_keyboard.downArrowKey.isPressed)
            {
                _dpadY = -1f;
            }
            else if (_keyboard.upArrowKey.isPressed)
            {
                _dpadY = 1f;
            }
            else
            {
                _dpadY = 0f;
            }
        }
        if (playerControllerType == PlayerControllerType.gamepad)
        {
            //Get the current controller
            var _gamepad = Gamepad.current;

            //South
            _southButton = _gamepad.buttonSouth.isPressed;

            //West
            _westButton = _gamepad.buttonWest.isPressed;

            //East
            _eastButton = _gamepad.buttonEast.isPressed;

            //North
            _northButton = _gamepad.buttonNorth.isPressed;

            //Left stick press
            _leftStickPress = _gamepad.leftStickButton.isPressed;

            //Right stick press
            _rightStickPress = _gamepad.rightStickButton.isPressed;

            //Options buttons
            _optionButton = _gamepad.startButton.isPressed;

            //Select button
            _selectButton = _gamepad.selectButton.isPressed;

            //Right bumper
            _rightBumper = _gamepad.rightShoulder.isPressed;

            //Right trigger
            _rightTrigger = _gamepad.rightTrigger.isPressed;

            //Left bumper
            _leftBumper = _gamepad.leftShoulder.isPressed;

            //Left trigger
            _leftTrigger = _gamepad.leftTrigger.isPressed;

            //Right joystick X
            _rightJoystickX = _gamepad.rightStick.x.ReadValue();

            //Right joystick Y
            _rightJoystickY = _gamepad.rightStick.y.ReadValue();

            //Left joystick X
            _leftJoystickX = _gamepad.leftStick.x.ReadValue();

            //Left joystick Y
            _leftJoystickY = _gamepad.leftStick.y.ReadValue();

            //Dpad X
            _dpadX = _gamepad.dpad.x.ReadValue();

            //Dpad Y
            _dpadY = _gamepad.dpad.y.ReadValue();
        }
    }

    //If controller type new, check which type
    //Need a input action asset binded for controller and keyboard 
    //in the Player input compoenent for this to work
    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            updatePlayerControllerType(user.controlScheme.Value.name);
        }
    }

    //Get the correct controller type
    void updatePlayerControllerType(string schemeName)
    {
        //2 controller type: keyboard and gamepad
        if (schemeName.Equals("Gamepad"))
        {
            Debug.Log("Using Gamepad");
            playerControllerType = PlayerControllerType.gamepad;
        }
        else
        {
            Debug.Log("Using Keyboard & Mouse");
            playerControllerType = PlayerControllerType.keyboard;
        }

        //Make sure no input is broken
        ResetAllInput();
    }

    //Reset all input
    public void ResetAllInput()
    {
        //All input for the controller
        _southButton = false;//X button on dualshock4
        _westButton = false;//Sqaure button on dualshock4
        _eastButton = false;//Circle button on dualshock4
        _northButton = false;//North button on dualshock4

        _leftStickPress = false;//L3 button on dualshock4
        _rightStickPress = false;//R3 button on dualshock4

        _optionButton = false;//Options button on dualshock4 (start on xbox)
        _selectButton = false;//Select button on Xbox controller (need to check for playstation (Share?))

        _rightBumper = false;//R1 button on dualshock4
        _rightTrigger = false;//R2 button on dualshock4
        _leftBumper = false;//L1 button on dualshock4
        _leftTrigger = false;//L2 button on dualshock4

        _leftJoystickX = 0f;//Check name
        _leftJoystickY = 0f;//Check name
        _rightJoystickX = 0f;//Check name
        _rightJoystickY = 0f;//Check name

        _dpadX = 0f;//Check name
        _dpadY = 0f;//Check name
    }

}

[System.Serializable]
public enum PlayerControllerType
{
    keyboard,
    gamepad
}
