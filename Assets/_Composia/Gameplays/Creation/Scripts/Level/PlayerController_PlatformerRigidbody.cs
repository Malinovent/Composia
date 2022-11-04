using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia
{
    public class PlayerController_PlatformerRigidbody : Singleton<PlayerController_PlatformerRigidbody>, IObserver
    {
        [System.Serializable]
        public struct GridPosition
        {
            public int x;
            public int y;
        }

        public GameObject treasure;
        public GameObject endPlatform;

        [SerializeField]
        public GridPosition gridPos;
        public float speed = 1;

        public int currentMeasure = 0;

        public float timeMeasureMultiplier;
        private TimeManager timeManager;
        private AnimationController _animator;

        private Rigidbody _rb;

        private bool _isMoving = true;
        private bool _isHalfNoteMoving = false;
        private bool _canJump = true;
        private bool _isJumping = false;
        private bool _isSliding = false;
        private bool _isResting = false;
        private bool _isPlaying = false;
        private bool _isEnding = false;
        private bool _isStarting = false;
        private bool _isPow = false;
        private bool _canPow = false;
        private bool _isCharging = false;
        private bool _isCharged = false;
        private bool _canInput = true;
        private bool _hasEnded = false;

        private bool _isTouchingBelow = true;

        private float _currentChargeValue = 0;
        private float _targetChargeValue;

        private NotePiece _currentInteractablePiece;
        private NotePiece _nextInteractablePiece;
        private HalfNotePiece _currentHalfNote;
        private NotePiece _lastPiece;

        //Serialized Fields
        [SerializeField]
        private float _jumpSustainDuration;
        private float _jumpSustainTimer;
        [SerializeField]
        private float _fallSpeed;
        private float _slideDuration = 1.25f;
        [SerializeField]
        private float _powSpeed = 1000;
        public float slideBoostSpeedVelocity = 1;


        //TEMPORARY
        private Material myMat;
        


        public bool IsSliding
        {
            get => _isSliding;
            set
            {
                _isSliding = value;

                if (_isSliding) { _rb.useGravity = false; }
                else { _rb.useGravity = true; }
            }
        }

        public bool CanJump
        {
            get => _canJump;
            set
            {
                _canJump = value;
                ChangeCanJumpFeedback(_canJump);
            }
        }

        public bool IsJumping 
        {
            get => _isJumping;
            set
            {
                _isJumping = value;
                if (_isJumping)
                {
                    _animator.animator.SetTrigger("jumpTrigger");
                }
                else 
                {
                    _animator.animator.SetTrigger("fallTrigger");              
                }       
            }   
        }

        private Vector3 _initialPos;
        private Vector3 _startTilePos;
        private float t;
        private float _timeToReachDestination;
        private Vector3 _targetPos;

        #region OVERRIDES

        void Awake()
        {
            _animator = GetComponent<AnimationController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
            Initialize();
            _animator.animator.SetTrigger("idleTrigger");
        }

        // Update is called once per frame
        void Update()
        {
            if (_canInput)
            {
                if (!_isEnding && !_isStarting)
                {
                    if (Level.Instance.mode == Level.Mode.Platforming)
                    {
                        JumpInput();
                        //PlayInput();
                        //PowInput();
                        ChargeInput();
                    }
                }
            }

            CheckStart();
            _animator.animator.SetBool("isTouchingBelow", _isTouchingBelow);
        }

        private void FixedUpdate()
        {
            if (_isEnding)
            {
                if (!_hasEnded) { LerpTowardPosition(); }
                else  {  MoveTranslate(); }

                return;
            } 
            else if (_isPlaying)
            {
                SlidingNote();
                HalfNoteMoving();
                MoveTranslate();
                Resting();
                Jetpack();
                Charging();
            }
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == treasure)
            {
                Pause();
                _animator.animator.SetTrigger("idleTrigger");
                _hasEnded = true;
                LadyBugController.Instance.Pause();
                Level.Instance.Save();
                PlatformerUI.Instance.BeginEndUI();
                return;
            }
           
            if (collision.gameObject.GetComponentInParent<NotePiece>())
            {
                _currentInteractablePiece = collision.gameObject.GetComponentInParent<NotePiece>();
                //Debug.Log("Collided with " + _currentInteractablePiece.gameObject.name);
                _currentInteractablePiece.Activate(_isPow);
                _isPow = false;

                FindNextPiece();

                //Does something if it is a notation piece
                if (_currentInteractablePiece is HalfNotePiece)
                {
                    _currentHalfNote = (HalfNotePiece)_currentInteractablePiece;
                    _isMoving = false;
                    _isHalfNoteMoving = true;
                    _rb.useGravity = false;
                    CanJump = true;
                }
                else if (_currentInteractablePiece is WholeNotePiece)
                {
                    _isMoving = false;
                    _rb.useGravity = false;
                    CanJump = false;
                    _isCharging = true;
                    ChargingFeedback(false);
                    WholeNotePiece piece = _currentInteractablePiece as WholeNotePiece;
                    _targetChargeValue = piece.CalculateChargeTime();
                    this.transform.position = _currentInteractablePiece.transform.position + Vector3.up;
                }
                else if (collision.contacts[0].normal.y > 0.6f)
                {
                    _animator.animator.SetTrigger("landTrigger");
                    CanJump = true;
                    _isMoving = true;
                    _isTouchingBelow = true;
                }

                //Detect side
                if (collision.contacts[0].normal.x > 0.8f)
                {
                    _isMoving = false;
                }
            }
            else if (collision.contacts[0].normal.y > 0.6f)
            {
                _animator.animator.SetTrigger("landTrigger");
                _isTouchingBelow = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            _isTouchingBelow = false;
            if (!IsJumping) { _animator.animator.SetTrigger("fallTrigger"); }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasEnded) { return; }

            if (other.gameObject == endPlatform)
            {
                Pause();
                _hasEnded = true;
                _rb.useGravity = true;
                _isMoving = true;
                Debug.Log("Triggered with end point");
                return;
            }
            else if (!_isSliding)
            {
                _currentInteractablePiece = other.GetComponentInParent<NotePiece>();
                FindNextPiece();

                if (_currentInteractablePiece is SlidingNotePiece)
                {
                    _currentInteractablePiece.Activate();
                    SetSlideDuration(_currentInteractablePiece.size);
                    SetSlidingNoteDestination();                     
                }
                else if (_currentInteractablePiece is RestNotePiece)
                {
                    PlatformerStats.Instance.AddHitNote();
                    SetRestDestinations();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<NotationPiece>())
            {
                _isTouchingBelow = false;
                if (!IsJumping) { _animator.animator.SetTrigger("fallTrigger"); }
            }
        }

        #endregion

        private void Initialize()
        {
            _initialPos = this.transform.position;
            timeManager = TimeManager.Instance;
            timeManager.AddObserver(this);
            Level.Instance.AddObserver(this);
            if (Level.Instance.NotePieces.Count > 0)
            {
                _lastPiece = Level.Instance._notationPieces[Level.Instance._notationPieces.Count - 1];
            }
            _rb = GetComponent<Rigidbody>();

            speed = (speed * timeManager.BPS) * 4;                  //1 beat = 4 squares
            _jumpSustainDuration *= timeManager.BPS;
            _slideDuration *= timeManager.BPS * 0.25f;
            _powSpeed *= timeManager.BPS;
            slideBoostSpeedVelocity *= timeManager.BPS;

            Physics.gravity *= timeManager.BPS * _fallSpeed;

            myMat = GetComponentInChildren<MeshRenderer>().material;
            ChangeCanJumpFeedback(CanJump);
        }

        private void SetSlideDuration(int pieceSize)
        {
            _slideDuration = (1.05f / timeManager.BPS) * (pieceSize / 4f);
        }

        private void ChangeCanJumpFeedback(bool on)
        {
            //if (on) { myMat.SetColor("_Color", Color.green); }
            //else { myMat.SetColor("_Color", Color.red); }
        }

        private void PowFeedback()
        {
            //myMat.SetColor("_Color", Color.yellow);
        }

        private void ChargingFeedback(bool on)
        {
           /* if (on)
            {
                myMat.SetColor("_Color", Color.blue);
            }
            else 
            {
                myMat.SetColor("_Color", Color.cyan);
            }*/
        }

        public void Pause()
        {
            _isPlaying = false;
            _canInput = false;
            _isMoving = false;
        }

        public void Play()
        {
            //Debug.Log("Playing...");
            //_animator.animator.SetBool("isWalking", _isMoving);
            _animator.animator.SetTrigger("walkTrigger");
            _isPlaying = true;
            _isStarting = true;
            CanJump = true;
            _nextInteractablePiece = Level.Instance.NotePieces[0];
        }

        public void Stop()
        {
            _animator.animator.SetTrigger("idleTrigger");
            _isStarting = false;
            _isPlaying = false;
            CanJump = true;
            ResetDestination();
            LadyBugController.Instance.Stop();
            Level.Instance.RestartNotationPieces();
            TimeManager.Instance.Restart();
            //FindFirstPiece();
        }

        public bool CheckDie()
        {
            if (this.gridPos.y < 0)
            {
                Stop();
                return true;
            }

            return false;
        }

        public void CheckEnd()
        {
            if (_currentInteractablePiece != _lastPiece || _isEnding) { return; }
            UpdateGridPosition();
            if (this.gridPos.x >= Level.Instance.TotalColumns - _lastPiece.size && _isEnding == false)
            {
                _isEnding = true;
                //_isPlaying = false;
                SetFinalDestination();
            }
        }

        public void CheckStart()
        {
            if (_isStarting)
            {
                UpdateGridPosition();
                if (gridPos.x == -1)
                {
                    _isStarting = false;
                    TimeManager.Instance.StartTimer();
                }
            }
        }
        
        private void SetSlidingNoteDestination()
        {
            Vector3 destination = new Vector3(_nextInteractablePiece.transform.position.x + _nextInteractablePiece.leftPadding, _nextInteractablePiece.transform.position.y + 1f, 0);
            float distToNextPiece = _nextInteractablePiece.x - (_currentInteractablePiece.x + (_currentInteractablePiece.size - 1));
            bool isSliding = true;
            if (distToNextPiece > 1) 
            {                
                destination = new Vector3(this.transform.position.x + 1, _nextInteractablePiece.transform.position.y + 1, 0);
                isSliding = false;
                Debug.Log("Destination is too far! " + distToNextPiece + " destination is " + destination);
            }           

            SetDestination(destination, _slideDuration);
            Debug.Log("Setting slide destination");
            IsSliding = isSliding;
            _isMoving = false;
            CanJump = false;
            IsJumping = false;
        }

        private void SlidingNote()
        {
            if (IsSliding)
            {
                LerpTowardPosition();
                if (Vector3.Distance(this.transform.position, _targetPos) < 0.005f)
                {
                    //_currentInteractablePiece.PlaySound();                   
                    _currentInteractablePiece = _nextInteractablePiece;
                    _currentInteractablePiece.Activate();
                    FindNextPiece();
                    SetSlidingNoteDestination();
                    Debug.Log("Setting Sliding destination because of dist");
                }

                //Ends the sliding if the next piece is not a sliding note
                if (!(_nextInteractablePiece is SlidingNotePiece))
                {
                    //Debug.Log("I'm touching not a sliding note");
                    _isSliding = false;
                    _isMoving = true;
                    CanJump = true;
                    _rb.useGravity = true;
                    EndOfSlideBoost();
                }
            }
        }

        private void EndOfSlideBoost()
        {
            //SetJetpackDestination();

            float yDist = FindYDistanceToNextNote();
            yDist = Mathf.Max(1, yDist);
            float boost = slideBoostSpeedVelocity * yDist;
            Debug.Log("Boost is " + boost);
            //Mathf.Max(1, )
            
            _rb.velocity = new Vector3(_rb.velocity.x, boost);
        }

        private float FindYDistanceToNextNote()
        {
            if (!_nextInteractablePiece) { return 1; }
            
            float yDist = _nextInteractablePiece.y - this.transform.position.y;

            return yDist;
        }

        public void SetFinalDestination()
        {
            //Debug.Log("Setting final destination");
            _isResting = false;
            _rb.useGravity = false;
            Pause();
            float time = (_currentInteractablePiece.size * 0.5f) * (0.5f / timeManager.BPS);     //
            Vector3 newDest = endPlatform.transform.position;

            SetDestination(newDest, time);
            Debug.Log("Setting final destination");
            _animator.animator.SetTrigger("fallTrigger");
        }

        public int heightAboveBlock = 2;
        public void SetRestDestinations()
        {
            FindNextPiece();
            if (_isEnding) { return; }      //Move towards the end rather than toward the next 'Piece'
            _isResting = true;
            _rb.useGravity = false;
            float time = (_currentInteractablePiece.size * 0.5f) * (0.5f / timeManager.BPS);     //
            Vector3 newDest;

            Vector2Int dest = new Vector2Int(_nextInteractablePiece.x, _nextInteractablePiece.y + heightAboveBlock);
            newDest = Level.Instance.GridToWorldCoordinates(dest.x, dest.y);
            newDest.x += _nextInteractablePiece.leftPadding;

            SetDestination(newDest, time);
            Debug.Log("Setting Rest Destination");
        }

        private void Resting()
        {
            if (_isResting)
            {
               
                LerpTowardPosition();
                

                if (Vector3.Distance(this.transform.position, _targetPos) < 0.1f)
                {
                    _isResting = false;
                    _isMoving = true;
                    _rb.useGravity = true;
                }
            }
        }

        private void Charging()
        { 
            if(_isCharged)
            {
               LerpTowardPosition();              
                if (Vector3.Distance(this.transform.position, _targetPos) < 0.1f)
                {           
                    _isMoving = true;
                    _rb.useGravity = true;
                    _isCharged = false;
                }
            }
        }

        public void SetDestination(Vector3 destination, float time)
        {
            t = 0;
            _startTilePos = transform.position;
            _timeToReachDestination = time;
            _targetPos = destination;
        }

        private void LerpTowardPosition()
        {
            t += Time.deltaTime / _timeToReachDestination;
            transform.position = Vector3.Lerp(_startTilePos, _targetPos, t);
        }

        private void LerpTowardPositionY()
        {
            t += Time.deltaTime / _timeToReachDestination;
            if (_targetPos.y <= _startTilePos.y) { _targetPos.y = _startTilePos.y + 1; }
            float yPos = Mathf.Lerp(_startTilePos.y, _targetPos.y, t);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }


        private void HalfNoteMoving()
        {
            if (_isHalfNoteMoving)
            {
                this.transform.position = _currentHalfNote.movingBlock.transform.position + Vector3.up;
            }
        }

        public void StopHalfNoteMoving()
        {
            Debug.Log("Stop Half note moving");
            _isMoving = true;
            _isHalfNoteMoving = false;
            _rb.useGravity = true;
        }

        #region TIME & GRID

        public void ResetDestination()
        {
            this.transform.position = _initialPos;
            _isEnding = false;
            _isPlaying = false;
            _canJump = false;
            _canPow = false;
            FindFirstPiece();
        }

        public float SpeedToBPM()
        {
            float num = timeMeasureMultiplier * 4;
            return num;
        }

        public Vector2Int UpdateGridPosition()
        {
            Vector2 pos = Level.Instance.WorldToGridCoordinates(this.transform.position);
            gridPos.x = (int)pos.x;
            gridPos.y = (int)pos.y;

            if (CheckDie()) { return new Vector2Int(gridPos.x, gridPos.y); }

            CheckMeasure();
            return new Vector2Int(gridPos.x, gridPos.y);
        }

        public void CheckMeasure()
        {
            //TASK: Add a measure length variable to Level.
            if (gridPos.x % Level.Instance.MeasureLength == 0)
            {
                currentMeasure = Level.Instance.FindMeasureIndex(gridPos.x);
                //Debug.Log("Current measure is: " + currentMeasure);
            }
        }


        //Much simpler version
        private void FindNextPiece()
        {
            CheckEnd();
            if (!_isEnding)
            {
                int nextIndex = Level.Instance.NotePieces.IndexOf(_currentInteractablePiece) + 1;
                _nextInteractablePiece = Level.Instance.NotePieces[nextIndex];
            }
        }

        private void FindFirstPiece()
        {
            _nextInteractablePiece = Level.Instance.NotePieces[0];
        }


        #endregion

        #region PHYSICS AND MOVEMENT
        private void ChargeInput()
        {/*
            ChargePress();
            ChargeHold();*/
        }

        public void ChargeHold()
        {
            if (_isCharging)
            {
                ChargingFeedback(false);
            }
        }

        public void ChargePress()
        {
            if (_isCharging)
            {
                _currentChargeValue += Time.deltaTime;
                ChargingFeedback(true);
                if (_currentChargeValue >= _targetChargeValue)
                {
                    _isCharging = false;
                    _isCharged = true;
                    _currentChargeValue = 0;
                    SetWholeNoteDestination();
                }
            }
        }

        //The player moves down at an increased speed
        private void POW()
        {
            _isPow = true;
            _rb.AddForce(Vector3.down * _powSpeed);
            PowFeedback();
        }

        private void SetJetpackDestination()
        {
            //FindNextPiece();
            if(_nextInteractablePiece == null) { return; }
            Vector3 jetpackPos = Level.Instance.GridToWorldCoordinates(_nextInteractablePiece.x, _nextInteractablePiece.y + heightAboveBlock);
            jetpackPos.x += _nextInteractablePiece.leftPadding;
            SetDestination(jetpackPos, _jumpSustainDuration);
            Debug.Log("Setting jetpack destination");
            _rb.useGravity = false;
        }

        private void SetWholeNoteDestination()
        {
            Vector3 wholeNotePos = Level.Instance.GridToWorldCoordinates(_nextInteractablePiece.x, _nextInteractablePiece.y + heightAboveBlock);
            wholeNotePos.x += _nextInteractablePiece.leftPadding;
            SetDestination(wholeNotePos, (0.5f / TimeManager.Instance.BPS));
        }

        private void Jetpack()
        {
            if (IsJumping)
            {
                LerpTowardPositionY();
            }
        }

        public void MoveVelocity()
        {
            if (_isMoving)
            {
                //move right
                _rb.velocity = new Vector3(speed, _rb.velocity.y, _rb.velocity.z);
            }
        }

        public void MoveTranslate()
        {
            if (_isMoving)
            {
                this.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            //_animator.animator.SetBool("isWalking", _isMoving);
        }

        public void UpdateData(IObservable o)
        {
            if (o is Level)
            {
                if (Level.Instance.mode == Level.Mode.Creating)
                {
                    _isPlaying = false;
                    Stop();
                }
            }

            if (o is TimeManager)
            {
                if (_isPlaying && !_isEnding)
                {
                    UpdateGridPosition();
                    FindNextPiece();
                }
            }
        }
        #endregion

        #region Input Actions
        public void PowInput()
        {
            _canPow = !_isTouchingBelow && !IsJumping;

            if (_canPow)
            {
                _canPow = false;
                POW();
            }
        }

        private void JumpInput()
        {

            JumpHold();

        }

        public void JumpRelease()
        {
            //ON RELEASE
            
            _jumpSustainTimer = 0;
            if (IsJumping) { IsJumping = false; _rb.useGravity = true; _isMoving = true; }
            
        }

        public void JumpHold()
        {
            Debug.Log("Being held");
            if (IsJumping)
            {
                _jumpSustainTimer += Time.deltaTime;

                if (_jumpSustainTimer >= _jumpSustainDuration)
                {
                    IsJumping = false;
                    _jumpSustainTimer = 0;
                    _rb.useGravity = true;
                    //_isMoving = true;
                }
            }
        }

        public void JumpPress()
        {
            //ON PRESSED
            if (CanJump)
            {
                if (_isHalfNoteMoving)
                {
                    StopHalfNoteMoving();
                }

                CanJump = false;
                IsJumping = true;
                //_animator.animator.SetTrigger("jumpTrigger");
                SetJetpackDestination();
            }
        }


        public void PlayInput()
        {
            Play();
            LadyBugController.Instance.Play();
            
        }
        #endregion
    }
}
