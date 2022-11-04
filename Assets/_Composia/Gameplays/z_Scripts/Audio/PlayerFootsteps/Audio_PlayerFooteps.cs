using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_PlayerFooteps : MonoBehaviour
{
    public AK.Wwise.Event _footsteps;

    public bool _canPlayFootsteps;
    private float _currentTimer;
    public float _nextFootstepTimer;

    private void Update()
    {
        //Manage timer for footstep
        if (!_canPlayFootsteps)
        {
            _currentTimer += Time.deltaTime;

            if (_currentTimer >= _nextFootstepTimer)
            {
                _canPlayFootsteps = true;
            }
        }
    }

    //Play footstep
    public void Audio_PlayerFootsteps()
    {
        if (_canPlayFootsteps)
        {
            _footsteps.Post(gameObject);
            _canPlayFootsteps = false;
            _currentTimer = 0f;
        }        
    }
}

