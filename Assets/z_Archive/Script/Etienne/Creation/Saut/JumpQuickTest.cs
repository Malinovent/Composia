using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpQuickTest : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _rigidbody { get { return GetComponent<Rigidbody>(); } }
    public RenderJumpPossibility _renderJumpPossibility;
    public RenderJumpPossibility _minJump;
    public RenderJumpPossibility _maxJump;

    public float _BPM;
    public float _BPS;

    public float _XBeatDistance;
    public float _YDegreeDistance;

    public float _BeatPerMesure;
    public float _NbrOfMesure;
    public float _totalMesure;

    public float _gravity;

    public AnglesData _XAngles;
    public AnglesData _YAngles;
    public float _angleFloat;
    public float _velocity;

    public float _jumpForce;

    public float _timeForAJump;

    private Coroutine _jumpCoroutine;

    [HideInInspector]
    public bool _once;
    private bool _forceReleased;

    private void Start()
    {
        _totalMesure = _BeatPerMesure * _NbrOfMesure;
        _gravity = Mathf.Abs(Physics.gravity.y);

        //Initialize
        ResetAnglesData(_XAngles);
        ResetAnglesData(_YAngles);
    }

    private void Update()
    {
        //Calculating the float angle value
        Vector3 _angle = new Vector3(_XAngles._currentAngle, _YAngles._currentAngle, 0);
        Vector3 xVector = new Vector3(_angle.x, 0, 0);
        Vector3 yVector = new Vector3(_angle.x, _angle.y, 0);
        _angleFloat = Vector3.Angle(xVector, yVector);
        //Debug.Log("Angle: " + _angleFloat);

        //Caulculating the velocity float value
        _velocity = ((Vector3.Magnitude(_rigidbody.velocity) + Vector3.Magnitude(_angle * _jumpForce))) * Time.fixedDeltaTime;      
        //Debug.Log("Current Velocity: " + _velocity);

        //Render other jump values
        //Min jump possible
        Vector3 _angleMin = new Vector3(_XAngles._minAngle, _YAngles._minAngle, 0);
        Vector3 xVectorMin = new Vector3(_angleMin.x, 0, 0);
        Vector3 yVectorMin = new Vector3(_angleMin.x, _angleMin.y, 0);
        float _angleMinFloat = Vector3.Angle(xVectorMin, yVectorMin);
        float _velocityMin = ((Vector3.Magnitude(_rigidbody.velocity) + Vector3.Magnitude(_angleMin * _jumpForce))) * Time.fixedDeltaTime;

        //Max jup possible
        Vector3 _angleMax = new Vector3(_XAngles._maxAngle, _YAngles._maxAngle, 0);
        Vector3 xVectorMax = new Vector3(_angleMax.x, 0, 0);
        Vector3 yVectorMax = new Vector3(_angleMax.x, _angleMax.y, 0);
        float _angleMaxFloat = Vector3.Angle(xVectorMax, yVectorMax);
        float _velocityMax = ((Vector3.Magnitude(_rigidbody.velocity) + Vector3.Magnitude(_angleMax * _jumpForce))) * Time.fixedDeltaTime;

        //Jump
        //if (Input.GetKey(KeyCode.Space) && !_forceReleased)
        if (Keyboard.current.spaceKey.isPressed && !_forceReleased)
        {
            //Calculate angle for the jump to work// based it on time this time
            //_XAngles._currentAngle += (_XAngles._timeForMinToMaxAngle * Time.deltaTime);
            //_YAngles._currentAngle += (_YAngles._timeForMinToMaxAngle * Time.deltaTime);
            //_XAngles._clampedT += (1f * Time.deltaTime) /_XAngles._timeForMinToMaxAngle;
            //_YAngles._clampedT += (1f * Time.deltaTime) /_YAngles._timeForMinToMaxAngle;

            _XAngles._clampedT += (1f * Time.fixedDeltaTime) /_XAngles._timeForMinToMaxAngle;
            _YAngles._clampedT += (1f * Time.fixedDeltaTime) /_YAngles._timeForMinToMaxAngle;

            _XAngles._currentAngle = Mathf.Lerp(_XAngles._minAngle, _XAngles._maxAngle, _XAngles._clampedT);
            _YAngles._currentAngle = Mathf.Lerp(_YAngles._minAngle, _YAngles._maxAngle, _YAngles._clampedT);

            //Check for max and stop when at max
            if (_XAngles._currentAngle >= _XAngles._maxAngle ||
                _XAngles._clampedT >= 1f)
            {
                _XAngles._currentAngle = _XAngles._maxAngle;
                _XAngles._clampedT = 1f;
                _forceReleased = true;
            }
            if (_YAngles._currentAngle >= _YAngles._maxAngle ||
                _YAngles._clampedT >= 1f)
            {
                _YAngles._currentAngle = _YAngles._maxAngle;
                _YAngles._clampedT = 1f;
                _forceReleased = true;
            }

            //Render jump line renderer
            _renderJumpPossibility.CheckJump(_angleFloat, _velocity);

            _minJump.CheckJump(_angleMinFloat, _velocityMin);
            _maxJump.CheckJump(_angleMaxFloat, _velocityMax);

            //Logic
            _once = true;
        }

        //if (Input.GetKeyUp(KeyCode.Space) && _once)
        if (!Keyboard.current.spaceKey.isPressed && _once)
        {
            Debug.Log("Jump!");

            //Actual jump
            //_rigidbody.AddForce(_angle * _jumpForce, ForceMode.Force);

            //Get path
            Vector3[] _positions = new Vector3[_renderJumpPossibility.lr.positionCount];
            for (int a = 0; a < _renderJumpPossibility.lr.positionCount; a++)
            {
                _positions[a] = (_renderJumpPossibility.lr.GetPosition(a));
            }
            
            //Jump mechanics
            _jumpCoroutine = StartCoroutine(FalseJumpMovement(_positions, _timeForAJump));

            //Reset
            ResetAnglesData(_XAngles);
            ResetAnglesData(_YAngles);

            _XAngles._clampedT = 0f;
            _YAngles._clampedT = 0f;

            _once = false;
            _forceReleased = false;
        }
    }

    private IEnumerator FalseJumpMovement(Vector3[] jumpPositions, float timeToMove)
    {
        float timeBetweenPos = timeToMove / (float)(jumpPositions.Length - 1);
        //Debug.Log("TimeBetweenPos : " + timeBetweenPos);
        float currentTimer = 0f;
        float totalTimeJump = 0f;

        _rigidbody.useGravity = false;

        for (int i = 1; i < jumpPositions.Length; i++)
        {
            currentTimer = 0f;

            transform.position = jumpPositions[i - 1];

            while (transform.position != jumpPositions[i])
            {
                //Clamped time
                currentTimer += (1f * Time.deltaTime) / (float)timeBetweenPos;
                totalTimeJump += (1f * Time.deltaTime);

                //Move
                transform.position = new Vector3(
                    Mathf.Lerp(jumpPositions[i - 1].x, jumpPositions[i].x, Mathf.Clamp01(currentTimer)),
                    Mathf.Lerp(jumpPositions[i - 1].y, jumpPositions[i].y, Mathf.Clamp01(currentTimer)),
                    0);

                //Debug.Log("Ping " + Time.time);

                yield return null;
            }

            transform.position = jumpPositions[i];
        }

        Debug.Log("Time to jump from code " + totalTimeJump);

        _rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_jumpCoroutine != null)
            StopCoroutine(_jumpCoroutine);
    }

    private void ResetAnglesData (AnglesData angle)
    {
        angle._currentAngle = angle._minAngle;
    }
}

[System.Serializable]
public class AnglesData
{
    public float _currentAngle;
    public float _minAngle;
    public float _maxAngle;
    public float _timeForMinToMaxAngle;
    [HideInInspector]
    public float _clampedT;
}
