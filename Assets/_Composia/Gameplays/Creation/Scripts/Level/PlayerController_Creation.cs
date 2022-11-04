using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace Composia
{

    public class PlayerController_Creation : Singleton<PlayerController_Creation>, IObserver
    {
        public Color selectedColor;
        private Level _level;

        private GameObject _selectedPiece;

        public Camera creationCam;

        [SerializeField]
        private SpriteRenderer[] _currentWorldTiles;
        [SerializeField]
        private SpriteRenderer[] _previousWorldTiles;
        public LayerMask ignoreLayerMask;

        public List<GameObject> _addedPieces = new List<GameObject>();
        public GameObject cursor;
        public float cursorSpeed = 300;

        private Vector2 cursorPosition;
        private Vector3 moveDirection;

        public bool isKeyboard = true;

        // Start is called before the first frame update
        void Start()
        {
            _level = Level.Instance;
            //UpdateData(CompositionInputSystem.Instance);
            CompositionInputSystem.Instance.AddObserver(this);
        }

        // Update is called once per frame
        void Update()
        {            
            MouseRaycast();
            //PlacePiece();
            //RemovePiece();
            MoveCursor();
        }

        public void SwitchToPlatforming()
        {
            Level.Instance.SwitchModes();
        }

        void MouseRaycast()
        {
            Vector3 screenpoint;

            if (!isKeyboard) { screenpoint = cursor.transform.position; }
            else
            {
                screenpoint = Mouse.current.position.ReadValue();
                //Debug.Log("IsMouse");
            }

            Ray ray = creationCam.ScreenPointToRay(screenpoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayerMask))
            {
                GridTileInfo info = hit.collider.GetComponent<GridTileInfo>();
                _previousWorldTiles = _currentWorldTiles;
                if (RolesController.Instance.GetMode() == Mode.Harmony)
                {
                    if (info.ReturnGridPos().y < 4)
                    {
                        Debug.LogWarning("Cannot place chord this low");
                        if (_previousWorldTiles != null) { ChangeGridTilesColors(_previousWorldTiles, Color.white); }
                        if (_currentWorldTiles[0] != null) { _currentWorldTiles[0] = info.GetComponent<SpriteRenderer>(); }
                        return;
                    }
                }
                _currentWorldTiles = ReturnGridTiles(info);

                if (_currentWorldTiles[0] == null || _previousWorldTiles == null) { return; }
                //if(_currentWorldTiles.Length < 1) { return; } if(_previousWorldTiles.Length < 1) { _previousWorldTiles = new SpriteRenderer[1]; }
                if (_currentWorldTiles[0] == _previousWorldTiles[0] && _currentWorldTiles != null) { return; }

                if (_previousWorldTiles != null) { ChangeGridTilesColors(_previousWorldTiles, Color.white); }
                if (_selectedPiece != null)
                {
                    if (!Level.Instance.IsInsideMeasure(_currentWorldTiles[0].GetComponent<GridTileInfo>()._gridPos.x, _selectedPiece.GetComponent<LevelPiece>().size))
                    {
                        ChangeGridTilesColors(_currentWorldTiles, Color.red);
                    }
                    else 
                    {
                        ChangeGridTilesColors(_currentWorldTiles, selectedColor);
                        //Debug.Log("Changing tiles to color " + selectedColor);
                    }
                }
                else
                {
                    ChangeGridTilesColors(_currentWorldTiles, selectedColor);
                }   
                
            }
            else 
            {
                if (_currentWorldTiles != null) { ChangeGridTilesColors(_currentWorldTiles, Color.white); _currentWorldTiles = null; }
                _previousWorldTiles = null;
            }
        }

        public void ReturnToWorld()
        {
            SceneManager.LoadScene("Demo_Menu");
        }

        private void ChangeGridTilesColors(SpriteRenderer[] sprites, Color c)
        {
            foreach (SpriteRenderer sr in sprites)
            {
                if(sr == null) { break; }
                sr.color = c;
            }
        }

        SpriteRenderer[] ReturnGridTiles(GridTileInfo info)
        {
            SpriteRenderer[] temp;

            if (_selectedPiece != null)
            {
                int pieceSize = _selectedPiece.GetComponent<LevelPiece>().size;
                int size = pieceSize;
                if (RolesController.Instance.GetMode() == Mode.Harmony)
                {
                    size *= 3;
                    temp = new SpriteRenderer[size];             
                }
                else
                {
                    temp = new SpriteRenderer[size];               
                }

                 
                for (int i = 0; i < pieceSize; i++)
                {
                    temp[i] = GridRuntime.Instance.gridTiles[(info._gridPos.y * Level.Instance.TotalColumns) + info._gridPos.x + i].GetComponent<SpriteRenderer>();
                }

                if (RolesController.Instance.GetMode() == Mode.Harmony)
                {

                    for (int i = pieceSize; i < (pieceSize * 2); i++)
                    {
                        temp[i] = GridRuntime.Instance.gridTiles[((info._gridPos.y - 2) * Level.Instance.TotalColumns) + info._gridPos.x + (i % pieceSize)].GetComponent<SpriteRenderer>();
                    }

                    for (int i = pieceSize * 2; i < (pieceSize * 3); i++)
                    {
                        temp[i] = GridRuntime.Instance.gridTiles[((info._gridPos.y - 4) * Level.Instance.TotalColumns) + info._gridPos.x + (i % pieceSize)].GetComponent<SpriteRenderer>();
                    }
                    
                }

            }
            else 
            {
                temp = new SpriteRenderer[1];
                temp[0] = info.GetComponent<SpriteRenderer>();
            }

            return temp;
        }

        //The player is dragging a piece from the inventory
        void DragPieceFromInventory()
        {

        }

        //The player is dragging a piece from inside the world
        void DragPieceFromWorld()
        {

        }

        public void CachePiece(NoteTypeEnum _type)
        {
            _selectedPiece = Inventory_Archive.Instance.ReturnInventoryObject(_type);
        }

        public void ExecuteAction()
        { 
            
        }

        public void ChoosePiece()
        {
            if (isKeyboard)
            { 
                
            }
        }

        //Thep player places a piece either into the world or back into their inventory
        public void PlacePiece()
        {
            if(_currentWorldTiles == null || _selectedPiece == null)
            {
                return;
            }

            //if (Mouse.current.leftButton.wasPressedThisFrame)
            //{
                Vector2Int gridTilePos = _currentWorldTiles[0].GetComponent<GridTileInfo>().ReturnGridPos();
                Paint(gridTilePos.x, gridTilePos.y, _selectedPiece.GetComponent<LevelPiece>());
            //}
        }


        //The player removes a piece rom the world, reutrning the piece to their inventory
        void Remove(int col, int row)
        {
            LevelPiece piece = _level.FindPiece(col, row);

            if (piece != null)
            {
                _level.EraseFromMeasure(col, row);
                //_addedPieces.Remove(obj);
                piece.Erase();
            }

        }

        GameObject FindLocalPiece(int col, int row)
        { 
            if(_addedPieces.Count <= 0) 
            { 
                Debug.LogWarning("There are no pieces");
                return null; 
            }

            foreach (GameObject p in _addedPieces)
            {
                LevelPiece lp = p.GetComponent<LevelPiece>();
                if(lp.x == col && lp.y == row)
                {
                    return p;
                }
            }

            Debug.LogWarning("There is no piece here at: [" + col + "] [" + row + "]");
            return null;
        }

        public void Paint(int col, int row, LevelPiece p)
        {
            //CHECK IF SPOT IS VALID
            // Check out of bounds and if we have a piece selected
            if (!_level.IsInsideGridBounds(col, row))
            {
                Debug.LogWarning("Out of bounds or no piece selected!");
                return;
            }
            
            if (!_level.IsInsideMeasure(col, p.size))
            {
                Debug.LogWarning("Piece does not fit within this measure");
                return;
            }

            //CHECK IF THERE IS PIECE THERE ALREADY
            int measureIndex = _level.FindMeasureIndex(col);
            LevelPiece[] inspectedPieces = _level.FindPieces(col, row, p.size, measureIndex);

            foreach (LevelPiece inspected in inspectedPieces)
            {
                if (inspected != null)
                {
                    Debug.Log("Cannot place block here");
                    //DISPLAY NEGATIVE FEEDBACK
                    return;
                }
            }

            //Check to see if there are any pieces in the same column
            if (_level.CheckColumns(col, p.size, measureIndex))
            {
                Debug.LogWarning("This column is already occupied");
                return;
            }

            if (RolesController.Instance.GetMode() == Mode.Harmony)
            {
                if(row < 4) 
                {
                    Debug.Log("Cannot place chord this low!");
                    return;
                }
                PlacePiece(col, row - 2, measureIndex, p);
                PlacePiece(col, row - 4, measureIndex, p);
            }

            PlacePiece(col, row, measureIndex, p);
        }

        private void PlacePiece(int col, int row, int measureIndex, LevelPiece p)
        {
            // Do paint !
            GameObject obj = Instantiate(p.gameObject);
            obj.transform.parent = RolesController.Instance.CurrentRole.measures[measureIndex].transform;
            obj.name = string.Format("[{0},{2}][{3}][S{1}]", col, p.size, row, obj.name);
            obj.transform.position = _level.GridToWorldCoordinates(col, row);


            _level.AddLevelPieces(col, row, obj.GetComponent<LevelPiece>(), measureIndex);
        }

        public void AddLevelPieces(int col, int row, LevelPiece piece, int measureIndex = 99)
        {
            piece.x = col;
            piece.y = row;

            for (int i = 0; i < piece.size; i++)
            {
                _level.CopyToMeasure(col + i, row, piece, measureIndex);
            }
        }

        #region INPUTS

        public void RemovePiece()
        {
            if (_currentWorldTiles != null)
            {
                
                Vector2Int gridTilePos = _currentWorldTiles[0].GetComponent<GridTileInfo>().ReturnGridPos();
                Remove(gridTilePos.x, gridTilePos.y);
                
            }
        }

        public void GetMoveVector(Vector2 direction)
        {
            if (!cursor.activeSelf) 
            { 
                cursor.SetActive(true);
                cursor.transform.position = direction;
            }
            //Debug.Log("Moving cursor");
            moveDirection = direction;
        }

        public void MoveCursor()
        {
            cursor.transform.position += moveDirection * cursorSpeed * Time.deltaTime;
        }

        public void SetCursorToCenter()
        {
            cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(20, 38);
        }
        public void SetCursorPositionToMeasure(int measureIndex)
        {
            Vector3 pos = Level.Instance.GridToWorldCoordinates((Level.Instance.MeasureLength - 1) * measureIndex, 0);

            Vector2 viewport = creationCam.WorldToViewportPoint(pos);

            cursor.GetComponent<RectTransform>().anchoredPosition = viewport;
            //Sets the position of the image cursor to be at the correct measure
        }

        public void UpdateData(IObservable o)
        {
            if (o is CompositionInputSystem)
            {
                isKeyboard = (o as CompositionInputSystem).IsKeyboard;
                //Debug.Log("IsKeyboard = " + isKeyboard);
            }
        }

        #endregion
    }
}
