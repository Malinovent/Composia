using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace Composia.LevelCreator
{
    [CustomEditor(typeof(Level))]
    public class LevelInspector : Editor
    {
        private Level _myTarget;

        private PaletteItem _itemSelected;
        private PaletteItem _itemInspected;
        private Texture2D _itemPreview;
        private LevelPiece _pieceSelected;

        private int _originalPosX;
        private int _originalPosY;

        private GUIStyle _titleStyle;
        private GUIStyle _secondaryTitleStyle;
        private GUIStyle _centered;
        private GUIStyle _sceneText;

        //private bool in2DMode = false;

        private string tileStringPath = "Assets/_Composia/Gameplays/Creation/Level Objects/GridTile.prefab";
        private string tileStringPathMeasure = "Assets/_Composia/Icons.png/GridPiece_5px/Measure";
        //private string tileStringPathBeat = "Assets/Sprite/GridPiece_5px_Measure.png";
        private GridTileInfo _currentGT;

        private bool _leftShiftIsPressed = false;

        [SerializeField]
        private List<NoteData> _copiedPieces = new List<NoteData>();


        private void InitStyles()
        {
            _titleStyle = new GUIStyle();
            _titleStyle.alignment = TextAnchor.MiddleCenter;
            _titleStyle.fontSize = 16;

            Texture2D titleBG = (Texture2D)Resources.Load("Color_Bg");
            Font titleFont = (Font)Resources.Load("Oswald-Regular");
            _titleStyle.normal.background = titleBG;
            _titleStyle.normal.textColor = Color.white;
            _titleStyle.font = titleFont;

            GUISkin skin = (GUISkin)Resources.Load("LevelCreatorSkin");
            _titleStyle = skin.label;

            _secondaryTitleStyle = new GUIStyle();
            _secondaryTitleStyle.alignment = TextAnchor.MiddleCenter;
            _secondaryTitleStyle.fontSize = 12;
            _secondaryTitleStyle.normal.textColor = Color.white;

            _centered = new GUIStyle();
            _centered.alignment = TextAnchor.MiddleCenter;
            _centered.normal.textColor = Color.white;

            _sceneText = new GUIStyle();
            _sceneText.fontSize = 32;
            _sceneText.normal.textColor = Color.red;
        }

        public enum Mode { 
            View,
            Paint,
            Edit,
            Erase,
            Validate
        }

        private Mode _selectedMode;
        private Mode _currentMode;

        #region Overrides
        private void OnEnable()
        {

            _myTarget = (Level)target;

            InitLevel();
            InitStyles();
            //ResetResizeValues();      
            SubscribeEvents();
            //in2DMode = SceneView.currentDrawingSceneView.in2DMode;

        }

        private void OnDisable()
        {
            //SceneView.currentDrawingSceneView.in2DMode = in2DMode;
            UnsubscribeEvents();
            _myTarget.StopDrawCube();
        }


        private void OnDestroy()
        {
            //Debug.Log("OnDestroy was called...");
            _myTarget.StopDrawCube();
        }

        public override void OnInspectorGUI()
        {
            DrawLevelDataGUI();
            DrawPieceSelectedGUI();
            DrawInspectedItemGUI();
            DrawMeasuresGUI();
            DrawRolesGUI();
            DrawButtonOpenPaletteWindowGUI();
            DrawCopied();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_myTarget);
            }

        }

        private void OnSceneGUI()
        {
            DrawModeGUI();
            ModeHandler();
            EventHandler();
            DetectLeftShift();
            Handles.Label(new Vector3(8, 16f, 0), (RolesController.Instance.CurrentRole.measures[0].PieceGroupIndex + 1).ToString(), _sceneText);
            Handles.Label(new Vector3(24, 16f, 0), (RolesController.Instance.CurrentRole.measures[1].PieceGroupIndex + 1).ToString(), _sceneText);
            Handles.Label(new Vector3(40, 16f, 0), (RolesController.Instance.CurrentRole.measures[2].PieceGroupIndex + 1).ToString(), _sceneText);
            Handles.Label(new Vector3(56, 16f, 0), (RolesController.Instance.CurrentRole.measures[3].PieceGroupIndex + 1).ToString(), _sceneText);
        }

        #endregion

        private void DetectLeftShift()
        {
            if (Event.current.keyCode == KeyCode.LeftShift)
            {
                if (Event.current.type == EventType.KeyDown)
                {
                    _leftShiftIsPressed = true;
                }
                else if (Event.current.type == EventType.KeyUp)
                {
                    _leftShiftIsPressed = false;
                }
            }
        }

        #region DRAWERS
        private void DrawLevelDataGUI()
        {
            EditorGUILayout.LabelField("Data", _titleStyle);
            EditorGUILayout.BeginVertical("box");
            RolesController.Instance.IsMinorScale = EditorGUILayout.Toggle("Is Minor Scale?", RolesController.Instance.IsMinorScale);
            TimeManager.Instance.BPM = EditorGUILayout.IntField("BPM", TimeManager.Instance.BPM);
            TimeManager.Instance.TimeSignature = EditorGUILayout.IntSlider("Time Signature", TimeManager.Instance.TimeSignature, 3, 4);
            _myTarget.saveName = EditorGUILayout.TextField("SaveName", _myTarget.saveName);
            if(_myTarget.saveName == "") { EditorGUILayout.HelpBox("DO NOT LEAVE THE BOX ABOVE EMPTY OR THE SCENE WON'T SAVE PROPERLY", MessageType.Warning, true); }
            if (GUILayout.Button("Fix Everything!!"))
            {
                RolesController.Instance.LinkRolesPieceGroups();
                EditorUtility.SetDirty(this);
            }
            //_myTarget.Bgm = (AudioClip)EditorGUILayout.ObjectField("Bgm", _myTarget.Bgm, typeof(AudioClip), false);
            //_myTarget.Background = (Sprite)EditorGUILayout.ObjectField("Background", _myTarget.Background, typeof(Sprite), false);
            EditorGUILayout.EndVertical();
        }

        private void DrawPieceSelectedGUI()
        {
            EditorGUILayout.LabelField("Piece Selected", _titleStyle);
            if (_pieceSelected == null)
            {
                EditorGUILayout.HelpBox("No piece selected!", MessageType.Info);
            }
            else 
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(new GUIContent(_itemPreview), GUILayout.Height(40));
                EditorGUILayout.LabelField(_itemSelected.itemName);         
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawModeGUI()
        {
            List<Mode> modes = EditorUtils.GetListFromEnum<Mode>();
            List<string> modeLabels = new List<string>();
            foreach (Mode mode in modes)
            {
                modeLabels.Add(mode.ToString());
            }

            Handles.BeginGUI();

            GUILayout.BeginArea(new Rect(10f, 10f, 360, 40f));
            _selectedMode = (Mode)GUILayout.Toolbar((int)_currentMode, modeLabels.ToArray(), GUILayout.ExpandHeight(true));
            if(_selectedMode == Mode.View) { SceneView.currentDrawingSceneView.in2DMode = false; }
            GUILayout.EndArea();

            Handles.EndGUI();
        }

        private void DrawInspectedItemGUI()
        {
            if (_currentMode != Mode.Edit)
            {
                return;
            }

            EditorGUILayout.LabelField("Piece Editor", _titleStyle);

            if (_itemInspected != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Name : " + _itemInspected.name);
                CreateEditor(_itemInspected.inspectedScript).OnInspectorGUI();
                
                DrawGroupPieceGUI();
                EditorGUILayout.EndVertical();
            }
            else { 
                EditorGUILayout.HelpBox("No Piece to edit!", MessageType.Info); 
            }
        }

        private void DrawButtonOpenPaletteWindowGUI()
        {

            EditorGUILayout.LabelField("Tools", _titleStyle);

            if (PaletteWindow.instance == null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Palette");
                if (GUILayout.Button("OPEN"))
                {
                    PaletteWindow.ShowPalette();
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.HelpBox("Palette is already open!", MessageType.Info);
            }

            
            if (_myTarget.gridTiles == null)
            {
               
                _myTarget.gridTiles = (GameObject)EditorGUILayout.ObjectField("GridTile Parent", _myTarget.gridTiles, typeof(GameObject),true);
                EditorGUILayout.HelpBox("Link GridTilesParent", MessageType.Info);
                GUI.enabled = _myTarget.gridTiles;
            }
            
            if (GUILayout.Button("Draw Tiles"))
            {
                PaintTiles();
            }
            GUI.enabled = true;
        }

        private void DrawMeasuresGUI()
        {
            EditorGUILayout.LabelField("Measures", _titleStyle);

            EditorGUILayout.LabelField("Rotate a measures");

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(120));
           
            
            for (int i = 0; i < 4; i++)
            {

                Measure measure = RolesController.Instance.CurrentRole.measures[i];                
 
                EditorGUILayout.BeginVertical("Box");

                EditorGUILayout.LabelField("Measure " + (i + 1), _secondaryTitleStyle, GUILayout.MinWidth (80), GUILayout.MaxWidth(100));

                EditorGUILayout.BeginHorizontal(GUILayout.Width(20));
                EditorGUILayout.Space(5);
                GUILayout.FlexibleSpace();
                int j = 0;
                foreach (LevelPieceGroup group in measure.PieceGroups)
                {
                    EditorGUILayout.BeginVertical(GUILayout.Width(20));
                    EditorGUILayout.LabelField((j+1).ToString(), GUILayout.MaxWidth(20));
                    group.IsActive = EditorGUILayout.Toggle(group.IsActive);
                    EditorGUILayout.EndVertical();
                    j++;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(80));

                if (GUILayout.Button("<", GUILayout.MaxWidth(20), GUILayout.MaxHeight(50)))
                {
                    measure.EditorPreviousPieceGroup();
                    measure.Turn();
                }

                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.LabelField("Lock", _centered, GUILayout.Width(40));
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(10);
                measure.isLocked = EditorGUILayout.Toggle(measure.isLocked);   
                EditorGUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndVertical();

                if (GUILayout.Button(">", GUILayout.MaxWidth(20), GUILayout.MaxHeight(50)))
                {
                    //Measure measure = RolesController.Instance.CurrentRole.measures[i];                   
                    measure.EditorNextPieceGroup();
                    measure.Turn();
                }
                EditorGUILayout.EndHorizontal();

                DrawTestButton(i);               

                EditorGUILayout.BeginHorizontal();
                DrawCopyMeasureButton(i);
                DrawPasteMeasureButton(i);
                EditorGUILayout.EndHorizontal();

                DrawDeletePiecesButton(i);
                DrawRepositionButton(i);
                
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawTestButton(int measureIndex)
        {
            //Display the current panel
            if (GUILayout.Button("Hold Test"))
            { 
            
            }
        }

        //COPIES THE NOTES IN THE MEASURE
        private void DrawCopyMeasureButton(int measureIndex)
        {
            if (GUILayout.Button("Copy"))
            {
                _copiedPieces.Clear();
                List<LevelPiece> temp = RolesController.Instance.FindPieces(measureIndex);
                foreach (LevelPiece p in temp)
                {
                    if (p is PieceGroup)
                    {
                        PieceGroup pg = p as PieceGroup;
                        foreach (NotePiece lp in pg.notePieces)
                        {
                            _copiedPieces.Add(new NoteData(lp.x, lp.y, lp.type));
                        }

                        break;
                    }
                    _copiedPieces.Add(new NoteData(p.x, p.y, (p as NotePiece).type));
                }

                if (_copiedPieces.Count == 0) { Debug.LogWarning("There are no pieces to copy!!"); }
            }
        }

        private void DrawPasteMeasureButton(int measureIndex)
        {
            if (GUILayout.Button("Paste"))
            {
                RolesController.Instance.CurrentRole.measures[measureIndex].DeleteAllPieces();

                foreach (NoteData p in _copiedPieces)
                {
                    ConvertDataToObject(p, measureIndex);
                }
            }
        }

        private void DrawRepositionButton(int measureIndex)
        {
            if (GUILayout.Button("Reposition Notes"))
            {
                RolesController.Instance.CurrentRole.measures[measureIndex].RepositionAllPieces();
            }
        }

        private void ConvertDataToObject(NoteData data, int measureIndex)
        {
            GameObject go = Inventory_Archive.Instance.ReturnFullInventoryObject(data.type);         
            LevelPiece p = go.GetComponent<LevelPiece>();
            p.x = data.x;
            p.y = data.y;
            PlacePiece(p, measureIndex);
        }

        private void DrawDeletePiecesButton(int measureIndex)
        {
            if (GUILayout.Button("Delete Pieces"))
            {
                RolesController.Instance.CurrentRole.measures[measureIndex].DeleteAllPieces();
                Debug.Log("Deleting pieces!");
            }
        }
        private void DrawGroupPieceGUI()
        {
            PieceGroup group = _itemInspected.inspectedScript as PieceGroup;
            if (group == null) { return; }

            int index = 1;

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(90));
                foreach (LevelPiece p in group.notePieces)
                {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Piece" + index, GUILayout.MaxWidth(60));

                    EditorGUILayout.BeginHorizontal();

                    //MOVE PIECE UP
                    if (GUILayout.Button("U", GUILayout.MaxWidth(20)))
                    {
                        p.MovePieceUp();
                    }

                    //MOVE PIECE DOWN
                    if (GUILayout.Button("D", GUILayout.MaxWidth(20)))
                    {
                        p.MovePieceDown();
                    }

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();
                    index++;
                }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawRolesGUI()
        {
            EditorGUILayout.LabelField("Roles", _titleStyle);

            GUIStyle style = new GUIStyle();
            style.fontSize = 24;
            switch (RolesController.Instance.GetMode())
            {
                case (Composia.Mode.Harmony):
                    style.normal.textColor = Color.red;
                    break;
                case (Composia.Mode.Bass):
                    style.normal.textColor = Color.green;
                    break;
                case (Composia.Mode.Melody):
                    style.normal.textColor = Color.cyan;
                    break;
            }
            
            //style.

            EditorGUILayout.LabelField("Current Role: " + RolesController.Instance.GetMode(), style);
            EditorGUILayout.Space();

            for (int i = 0; i < RolesController.Instance.roles.Count; i++)
            {
                RolesController.Instance.roles[i] = (Role)EditorGUILayout.ObjectField(RolesController.Instance.roles[i], typeof(Role), true);
            }

            if (GUILayout.Button("Next Role"))
            {
                RolesController.Instance.NextRole();
                RolesController.Instance.SetAnimation();
            }    
        }

        private void DrawCopied()
        {
            EditorGUILayout.LabelField("Copied List", _titleStyle);
          
            for (int i = 0; i < _copiedPieces.Count; i++)
            {
                string labelName = string.Format("[{0},{2}][{3}][S{1}][{4}]", 
                                                    _copiedPieces[i].x, 
                                                    NoteUtils.ConvertNoteToSize(_copiedPieces[i].type), _copiedPieces[i].y,   
                                                    _copiedPieces[i].type.ToString(),      
                                                    NoteUtils.ConvertYToNote(_copiedPieces[i].y));


                EditorGUILayout.LabelField(labelName);
            }
        }

        #endregion

        #region HANDLERS

        private void EventHandler()
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Camera camera = SceneView.currentDrawingSceneView.camera;

            Vector3 mousePosition = Event.current.mousePosition;
            mousePosition = new Vector2(mousePosition.x, camera.pixelHeight - mousePosition.y);
            //Debug.LogFormat("MousePos: {0}", mousePosition);
            Vector3 worldPos = camera.ScreenToWorldPoint(mousePosition);
            //worldPos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            Vector3 gridPos = _myTarget.WorldToGridCoordinates(worldPos);
            //Debug.Log("Camera:" + camera + "Mouse Position:" + mousePosition + ".....worldPos:" + worldPos + "....GridPos:" + gridPos);
            int col = (int)gridPos.x;
            int row = (int)gridPos.y;

            switch (_currentMode)
            {
                case Mode.Validate:
                    if(Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag)
                    {
                        Validate(col, row);
                    }
                    break;
                case Mode.Paint:
                    //PAINTS THE CUBE INTO SCENE
                    //SceneView.currentDrawingSceneView.in2DMode = true;
                    if (Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag)
                    {
                        Paint(col, row);
                    }

                    //DRAWS A PREVIEW OF WHERE THE CUBE(S) WILL BE PLACED
                    if(_pieceSelected != null) 
                    { 
                        _myTarget.CanDrawCube(row, col, _pieceSelected.GetComponent<LevelPiece>().size); /*Debug.Log(col +","+row);*/ 
                    }                         
                    break;
                case Mode.Edit:
                    //On Press
                    if (Event.current.type == EventType.MouseDown)
                    {
                        Edit(col, row);
                        _originalPosX = col;
                        _originalPosY = row;
                    }

                    //On Let Go
                    if (Event.current.type == EventType.MouseUp || Event.current.type == EventType.Ignore)
                    { 
                        if(_itemInspected != null) { Move(); } 
                    }

                    if (_itemInspected != null)
                    {
                        _itemInspected.transform.position = Handles.FreeMoveHandle(
                            _itemInspected.transform.position,
                            _itemInspected.transform.rotation,
                            Level.Instance.CellSize / 2,
                            Level.Instance.CellSize / 2 * Vector3.one,
                            Handles.RectangleHandleCap );
                    }
                    break;

                case Mode.Erase:
                    if (Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag)
                    {
                        Erase(col, row);
                    }
                break;
                case Mode.View:
                    if (Event.current.type == EventType.MouseDown)
                    {
                        SceneView.currentDrawingSceneView.in2DMode = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private void ModeHandler()
        {
            switch (_selectedMode)
            {
                case Mode.Validate:
                case Mode.Paint:
                case Mode.Edit:
                case Mode.Erase:
                    Tools.current = Tool.None;
                    SceneView.currentDrawingSceneView.in2DMode = true;
                    break;
                case Mode.View:
                default:
                    Tools.current = Tool.View;
                    break;
            }

            //Detects mode change
            if (_selectedMode != _currentMode)
            {
                _myTarget.StopDrawCube();
                _currentMode = _selectedMode;
                _itemInspected = null;
                Repaint();
            }

            //Force 2D Mode
            
        }


        #endregion
        
        private void PaintTiles()
        {
            GameObject go = (GameObject)AssetDatabase.LoadAssetAtPath(tileStringPath, typeof(GameObject));
            Sprite textureMeasure = (Sprite)AssetDatabase.LoadAssetAtPath(tileStringPathMeasure, typeof(Sprite));
            //Texture2D textureBeat = (Texture2D)AssetDatabase.LoadAssetAtPath(tileStringPathBeat, typeof(Texture2D));

            Transform parent = _myTarget.gridTiles.transform;
            foreach (GridTileInfo gt in GridRuntime.Instance.gridTiles)
            {
                DestroyImmediate(gt.gameObject);
            }

            CalculateColumns();
            GridRuntime.Instance.gridTiles.Clear();
            for (int row = 0; row < _myTarget.TotalRows; row++)
            {
                for (int col = 0; col < _myTarget.TotalColumns; col++)
                {
                    GameObject obj = PrefabUtility.InstantiatePrefab(go) as GameObject;

                    obj.transform.parent = parent;
                    obj.name = string.Format("[{0},{1}]", col, row);
                    obj.transform.position = _myTarget.GridToWorldCoordinates(col, row);
                    obj.GetComponent<GridTileInfo>().SetGridPos(col, row);
                    
                    GridRuntime.Instance.gridTiles.Add(obj.GetComponent<GridTileInfo>());

                    if (col % _myTarget.MeasureLength == 0) { obj.GetComponent<SpriteRenderer>().sprite = textureMeasure; }
                }
            }

            RolesController.Instance.LinkRolesPieceGroups();
        }

        private void Move()
        {
            Vector3 gridPoint = _myTarget.WorldToGridCoordinates(_itemInspected.transform.position);
            int col = (int)gridPoint.x;
            int row = (int)gridPoint.y;

            if (col == _originalPosX && row == _originalPosY)
            {
                return;
            }

            if (!_myTarget.IsInsideGridBounds(col, row))
            {
                Debug.LogWarning("Invalid Placement... Either outside of bounds or placed on top of an already existing block");
                _itemInspected.transform.position = _myTarget.GridToWorldCoordinates(_originalPosX, _originalPosY);
            }
            else 
            {
                LevelPiece piece = _itemInspected.GetComponent<LevelPiece>();

                //_myTarget.EraseFromMeasure(col, row);
                _myTarget.CopyToMeasure(col, row, piece);
                piece.transform.position = _myTarget.GridToWorldCoordinates(col, row);


            }
        }

        private void Validate(int col, int row)
        {
           // Debug.Log(GridRuntime.Instance.gridTiles);
           // Debug.Log(GridRuntime.Instance.gridTiles[0].GetComponent<GridTileInfo>());
            GridTileInfo gt = GridRuntime.Instance.gridTiles[col + (_myTarget.TotalColumns * row)];
            if(gt == _currentGT) { return; }
            _currentGT = gt;

            if (_leftShiftIsPressed)
            {
                GridRuntime.Instance.RemoveValidation(new Vector2Int(col, row));
                gt.IsValidPlacement = false;
                //Debug.Log("Invalidating!");
            }
            else
            {
                GridRuntime.Instance.AddValidation(new Vector2Int(col, row));
                gt.IsValidPlacement = true;
            }
    
            EditorUtility.SetDirty(gt);
        }


        private void Paint(int col, int row)
        {
            // Check out of bounds and if we have a piece selected
            if (!_myTarget.IsInsideGridBounds(col, row) || _pieceSelected == null)
            {
                Debug.LogWarning("Out of bounds or no piece selected![" + col + "," + row + "]");
                return;
            }

            if (!_myTarget.IsInsideMeasure(col, _pieceSelected.size))
            {
                Debug.LogWarning("Piece does not fit within this measure");
                return;
            }

            int measureIndex = _myTarget.FindMeasureIndex(col);
            //LevelPiece[] inspectedPieces = _myTarget.FindPieces(col, row, _pieceSelected.size, measureIndex);

            //Check to see if there are any pieces in the same column
            if (_myTarget.CheckColumns(col, _pieceSelected.size, measureIndex))
            {
                //Debug.Log("Measure index is...." + measureIndex);
                Debug.LogWarning("This column is already occupied");
                return;
            }

            if (RolesController.Instance.GetMode() == Composia.Mode.Harmony)
            {
                if (row < 4)
                {
                    Debug.Log("Cannot place chord this low!");
                    return;
                }

                PlacePiece(col, row - 2, measureIndex);
                PlacePiece(col, row - 4, measureIndex);
            }

            PlacePiece(col, row, measureIndex);

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        private void PlacePiece(LevelPiece piece, int measureIndex)
        {
            int col = piece.x;
            int row = piece.y;

            GameObject obj = PrefabUtility.InstantiatePrefab(piece.gameObject) as GameObject;
            piece = obj.GetComponent<LevelPiece>();
            piece.x = col;
            piece.y = row;

            if (piece is PieceGroup)
            {
                PlacePieceGroup(col, row, measureIndex, obj);
                Debug.Log("Placing piece group");
                return;
            }

            _myTarget.AddLevelPieces(col, row, piece, measureIndex);
            RolesController.Instance.PlacePiece(piece, measureIndex);                  //Sets the parent of this piece to be in the correct panel

            if (piece is NotationPiece)                                                     //Sets the name of the piece in the inspector
            {
                obj.name = string.Format("[{0},{2}][{3}][S{1}][{4}]", col, piece.size, row, obj.name, NoteUtils.ConvertYToNote(row));
            }
            else
            {
                obj.name = string.Format("[{0},{2}][{3}][S{1}][{4}]", col, piece.size, row, obj.name);
            }

            obj.transform.position = _myTarget.GridToWorldCoordinates(col, row);
            //obj.hideFlags = HideFlags.HideInHierarchy;

        }

        private void PlacePiece(int col, int row, int measureIndex)
        {
            GameObject obj = PrefabUtility.InstantiatePrefab(_pieceSelected.gameObject) as GameObject;
            LevelPiece piece = obj.GetComponent<LevelPiece>();
            if (piece is PieceGroup)
            {
                PlacePieceGroup(col, row, measureIndex, obj);
                return;
            }

            _myTarget.AddLevelPieces(col, row, piece, measureIndex);
            RolesController.Instance.PlacePiece(piece, measureIndex);                  //Sets the parent of this piece to be in the correct panel

            if (piece is NotationPiece)                                                     //Sets the name of the piece in the inspector
            {
                obj.name = string.Format("[{0},{2}][{3}][S{1}][{4}]", col, piece.size, row, obj.name, NoteUtils.ConvertYToNote(row));
            }
            else
            {
                obj.name = string.Format("[{0},{2}][{3}][S{1}][{4}]", col, piece.size, row, obj.name);
            }

            obj.transform.position = _myTarget.GridToWorldCoordinates(col, row);
            obj.hideFlags = HideFlags.HideInHierarchy;
        }

        private void PlacePieceGroup(int col, int row, int measureIndex, GameObject obj)
        {
            PieceGroup pieceGroup = obj.GetComponent<PieceGroup>();
            if (pieceGroup == null) { Debug.LogWarning("tried placing a note piece group that doesn't exist!");  return; }
            RolesController.Instance.PlacePiece(pieceGroup, measureIndex);
            pieceGroup.transform.position = _myTarget.GridToWorldCoordinates(col, row);
            pieceGroup.x = col;
            pieceGroup.y = row;
            pieceGroup.SetNotePieces();

            for (int i = 0; i < pieceGroup.notePieces.Length; i++)
            {
                LevelPiece piece = pieceGroup.notePieces[i];
                row = piece.y;
                piece.name = string.Format("[{0},{2}][{3}][S{1}][{4}]", col + (i * piece.size), piece.size, row, obj.name, NoteUtils.ConvertYToNote(row));

                _myTarget.AddLevelPieces(col + (i * piece.size), row, piece, measureIndex);             
            }

        }


        private void Erase(int col, int row)
        {
            // Check out of bounds and if we have a piece selected
            if (!_myTarget.IsInsideGridBounds(col, row))
            {
                Debug.LogWarning("Not inside the bounds!");
                return;
            }

            LevelPiece piece = _myTarget.FindPiece(col, row);

            if (piece != null)
            {
                if (RolesController.Instance.GetMode() == Composia.Mode.Harmony)
                {
                    _myTarget.FindMeasure(col).DeleteChordImmediate(col);
                }

                piece.EraseImmediate();
            }
            else 
            {
                Debug.Log("Nothing to erase!");
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        private void Edit(int col, int row)
        {
            LevelPiece tempPiece = _myTarget.FindPiece(col, row);
            if(tempPiece == null) { Debug.Log("There is no piece here"); return; }
            if(tempPiece.myGroup != null) { Debug.Log("This piece has a groupPiece"); tempPiece = tempPiece.myGroup; }

            if (!_myTarget.IsInsideGridBounds(col, row))
            {
                _itemInspected = null;
            }
            else {
                _itemInspected = tempPiece.GetComponent<PaletteItem>() as PaletteItem;
            }

            Repaint();
        }


        private void CalculateColumns()
        {
            _myTarget.TotalColumns = _myTarget.MEASURES * _myTarget.MeasureLength;
        }

        private void InitLevel()
        {
            CalculateColumns();
            _myTarget.transform.hideFlags = HideFlags.NotEditable;
        }

        private void UpdateCurrentPieceInstance(PaletteItem item, Texture2D preview)
        {
            _itemSelected = item;
            _itemPreview = preview;
            _pieceSelected = (LevelPiece)item.GetComponent<LevelPiece>();
            Repaint();
        }

        private void SubscribeEvents()
        {
            PaletteWindow.ItemSelectedEvent += new PaletteWindow.itemSelectedDelegate(UpdateCurrentPieceInstance);
        }

        private void UnsubscribeEvents()
        {
            PaletteWindow.ItemSelectedEvent -= new PaletteWindow.itemSelectedDelegate(UpdateCurrentPieceInstance);
        }
    }
}
