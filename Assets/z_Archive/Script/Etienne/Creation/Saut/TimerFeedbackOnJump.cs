using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerFeedbackOnJump : MonoBehaviour
{
    private JumpQuickTest _jumpQuickTest { get { return GetComponent<JumpQuickTest>(); } }

    public TextMeshProUGUI _timerTimeInJump;
    public TextMeshProUGUI _timerChargingJump;

    [HideInInspector]
    public float _timerJump, _timerCharge;

    private float _timeSinceStart, _currentTime, _timeAtStartJump, _timeAtStartCharge;

    private bool _inAir;

    private void Awake()
    {
        _timeSinceStart = Time.time;
    }

    private void OnCollisionExit(Collision collision)
    {
        _inAir = true;
        _timeAtStartJump = _currentTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _inAir = false;

        if (_timerJump > 0)
        {
            Debug.Log("Time Landing " + _timerJump);
        }

        _timerJump = ResetTime(_timerTimeInJump, "Jump Time: ", _timerJump);
    }

    private void Update()
    {
        _currentTime = Time.time - _timeSinceStart;

        if (_jumpQuickTest._once)
        {
            _timerCharge = CalculateTime(_timerChargingJump, "Charge Time: ", _timerCharge, true, _jumpQuickTest._XAngles._timeForMinToMaxAngle, _timeAtStartCharge);
        }
        else if (!_jumpQuickTest._once && _timerCharge > 0)
        {
            _timerCharge = ResetTime(_timerChargingJump, "Charge Time: ", _timerCharge);
        }
        else
        {
            _timeAtStartCharge = _currentTime;
        }

        if (_inAir)
        {
            _timerJump = CalculateTime(_timerTimeInJump, "Jump Time: ", _timerJump, false, 0, _timeAtStartJump);
        }
    }

    //As long this function is called, calculate time
    public float CalculateTime(TextMeshProUGUI _timer, string _name, float timerUsed, bool _isClamped, float _maxClamped, float timeAtJump)
    {
        float time = timerUsed;
        time = Time.time - timeAtJump;
        if (_isClamped)
        {
            time = Mathf.Clamp(time, 0, _maxClamped);
        }
        _timer.text = _name + timerUsed.ToString();

        return time;
    }

    public float ResetTime(TextMeshProUGUI _timer, string _name, float timerUsed)
    {
        timerUsed = 0;
        //_timer.text = _name + timerUsed.ToString();

        return timerUsed;
    }
}
