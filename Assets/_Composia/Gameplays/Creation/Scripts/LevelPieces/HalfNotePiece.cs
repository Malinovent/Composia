using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class HalfNotePiece : NotePiece
    {
        private bool _isMoving = false;
        private Vector3 _targetPos;
        private Vector3 _startingPos;
        public float moveSpeed;
        public float bufferDistance;
        private float _timeToReachDestination = 0;

        public GameObject movingBlock;

        private void Awake()
        {
            _targetPos = Level.Instance.GridToWorldCoordinates(x + size - 1, y);
            _startingPos = movingBlock.transform.position;
            moveSpeed *= 120 / TimeManager.Instance.BPM;

            //BPS * 2
            _timeToReachDestination = (60f / TimeManager.Instance.BPM) * 2;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_isMoving)
            {
                LerpTowardPosition();

                if (Vector3.Distance(movingBlock.transform.position, _targetPos) < bufferDistance && _isMoving)
                {
                    _isMoving = false;
                    PlayerController_PlatformerRigidbody.Instance.StopHalfNoteMoving();
                }

                //if()
            }
        }

        private float t;


        private void LerpTowardPosition()
        {
            t += Time.deltaTime / _timeToReachDestination;
            movingBlock.transform.position = Vector3.Lerp(_startingPos, _targetPos, t);
        }

        public override void Restart()
        {
            movingBlock.transform.position = _startingPos;
            t = 0;
            _isMoving = false;
            isActivated = false;
            
        }

        public override void Activate(bool isPow)
        {
            base.Activate(isPow);
            _isMoving = true;       
        }

    }
}
