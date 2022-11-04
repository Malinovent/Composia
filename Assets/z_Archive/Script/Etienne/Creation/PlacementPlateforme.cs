using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPlateforme : MonoBehaviour
{
    //Joueur peut cliquer sur le bouton
    //Glisser la plateforme vers la grille
    //Poser plateforme 'a une position predeterminer
    private GrilleMusicale _grilleMusicale { get { return GameObject.FindObjectOfType<GrilleMusicale>(); } }

    public GameObject _plateforme;
    public GameObject _currentPlateforme;
    public bool _isPlacingPlateforme;
    public bool _isOnGrid;

    public void Update()
    {
        if (_isPlacingPlateforme)
        {
            //Get mouse position
            Vector3 mousePos = Input.mousePosition;
            Vector3 correctMousePos = new Vector3(mousePos.x, mousePos.y, 10);
            //make sure the z is aligned with the level and the camera
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(correctMousePos);

            //Move platforme around
            _currentPlateforme.transform.position = MagnetiseToGridPosition(worldPosition);
        }

        if (Input.GetMouseButtonDown(0) && _isPlacingPlateforme)
        {
            if (_isOnGrid)
            {
                //it is good
            }
            else
            {
                //Dont keep the platform
                Destroy(_currentPlateforme.gameObject);
            }

            //Reset
            _currentPlateforme = null;

            _isOnGrid = false;
            _isPlacingPlateforme = false;
        }
    }

    public void GetANewPlateforme()
    {
        //Instantiate object and make sure it follow the mouse
        _currentPlateforme = Instantiate(_plateforme);

        //Is placing plateforme
        _isPlacingPlateforme = true;
    }

    public Vector3 MagnetiseToGridPosition(Vector3 position)
    {
        float magnetizeValueX = _grilleMusicale._distanceBetweenBeats / 2;
        float magnetizeValueY = _grilleMusicale._distanceBetweenDegrees / 2;

        Debug.Log(magnetizeValueX);

        foreach (Vector2 pos in _grilleMusicale.positions)
        {
            if (pos.x + magnetizeValueX >= position.x &&
                pos.x - magnetizeValueX <= position.x &&
                pos.y + magnetizeValueY >= position.y &&
                pos.y - magnetizeValueY <= position.y)
            {
                _isOnGrid = true;
                return pos;
            }
        }

        _isOnGrid = false;
        return position;
    }
}
