using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme_BeatLongueur : MonoBehaviour
{
    //Manage the platform longuer par-rapport au beat
    private GrilleMusicale _grilleMusicale { get { return GameObject.FindObjectOfType<GrilleMusicale>(); } }

    public GameObject _cubeRenderer;
    public float _beatValue = 1f;

    private void Start()
    {
        //New scale
        _cubeRenderer.transform.localScale = 
            new Vector3(
            _grilleMusicale._distanceBetweenBeats*_beatValue - (_grilleMusicale._distanceBetweenBeats/2),//To create illusion of space between platforms
            _grilleMusicale._distanceBetweenDegrees
            ,_cubeRenderer.transform.localScale.z);

        //X position
        _cubeRenderer.transform.localPosition = new Vector3(
            _cubeRenderer.transform.localPosition.x + (_cubeRenderer.transform.localScale.x/2),
            _cubeRenderer.transform.localPosition.y - (_cubeRenderer.transform.localScale.y/2),
            _cubeRenderer.transform.localPosition.z);
    }

}
