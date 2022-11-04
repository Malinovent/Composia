using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Composia
{
	public partial class Level : Singleton<Level>, IObservable {

		public enum Mode
		{
			Platforming,
			Creating
		}

		public List<IObserver> observers = new List<IObserver>();

		public Mode mode = Mode.Creating;
		public GameObject platformingParent;
		public GameObject creationParent;
		public string saveName;


		[HideInInspector] public int MEASURES = 4;
		[SerializeField] private int _totalColumns;
		[SerializeField] private const int _totalRows = 12;
		[SerializeField] private Color _fullMeasureLineColor = Color.blue;
		[SerializeField] private Color _fullNoteLineColor = Color.green;
		[SerializeField] private Color _halfNoteLineColor = Color.red;
		[SerializeField] private Color _quarterNoteLineColor = Color.gray;

		private float _cellSize = 1f;

		private readonly Color _normalColor = Color.grey;
		private readonly Color _selectedColor = Color.yellow;

		//[SerializeField]
		//public List<Instrument> instruments = new List<Instrument>();
		//[SerializeField]
		//private Instrument _currentInstrument;
		//[SerializeField]
		//private int _instrumentIndex = 0;

		public List<NotePiece> _notationPieces = new List<NotePiece>();
		public List<NotePiece> _notationPiecesPlayer = new List<NotePiece>();

		private bool _canDrawPreviewCube = false;
		private Vector3 _previewCubePosition;
		private int _previewCubeSize;

		private int _measureLength;
		public GameObject gridTiles;

		[ReadOnly]
		public List<LevelPiece> _copiedPieces = new List<LevelPiece>();

		#region GETTERS

		public int TotalColumns
		{
			get { return _totalColumns; }
			set { _totalColumns = value; }
		}

		public int TotalRows
		{
			get { return _totalRows; }
		}

		public int MeasureLength
		{
			get
			{
				_measureLength = TimeManager.Instance.TimeSignature * 4;
				return _measureLength;
			}
			set => _measureLength = value;
		}

		public List<NotePiece> NotePieces { get => _notationPieces; set => _notationPieces = value; }
		public List<NotePiece> NotePiecesPlayer { get => _notationPiecesPlayer; set => _notationPiecesPlayer = value; }
        public float CellSize { get => _cellSize; set => _cellSize = value; }

        #endregion

        #region OVERRIDES
        private void Awake()
		{
			DetermineNotationsPiecesInOrder();
		}

		private void Update()
		{
			/*if (Keyboard.current.escapeKey.wasPressedThisFrame && mode == Mode.Platforming)
			{
				SwitchModes();
				
			}*/
		}


		#endregion

		public void SwitchModes()
		{
			//Debug.Log("Switching Mode");
			if (mode == Mode.Platforming)		//SWITCH TO CREATING
			{
				creationParent.SetActive(true);
				platformingParent.SetActive(false);
				CompositionInputSystem.Instance.SwitchToCreationInputs();
				mode = Mode.Creating;
			}
			else if (mode == Mode.Creating)		//SWITCH TO PLATFORMING
			{
				RolesController.Instance.MelodyInstrument();
				//Find chords for each notes
				creationParent.SetActive(false);
				platformingParent.SetActive(true);
				mode = Mode.Platforming;
				DetermineNotationsPiecesInOrder();
				CompositionInputSystem.Instance.SwitchToPlatformingInputs();
				PlayerController_PlatformerRigidbody.Instance.Stop();
			}

			NotifyObservers();
		}

		//ONLY SAVE PLAYER OBJECTS
		public void SaveLocal()
		{

		}

		public void Save()
		{
			DetermineNotationsPiecesInOrder();
			SaveLoad.SaveSegment(TimeManager.Instance.TimeSignature, TimeManager.Instance.BPM, NotePieces, "Melody_" + saveName);
			SaveLoad.SaveSegment(TimeManager.Instance.TimeSignature, TimeManager.Instance.BPM, RolesController.Instance.HarmonyNotes, "Harmony_" + saveName);
			//SaveLoad.SaveSegment(TimeManager.Instance.TimeSignature, TimeManager.Instance.BPM, RolesController.Instance.HarmonyNotes, "Bass_" + saveName);
		}

		//LOAD ALL OBJECTS
		public void Load()
		{
			SegmentData segmentData = SaveLoad.LoadSegment("SegmentSave1");
			TimeManager.Instance.TimeSignature = segmentData.timeSignature;
			TimeManager.Instance.BPM = segmentData.originalBPM;			
			DeleteAllNotationPieces();

			foreach (NoteData data in segmentData.notesInOrder)
			{
				GameObject go = Inventory_Archive.Instance.ReturnInventoryObject(data.type);
				AddLevelPieces(data.x, data.y, go.GetComponent<LevelPiece>());
				CreateLevelPiece(data.x, data.y, go);
			}
		}

		//LOAD ONLY THE PLAYER OBJECTS FOR THE CONSTRAINTS AREAS
		public void LoadLocal()
		{

		}

		public void CreateLevelPiece(int x, int y, GameObject go)
		{
			go = Instantiate(go);
			LevelPiece piece = go.GetComponent<LevelPiece>();
			int measureIndex = FindMeasureIndex(x);
			//go.transform.parent = CurrentInstrument.measures[measureIndex].transform;
			go.transform.parent = RolesController.Instance.CurrentRole.CurrentMeasure.transform; //REFACTOR TO FEED IN OBJECT
			go.name = string.Format("[{0},{2}][{3}][S{1}]", x, piece.size, y, piece.name);
			go.transform.position = GridToWorldCoordinates(x, y);

			AddLevelPieces(x, y, piece, measureIndex);
        }

        private void DetermineNotationsPiecesInOrder()
        {
            //Debug.Log("Getting all pieces");
            NotePieces.Clear();
            List<NotePiece> newTemp = new List<NotePiece>();
            foreach (Measure measure in RolesController.Instance.CurrentRole.measures)
            {
                NotePiece[] tempList = new NotePiece[TotalColumns];

                if (measure.CurrentPieceGroup.Pieces.Length <= 0) { Debug.LogWarning("There no are no pieces!"); return; }

                for (int i = 0; i < measure.CurrentPieceGroup.Pieces.Length; i++)
                {
                    LevelPiece piece = measure.CurrentPieceGroup.Pieces[i];
                    if (piece != null)
                    {
                        //Debug.Log(piece);
                        tempList[piece.x] = (NotePiece)piece;
                    }
                }

                NotePieces.AddRange(tempList);
                NotePieces.RemoveAll(item => item == null);
            }

			//Debug.Log("Attempting to sort");
			NotePieces.Sort(SortByX);
        }

		int SortByX(NotePiece p1, NotePiece p2)
		{
			//Debug.Log(p1.x + "_" + p2.x);
			return p1.x.CompareTo(p2.x);
		}
	

        public void AddLocalPiece()
		{ 
		
		}

		public void AddLevelPieces(int col, int row, LevelPiece piece, int measureIndex = 99)
        {
			piece.x = col;
            piece.y = row;

            for (int i = 0; i < piece.size; i++)
			{
				CopyToMeasure(col + i, row, piece, measureIndex);  
            }
        }

        public int FindMeasureIndex(int col)
		{
			for (int i = 0; i < MEASURES; i++)
			{ 
				if(col < MeasureLength * (i + 1)) 
				{
					//Debug.Log("Found Measure: " + i);
					return i; 
				}
			}

			//Debug.LogWarning("Did not find proper measure!! Returned first one as default");
			return 0;
        }

		public Measure FindMeasure(int col, int measureIndex = 99)
		{
			if (measureIndex != 99) 
			{ 
				return RolesController.Instance.CurrentRole.measures[measureIndex]; 
			}
			else
			{
				return RolesController.Instance.CurrentRole.measures[FindMeasureIndex(col)];
			}
		}

		public LevelPiece FindPiece(int col, int row, int measureIndex = 99)
		{
			Measure measure = FindMeasure(col, measureIndex);			

			int measureColIndex = col % MeasureLength;

			return measure.CurrentPieceGroup.GetPiece(measureColIndex + row * MeasureLength);
        }

		//Looks for a p
		public LevelPiece[] FindPieces(int col, int row, int size, int measureIndex = 99)
        {
			LevelPiece[] pieces = new LevelPiece[size];
			Measure measure = FindMeasure(col, measureIndex);

            int measureColIndex = col % MeasureLength;

			for (int i = 0; i < size; i++)
			{
				pieces[i] = measure.GetPiece(measureColIndex + i + row * MeasureLength);
			}

			return pieces;
        }

		public LevelPiece[] FindPieces(LevelPiece p, int measureIndex = 99)
		{
			LevelPiece[] pieces = new LevelPiece[p.size];
			Measure measure = FindMeasure(p.x, measureIndex);

			int measureColIndex = p.x % MeasureLength;

			for (int i = 0; i < p.size; i++)
			{
				pieces[i] = measure.GetPiece(measureColIndex + i + p.y * MeasureLength);
			}

			return pieces;
		}

        //Copies the data on the level to the measure
        public void CopyToMeasure(int col, int row, LevelPiece piece, int measureIndex = 99)
        {

			Measure measure = FindMeasure(col, measureIndex); 
            int measureColIndex = col % MeasureLength;

			int index = measureColIndex + row * MeasureLength;
			//Debug.Log("Plaching object at index: " + index);
            measure.SetPiece(index, piece);
        }

		public void EraseFromMeasure(int col, int row, int measureIndex = 99)
		{
			Measure measure = FindMeasure(col, measureIndex);
			int measureColIndex = col % MeasureLength;

			int index = measureColIndex + row * MeasureLength;
			//Debug.Log("Erasing object at index: " + index);

			measure.RemovePiece(index);
		}
		/*
		public void SwapMeasure(int measureIndex)
        {
            Debug.Log(string.Format("Swapping measures {0}", measureIndex + 1));
			Measure tempMeasure = instruments[0].measures[measureIndex];

			for (int i = 0; i < instruments.Count - 1; i++)
			{
				instruments[i].measures[measureIndex] = instruments[i + 1].measures[measureIndex];
				instruments[i].measures[measureIndex].transform.parent = instruments[i].transform;
			}

			instruments[instruments.Count - 1].measures[measureIndex] = tempMeasure;
			instruments[instruments.Count - 1].measures[measureIndex].transform.parent = instruments[instruments.Count - 1].transform;
		}*/


        public void RemoveAllLevelPieces()
		{
			foreach (Measure measure in RolesController.Instance.CurrentRole.measures)
			{
				measure.DeleteAllPieces();
			}
		}

		public void DeleteAllNotationPieces()
		{
			DetermineNotationsPiecesInOrder();

			foreach (LevelPiece p in NotePieces)
			{
				if(p != null) { p.Erase(); }			
			}
		}

		public void RestartNotationPieces()
		{
			foreach (NotationPiece p in _notationPieces)
			{
				p.Restart();
			}
		}

		#region GIZMOS

		private void GridFrameGizmo(int cols, int rows)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, rows * CellSize, 0));
			Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(cols * CellSize, 0, 0));
			Gizmos.DrawLine(new Vector3(cols * CellSize, 0, 0), new Vector3(cols * CellSize, rows * CellSize, 0));
			Gizmos.DrawLine(new Vector3(0, rows * CellSize, 0), new Vector3(cols * CellSize, rows * CellSize, 0));
		}

		private void GridGizmo(int cols, int rows)
		{
			
			//Draws each row
			Gizmos.color = _quarterNoteLineColor;
			for (int j = 1; j < rows; j++)
			{						
				Gizmos.DrawLine(new Vector3(0, j * CellSize, 0), new Vector3(cols * CellSize, j * CellSize, 0));
			} 

			//Draws each columns
			for (int i = 1; i < cols; i++)
			{
				Gizmos.color = _quarterNoteLineColor;
				
				if (i > 0 && i < cols)
				{
					if (i % MeasureLength == 0) { Gizmos.color = _fullMeasureLineColor; }
					else if (i % 4 == 0) { Gizmos.color = _fullNoteLineColor; }
					else if (i % 2 == 0) { Gizmos.color = _halfNoteLineColor; }
					else { Gizmos.color = _quarterNoteLineColor; }
					Gizmos.DrawLine(new Vector3(i * CellSize, 0, 0), new Vector3(i * CellSize, rows * CellSize, 0));
				}			
			}			
		}

		//Draws the grid in the scene view
		private void OnDrawGizmos()
        {/*
			Color oldColor = Gizmos.color;
			Matrix4x4 oldMatrix = Gizmos.matrix;
			Gizmos.matrix = transform.localToWorldMatrix;

			Gizmos.color = _normalColor;
			GridGizmo(_totalColumns, _totalRows);
			GridFrameGizmo(_totalColumns, _totalRows);

			Gizmos.color = oldColor;
			Gizmos.matrix = oldMatrix;
			*/
			if (_canDrawPreviewCube)
			{
				if (RolesController.Instance.GetMode() == Composia.Mode.Harmony)
				{ 
					DrawPreviewCubes(); 
				}
				else
				{
					DrawPreviewCube();
				}
#if UNITY_EDITOR
				UnityEditor.HandleUtility.Repaint();
#endif
			}
        }

		//Draws the grid in the scen view
        private void OnDrawGizmosSelected()
        {
			Color oldColor = Gizmos.color;
			Matrix4x4 oldMatrix = Gizmos.matrix;
			Gizmos.matrix = transform.localToWorldMatrix;

			Gizmos.color = _selectedColor;
			GridFrameGizmo(_totalColumns, _totalRows);

			Gizmos.color = oldColor;
			Gizmos.matrix = oldMatrix;
		}

		private void DrawPreviewCube()
		{
			Color oldColor = Gizmos.color;

			Gizmos.color = IsInsideMeasure((int)_previewCubePosition.x, _previewCubeSize) ? Color.green : Color.red;
			if (Gizmos.color == Color.green)
			{
				Gizmos.color = CheckColumns((int)_previewCubePosition.x, _previewCubeSize, FindMeasureIndex((int)_previewCubePosition.x)) ? Color.red : Color.green;
			}
			_previewCubePosition = GridToWorldCoordinates((int)_previewCubePosition.x, (int)_previewCubePosition.y);

			if (!IsInsideGridBounds(_previewCubePosition)) { return; }

			_previewCubePosition.x += (CellSize * _previewCubeSize) / 2 - (CellSize / 2);
            Gizmos.DrawCube(_previewCubePosition, new Vector3(CellSize * _previewCubeSize, CellSize, 0));
            Gizmos.color = oldColor;
        }

		private void DrawPreviewCubes()
		{
			//IF THE CUBES POSITION IS LOWER THAN 5, THEN TURN RED

			Color oldColor = Gizmos.color;

			Gizmos.color = IsInsideMeasure((int)_previewCubePosition.x, _previewCubeSize) ? Color.green : Color.red;
			//Debug.Log(Gizmos.color);
			_previewCubePosition = GridToWorldCoordinates((int)_previewCubePosition.x, (int)_previewCubePosition.y);

			if (!IsInsideGridBounds(_previewCubePosition)) { return; }
			if (Gizmos.color == Color.green){ Gizmos.color = CheckColumns((int)_previewCubePosition.x, _previewCubeSize, FindMeasureIndex((int)_previewCubePosition.x)) ? Color.red : Color.green; }
			if (Gizmos.color == Color.green) {Gizmos.color = (int)_previewCubePosition.y >= 4 ? Color.green : Color.red;} //If in harmony

			_previewCubePosition.x += (CellSize * _previewCubeSize) / 2 - (CellSize / 2);
			Gizmos.DrawCube(_previewCubePosition, new Vector3(CellSize * _previewCubeSize, CellSize, 0));
			Gizmos.DrawCube(new Vector3(_previewCubePosition.x, _previewCubePosition.y - (CellSize * 2), _previewCubePosition.z), new Vector3(CellSize * _previewCubeSize, CellSize, 0));
			Gizmos.DrawCube(new Vector3(_previewCubePosition.x, _previewCubePosition.y - (CellSize * 4), _previewCubePosition.z), new Vector3(CellSize * _previewCubeSize, CellSize, 0));
			Gizmos.color = oldColor;
		}

		

        public void CanDrawCube(int row, int col, int size)
        {
			_previewCubePosition = new Vector3(col, row, 0);
            _previewCubeSize = size;
            _canDrawPreviewCube = true;
        }

        public void StopDrawCube()
		{
			_canDrawPreviewCube = false;
		}

