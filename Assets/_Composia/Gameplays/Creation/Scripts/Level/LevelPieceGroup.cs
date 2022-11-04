using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Composia
{
    public class LevelPieceGroup : MonoBehaviour
    {      
        [SerializeField]
        private LevelPiece[] _pieces;
        private int _arraySize;

        [SerializeField]
        private bool _isActive = true;

        public LevelPiece[] Pieces { get => _pieces; set => _pieces = value; }
        public int ArraySize 
        {
            get
            {
                if(_arraySize == 0) { SetLevelPieceArraySize(); }
                return _arraySize;
            }
            set => _arraySize = value; }

        public bool IsActive 
        { 
            get => _isActive;
            set
            {
                _isActive = value;
                gameObject.SetActive(_isActive);
            }
        }

        private void Awake()
        {
            IsActive = gameObject.activeSelf;
        }

        public LevelPiece GetPiece(int index)
        {
            SetLevelPieceArraySize();

            if (index < 0 || index >= Pieces.Length) { return null; }

            return Pieces[index];
        }

        public void LinkChildrenNotePieces()
        {
            List<NotePiece> notes = new List<NotePiece>();
            notes.AddRange(GetComponentsInChildren<NotePiece>());
            SetLevelPieceArraySize();

            for (int i = 0; i < Pieces.Length; i++)
            {              
                Pieces[i] = null;
            }

            foreach (NotePiece note in notes)
            {
                note.FindCoordinates();
                SetPiece(note.ReturnPieceIndex(), note);          
            }
        }

        public void SetLocation()
        {
            foreach (NotePiece note in GetSingleNotePieces())
            {
                note.transform.position = Level.Instance.GridToWorldCoordinates(note.x, note.y);
            }
        }

        public void RepositionPieces()
        {
            foreach (NotePiece note in GetSingleNotePieces())
            {
                if (note != null)
                {
                    note.PositionPiece();
                }
            }
        }

        public void SetPiece(int index, LevelPiece piece)
        {
            if(index > 191) { Debug.LogWarning("Index is too high!: " + index); }
            if (Pieces == null)
            {
                SetLevelPieceArraySize();
            }
            
            if (Pieces.Length != ArraySize)
            {
                SetLevelPieceArraySize();
            }

            //Debug.Log("Index is: " + index + "...ArraySize: " + arraySize);
            for (int i = 0; i < piece.size; i++)
            {
                if((index + i) < 0) { }
                Pieces[index + i] = piece;
            }

        }

        public void RemovePiece(int index)
        {
            //Debug.Log(index);
            int size = GetPiece(index).size;
            for (int i = 0; i < size; i++)
            {
                Pieces[index + i] = null;
            }
        }

        public void SetLevelPieceArraySize()
        {
            ArraySize = Level.Instance.TotalRows * Level.Instance.MeasureLength; //Calculates the number of cells in the box
            //if(Pieces == null) { Pieces = new LevelPiece[ArraySize]; }
            if (Pieces.Length != ArraySize)
            {
                Pieces = new LevelPiece[ArraySize];
            }
        }

        public void DeleteAllPieces()
        {
            foreach (LevelPiece piece in Pieces)
            {
                if (piece != null)
                {
                    if (piece.myGroup != null)
                    {
                        DestroyImmediate(piece.myGroup);
                    }
                    else
                    {
                        DestroyImmediate(piece.gameObject);
                    }
                }
            }
        }

        public LevelPiece[] GetPieces()
        {
            return Pieces;
        }

        public List<LevelPiece> GetOnlyPieces()
        {
            List<LevelPiece> temp = new List<LevelPiece>();
            temp.AddRange(Pieces);

            temp.RemoveAll(x => x == null);
            return temp;

        }

        public List<NotePiece> GetSingleNotePieces()
        {
            List<LevelPiece> tempPieces = new List<LevelPiece>();
            tempPieces.AddRange(Pieces);
            tempPieces.RemoveAll(x => x == null);

            List<NotePiece> notePieces = new List<NotePiece>();

            foreach (LevelPiece piece in tempPieces)
            {
                if (piece is NotePiece)
                { 
                    if(!notePieces.Contains(piece as NotePiece))
                    {
                        notePieces.Add(piece as NotePiece);
                    }
                }
            }

            return notePieces;
        }
    }
}
