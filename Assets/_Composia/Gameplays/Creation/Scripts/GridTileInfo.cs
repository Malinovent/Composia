using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileInfo : MonoBehaviour
{
    [ReadOnly]
    public Vector2Int _gridPos;
    
    [SerializeField]
    private bool _isValidPlacement;
  
    public bool IsValidPlacement { 
        get => _isValidPlacement;
        set 
        { 
            _isValidPlacement = value;
            if (_isValidPlacement) { GetComponent<SpriteRenderer>().color = Color.green; }
            else { GetComponent<SpriteRenderer>().color = Color.white; }
        }
    }

    public void SetGridPos(int col, int row)
    {
        _gridPos.x = col;
        _gridPos.y = row;     
    }

    public Vector2Int ReturnGridPos()
    { 
        return _gridPos;
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
