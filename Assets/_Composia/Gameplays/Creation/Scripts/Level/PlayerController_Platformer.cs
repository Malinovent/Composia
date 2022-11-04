using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class PlayerController_Platformer : LadyBugController 
    { 

        public bool isDebugCheckingGrid;
        [SerializeField]
        private float _jumpBurst;
        [SerializeField]
        private float _jumpSustainSpeed = 1;
        [SerializeField]
        private float _fallSpeed = 5;
        [SerializeField]
        private float _jumpSustainDuration = 1;
        private float _jumpSustainTimer = 0;

        private bool _canJump = true;
        private bool _isJumping = false;
        private bool _isFalling = false;
        

        private LevelPiece _currentInteractablePiece;


        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();

            Initiliaze();
        }

        private void Update()
        {
            JumpInput();

            FallingPositionCorrection();
        }

        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Falling();
            Jumping();
        }


        public override void BeatUpdate()
        {
            base.BeatUpdate();

            if (IsOnObstacle())
            {
                _canJump = true;
            }

            if (IsInObstacle())
            {
                CorrectPosition();
            }

            
            if (DetectSideObstacle())
            {
                IsMoving = false;
                _canJump = false;
                _isJumping = false;
            }
        }


        private void Initiliaze()
        {/*
            _fallSpeed *= BpmSpeed;
            _jumpSustainDuration /= BpmSpeed;*/
        }

        private void DoJumpBurst()
        {
            if (_isJumping)
            {
                
            }
        }
                

        private void JumpInput()
        {
            DoJumpBurst();

            //ON PRESSED
            if (Keyboard.current.wKey.wasPressedThisFrame && _canJump)
            {              
                _canJump = false;
                _isJumping = true;
            }

            if (Keyboard.current.wKey.isPressed && _isJumping)
            {
                _jumpSustainTimer += Time.deltaTime;

                if (_jumpSustainTimer >= _jumpSustainDuration)
                {
                    _isJumping = false;
                    _jumpSustainTimer = 0;
                }
            }

            //ON RELEASE
            if (Keyboard.current.wKey.wasReleasedThisFrame)
            {
                _jumpSustainTimer = 0;
                if (_isJumping) { _isJumping = false; }
            }          
        }

        private void Jumping()
        {
            if (_isJumping)
            {
                this.transform.Translate(Vector3.up * _jumpSustainSpeed * Time.deltaTime);
            }
        }

        private void Interact()
        {

        }

        private void Falling()
        {
            if (!(_currentInteractablePiece = ReturnPieceBelow()) && !_isJumping)
            {
                _isFalling = true;

                this.transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);

                //Add Coyote 
                _canJump = false;
                CorrectPosition();
            }
        }

        private void CorrectPosition()
        {
            if (!_currentInteractablePiece) { return; }

            Vector3 newTransform = this.transform.position;

            newTransform.y = Level.Instance.GridToWorldCoordinates(_currentInteractablePiece.y + 1) - (Level.Instance.CellSize / 2);

            this.transform.position = newTransform;
            _canJump = true;
        }

        private void FallingPositionCorrection()
        {
            if (_isFalling)
            {
                if (IsOnObstacle())
                {
                    CorrectPosition();
                    _isFalling = false;
                }
            }
        }

        private bool IsOnObstacle()
        {
            if (gridPos.x >= 0 && gridPos.y >= 0.1f)
            {
                if (_currentInteractablePiece)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsInObstacle()
        {
            LevelPiece piece = Level.Instance.FindPiece(gridPos.x, gridPos.y, currentMeasure);
           
            if (piece != null) { Debug.Log("I'm inside an obstacle!"); return true;  }

            return false;
        }

        private LevelPiece DetectSideObstacle()
        {
            LevelPiece piece = Level.Instance.FindPiece(gridPos.x + 1, gridPos.y, currentMeasure);
            if(piece != null) 
            { 
                Debug.Log("Detected side! Found piece at x:" + (gridPos.x + 1) + " y-" + gridPos.y);
                if (IsOnObstacle())
                {
                    CorrectPosition();
                    piece = Level.Instance.FindPiece(gridPos.x + 1, gridPos.y, currentMeasure);
                }
            }
            return piece;
        }

        private LevelPiece ReturnPieceBelow()
        {
            int gridPosY = (int)Level.Instance.WorldToGridCoordinates(new Vector3(this.transform.position.x, this.transform.position.y - 0.1f, this.transform.position.z)).y;            

            return Level.Instance.FindPiece(gridPos.x, gridPosY, currentMeasure);
        }

    }
}
