using System.Collections.Generic;
using UnityEngine;

public class MusicTimer : MonoBehaviour, IRestartable
{
    public bool isModifiableOnRuntime = false;

    public bool isDebug = false;
    [SerializeField]
    private bool _isPlaying = false;
    private List<float> _expectedTimes = new List<float>();

    [SerializeField][Range(3,4)]
    private int _timeSignature;
    [SerializeField]
    private int _bpm = 60;
    private int _startingBPM = 60;
    private float _bps;
    private float _sixteenthBPS;
    private int _currentSixteenthBeat = 0;
    private int _currenBeat = 0;
    private int _lastBeat;

    private float _delay = 0;
    //private float _timer = 0;

    #region GETTERS
    public int TimeSignature { get => _timeSignature; set => _timeSignature = value; }
    public int BPM 
    { 
        get => _bpm;
        private set 
        {
            _bpm = value; 
            if(_bpm > _startingBPM + 20) { _bpm = _startingBPM + 20; }
            if(_bpm < _startingBPM - 20) { _bpm = _startingBPM - 20; }
        }
    }
    public float BPS { get => _bps; set => _bps = value; }
    public float SixteenthBPS { get => _sixteenthBPS; set => _sixteenthBPS = value; }
    public int CurrentSixteenthBeat { get => _currentSixteenthBeat; set => _currentSixteenthBeat = value; }
    public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }

    #endregion

    #region OVERRIDES

    private void Start()
    {       
        AdjustBPM(BPM);
        SetStartingBPM();
        CalculateExpectedTimes();

        if (_isPlaying)
        {
            StartPlaying();
        }
    }

    #endregion

    public bool IsLastBeat()
    {
        return _lastBeat == CurrentSixteenthBeat;
    }
    public void CalculateLastBeat()
    {
        _lastBeat = (TimeSignature * 16) - 1;
    }

    private void CalculateExpectedTimes()
    {       
        if (isDebug)
        {
            int numOfBeats = TimeSignature * 4; //Mulitply the TimeSignature with the number of measures(4) to get the total number of beats
            for (int i = 0; i < numOfBeats; i++)
            {
                _expectedTimes.Add(i * BPS);
            }
        }
    }

    public void AdjustBPM(int newBPM)
    {
        BPM = newBPM;
        BPS = 60f / BPM;
        SixteenthBPS = BPS * 0.25f;       
    }

    public void SetStartingBPM()
    {
        _startingBPM = BPM;
    }

    private void SixteenthBeatRepeating()
    {
        DebugTimer();
        OnSixteenthBeat();
        if (IsPlaying) { _currentSixteenthBeat++; }

    }

    private void SixteenthBeat()
    {
        DebugTimer();
        Invoke("SixteenthBeat", SixteenthBPS);
        OnSixteenthBeat();

        _currentSixteenthBeat++;
    }

    protected virtual void OnSixteenthBeat()
    { 
    
    }
    public virtual void StartPlaying()
    {
        if (isModifiableOnRuntime)
        {
            Invoke("SixteenthBeat", 0);
        }
        else
        {
            InvokeRepeating("SixteenthBeatRepeating", _delay, SixteenthBPS);
        }
        IsPlaying = true;
    }

    public void PausePlaying()
    {
        IsPlaying = false;
    }

    public virtual void Restart()
    {
        CancelInvoke();
        PausePlaying();
        CurrentSixteenthBeat = 0;
        _currenBeat = 0;
    }

    private void DebugTimer()
    {
        if (isDebug && _currentSixteenthBeat % 4 == 0)
        {
            Debug.Log("Playing beat " + _currenBeat + " at time: " + (Time.time));
            _currenBeat++;
        }
    }
}
