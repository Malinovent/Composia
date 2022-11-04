using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Composia
{
    public class CompositionInputSystem : Singleton<CompositionInputSystem>, IObservable
    {
        [SerializeField]
        public ComposiaControls controls;

        private PlayerController_PlatformerRigidbody _playerRigidbody;        //Reference to the class performing the actions
        private PlayerController_Creation _playerCreation;

        private int measureIndex = 0;

        private bool _isKeyboard = true;

        private List<IObserver> _observers = new List<IObserver>();

        public bool IsKeyboard { get => _isKeyboard; set => _isKeyboard = value; }

        #region Overrides
        void Awake()
        {
            if (PlayerController_PlatformerRigidbody.Instance) { _playerRigidbody = PlayerController_PlatformerRigidbody.Instance; }
            if (PlayerController_Creation.Instance) { _playerCreation = PlayerController_Creation.Instance; }

            controls = new ComposiaControls();
        }

        private void Start()
        {
            InitializePlatformerControls();
            InitializeCreationControls();
        }
        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        void Update()
        {
            DetermineInputType();
        }

        #endregion

        private void DetermineInputType()
        {
            if (!IsKeyboard && (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
            {
                IsKeyboard = true;
                NotifyObservers();
                CreationUI.Instance.ActivateAllMeasureInputs();
                Cursor.visible = true;
                _playerCreation.cursor.SetActive(false);
                Debug.Log("Switching to keyboard and mouse");
            }

            if (Gamepad.current != null)
            {
                if (IsKeyboard && Gamepad.current.IsActuated())
                {
                    Cursor.visible = false;
                    IsKeyboard = false;
                    _playerCreation.cursor.SetActive(true);
                    _playerCreation.SetCursorToCenter();
                    //CreationUI.Instance.UpdateInput(IsKeyboard);
                    NotifyObservers();
                    CreationUI.Instance.DeactivateAllButCurrent(measureIndex);
                    Debug.Log("Switching to gamepad");
                }
            }

        }
        private void InitializePlatformerControls()
        {
            controls.Platforming.Jump_Press.performed += _ => JumpPress();
            controls.Platforming.Jump_Hold.performed += _ => JumpHold();
            controls.Platforming.Jump_Release.performed += _ => JumpRelease();
            controls.Platforming.Start.performed += _ => PlayPerformed();
            controls.Platforming.Pows.performed += _ => PowPerformed();
            controls.Platforming.SwitchModes.performed += _ => SwitchToCreation();
            controls.Platforming.Restart.performed += _ => RestartPerformed();
            controls.Platforming.Charge_Press.performed += _ => ChargePress();
            controls.Platforming.Charge_Hold.performed += _ => ChargeHold();

            controls.Platforming.Disable();
        }

        private void InitializeCreationControls()
        {
            controls.Creating.SwitchModes.performed += _ => SwitchToPlatforming();
            controls.Creating.PlacePiece.performed += _ => PlacePiecePerformed();
            controls.Creating.ChoosePiece.performed += _ => ChoosePiecePerformed();
            controls.Creating.ErasePiece.performed += _ => ErasePiecePerformed();
            controls.Creating.NextRole.performed += _ => NextRolePerformed();
            controls.Creating.PreviousRole.performed += _ => PreviousRolePerformed();
            controls.Creating.RotateMeasureClockwise.performed += _ => RotateMeasureClockwisePerformed();
            controls.Creating.RotateMeasureCounterClockwise.performed += _ => RotateMeasureCounterClockwisePerformed();
            controls.Creating.PreviousMeasure.performed += _ => PreviousMeasurePerformed();
            controls.Creating.NextMeasure.performed += _ => NextMeasurePerformed();
            controls.Creating.MoveCursor.performed += ctx => MoveCursor(ctx.ReadValue<Vector2>());
            controls.Creating.MoveCursor.canceled += ctx => MoveCursor(ctx.ReadValue<Vector2>());
            controls.Creating.SwitchScales.performed += _ => SwitchScalesPerformed();
            controls.Creating.ReturnToWorld.performed += _ => ReturnToWorldPerformed();
            //controls.Creating.ExecuteAction.performed += _ => ExecuteAction();

            controls.Creating.Enable();
        }

        #region INPUTS

        #region PLATFORMING
        private void JumpPress()
        {
            _playerRigidbody.JumpPress();
        }
        private void JumpHold()
        {
            _playerRigidbody.JumpHold();
        }
        private void JumpRelease()
        {
            _playerRigidbody.JumpRelease();
        }
        private void PowPerformed()
        {
            Debug.Log("Performed Pow");
            _playerRigidbody.PowInput();
        }
        private void PlayPerformed()
        {
            Debug.Log("Playing");
            _playerRigidbody.PlayInput();
        }
        private void ChargePress()
        {
            Debug.Log("Charge Press");
            _playerRigidbody.ChargePress();
        }
        private void ChargeHold()
        {
            Debug.Log("Charge Hol");
            _playerRigidbody.ChargeHold();
        }
        private void RestartPerformed()
        {
            Debug.Log("Restarting");
        }
        public void SwitchToCreation()
        {
            Debug.Log("Switching to Creation");
            SwitchToCreationInputs();
            Level.Instance.SwitchModes();
        }

        public void SwitchToCreationInputs()
        {
            controls.Platforming.Disable();
            controls.Creating.Enable();
        }

        #endregion

        #region CREATION

        public void ExecuteAction()
        {
            _playerCreation.ExecuteAction();
        }

        public void PlacePiecePerformed()
        {
            _playerCreation.PlacePiece();
            //Debug.Log("Placing Piece");
        }

        public void ChoosePiecePerformed()
        {

            Debug.Log("Chose Piece");
        }

        public void ErasePiecePerformed()
        {
            Debug.Log("Erasing Piece");
            _playerCreation.RemovePiece();
        }

        public void NextRolePerformed()
        {
            RolesController.Instance.NextRole();
            Debug.Log("Next Role");
        }

        public void PreviousRolePerformed()
        {
            RolesController.Instance.PreviousRole();
            Debug.Log("Previous Role");
        }

        public void RotateMeasureClockwisePerformed()
        {
            RolesController.Instance.RotateMeasureClockwise(measureIndex);
            Debug.Log("RotateCockwise");
        }

        public void RotateMeasureCounterClockwisePerformed()
        {
            RolesController.Instance.RotateMeasureCounterClockwise(measureIndex);
            Debug.Log("Rotate Counterclockwise");
        }

        public void PreviousMeasurePerformed()
        {
            CreationUI.Instance.HideMeasureFeedback(measureIndex);
            measureIndex--;
            if (measureIndex < 0)
            {
                measureIndex = 3;
            }
            CreationUI.Instance.DisplayMeasureFeedback(measureIndex);
            //UpdateCursorPosition();
        }

        public void NextMeasurePerformed()
        {
            CreationUI.Instance.HideMeasureFeedback(measureIndex);
            measureIndex++;
            if (measureIndex > 3)
            {
                measureIndex = 0;
            }

            CreationUI.Instance.DisplayMeasureFeedback(measureIndex);

            //UpdateCursorPosition();
            Debug.Log("Next measure");
        }

        public void UpdateCursorPosition()
        {
            _playerCreation.SetCursorPositionToMeasure(measureIndex);
        }


        public void SwitchToPlatforming()
        {/*
            Debug.Log("Switching to Platforming");
            controls.Platforming.Enable();
            controls.Creating.Disable();
            Level.Instance.SwitchModes();*/
        }

        public void SwitchToPlatformingInputs()
        {
            controls.Platforming.Enable();
            controls.Creating.Disable();
        }

        public void MoveCursor(Vector2 direction)
        {
           // Debug.Log("Player wants to move cursor " + direction);
            _playerCreation.GetMoveVector(direction);
        }

        public void SwitchScalesPerformed()
        {
            CreationUI.Instance.SwitchScale();
        }

        public void ReturnToWorldPerformed()
        {
            _playerCreation.ReturnToWorld();
        }

        #endregion

        #region PERFORMANCE


        #endregion

        #region ARRANGEMENT

        #endregion

        #endregion

        public void AddObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach(IObserver o in _observers)
            {
                o.UpdateData(this);
            }
        }

    }
}