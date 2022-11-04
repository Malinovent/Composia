using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    //Make camera lerp movement to smooth gameplay
    private Transform _currentTransform { get { return GetComponent<Transform>(); } }

    public Transform _transformFollow;
    public float _lerpSpeed;

    private void FixedUpdate()
    {
        //Lerp the camera
        _currentTransform.position = Vector3.Lerp(
            _currentTransform.position, 
            _transformFollow.position, 
            _lerpSpeed * Time.fixedDeltaTime);
    }

}
