using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Deplacement : MonoBehaviour
{
    private GrilleMusicale _grilleMusicale { get { return GameObject.FindObjectOfType<GrilleMusicale>(); } }
    private PlayerController _playerController { get { return GameObject.FindObjectOfType<PlayerController>(); } }
    
    public float _bpm;
    public float _speed;

    private void Start()
    {
        float startPos = _grilleMusicale._startXpositions;
        transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (_playerController._canWalk)
        {
            TranslateDroneSideToSide();
        }
    }

    private void TranslateDroneSideToSide()
    {
        //Get last position
        float endPos = _grilleMusicale._endXpositions - (_grilleMusicale._distanceBetweenBeats);//Initial X not calculated
        Debug.Log(endPos);
        Vector3 _v_endPos = new Vector3(endPos, transform.position.y, transform.position.z);

        //Calculate the speed
        //S = D*t;
        _speed = _grilleMusicale._levelWidthX / (_grilleMusicale._beatsPerMesure * _grilleMusicale._nbrOfMesure * (60/_bpm));
        float step = _speed * Time.fixedDeltaTime;

        Debug.Log("Disatnce" + _grilleMusicale._levelWidthX);
        Debug.Log("Time" + (_grilleMusicale._beatsPerMesure * _grilleMusicale._nbrOfMesure * (60 / _bpm)));
        Debug.Log("Step" + step);

        //Move
        transform.position = Vector3.MoveTowards(transform.position, _v_endPos, step);
    }
}