#endregion


		public Vector3 WorldToGridCoordinates(Vector3 point)
		{
			Vector3 gridPoint = new Vector3((int)((point.x - transform.position.x) / CellSize),(int)((point.y - transform.position.y) / CellSize), 0);
			return gridPoint;
		}

		public Vector3 GridToWorldCoordinates(int col, int row)
		{
			Vector3 worldPoint = new Vector3(transform.position.x + (col * CellSize + CellSize / 2.0f), transform.position.y + (row * CellSize + CellSize / 2.0f), 0.0f);
			return worldPoint;
		}

		public float GridToWorldCoordinates(int row)
		{
			float worldPoint = transform.position.y + (row * CellSize + CellSize / 2.0f);
			return worldPoint;
		}

		public bool IsInsideGridBounds(Vector3 point)
		{
			float minX = transform.position.x;
			float maxX = minX + _totalColumns * CellSize;
			float minY = transform.position.y;
			float maxY = minY + _totalRows * CellSize;
			return (point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY);
		}
		
		public bool IsInsideGridBounds(int col, int row)
		{
			return (col >= 0 && col < _totalColumns && row >= 0 && row < _totalRows);
		}

		//Determines if block is small enough to stay within a measure
		public bool IsInsideMeasure(int column, int size)
		{
			int measureLength = TimeManager.Instance.TimeSignature * 4;
			int distanceAwayFromNextMeasure = measureLength - (column % measureLength);

			//Debug.Log("Distance away from measure " + (size - distanceAwayFromNextMeasure));

			return (size - distanceAwayFromNextMeasure) <= 0;
		}

		public bool CheckColumns(int col, int size, int measureIndex)
		{
			return RolesController.Instance.CurrentRole.measures[measureIndex].CheckColumn(col, size);
		}


		#region OBSERVER IMPLEMENTATION
		public void AddObserver(IObserver o)
        {
			observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
			observers.Remove(o);
        }

        public void NotifyObservers()
        {
			foreach (IObserver o in observers)
			{
				o.UpdateData(this);
			}
        }
#endregion
    }
}