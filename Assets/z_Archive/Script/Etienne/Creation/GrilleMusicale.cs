using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrilleMusicale : MonoBehaviour
{
    //Musical grid data for platforms and sounds

    //Position X and Y of platforms;
    //X is based upon jump and beats in the song
    //Y is based upon set notation of pitch
    //Just data needed

    //Need feedback for this grid, but apart
    private GrilleMusical_LigneFeedback grilleMusical_LigneFeedback { get { return GameObject.FindObjectOfType<GrilleMusical_LigneFeedback>(); } }

    //Positions for platforms to work
    [Header("Positions")]
    public List<Vector2> positions = new List<Vector2>();

    //How to calculate the X position???
    [Header("X info")]
    public float _startXpositions;
    public float _levelWidthX;
    public float _beatsPerMesure;
    public float _nbrOfMesure;

    [HideInInspector]
    public float _endXpositions, _distanceBetweenBeats;//_levelWidthX / (_beatsPerMesure * _nbrOfMesure);

    //How to calculate the Y positions???
    [Header("Y info")]
    public float _startYpositions;
    public float _levelHeightY;//_endYpositions - _startYpositions or
    public float _nbrDegrees;

    [HideInInspector]
    public float _endYpositions, _distanceBetweenDegrees;//_startYpositions + _levelHeightY or //_levelHeightY/_nbrDegrees

    private void Start()
    {
        //Calculate poins positions
        _endXpositions = CalculateLastPoint(_startXpositions, _levelWidthX);
        _endYpositions = CalculateLastPoint(_startYpositions, _levelHeightY);

        //Calculate distance for division
        _distanceBetweenBeats = CalculateDistanceBetweenDivision(_levelWidthX,_nbrOfMesure*_beatsPerMesure);
        _distanceBetweenDegrees = CalculateDistanceBetweenDivision(_levelHeightY,_nbrDegrees);

        //Generate Grid
        positions = GenerateGridPositions();

        //Generate grid
        grilleMusical_LigneFeedback.GenerateGridVisuals(positions);
    }

    private float CalculateDistanceFromTwoPoints(float _start, float _end)
    {
        return _end - _start;
    }

    private float CalculateLastPoint(float _start, float distance)
    {
        return _start + distance;
    }

    private float CalculateDistanceBetweenDivision(float _distance, float _divisions)
    {
        return _distance / _divisions;
    }

    private List<Vector2> GenerateGridPositions()
    {
        List<Vector2> newGrille = new List<Vector2>();

        for (int x = 0; x < (_beatsPerMesure*_nbrOfMesure); x++)//for width
        {
            for (int y = 0; y < _nbrDegrees; y++)//for height
            {
                Vector2 newPos = new Vector2();

                newPos.x = _startXpositions + (x * _distanceBetweenBeats);
                newPos.y = _startYpositions + (y * _distanceBetweenDegrees);

                //Debug.Log(newPos);

                newGrille.Add(newPos);
            }
        }

        return newGrille;
    }
}
