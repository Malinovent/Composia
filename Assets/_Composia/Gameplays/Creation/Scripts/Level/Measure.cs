using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.EditorCoroutines.Editor;

namespace Composia
{
    public class Measure : MonoBehaviour
    {
        private bool _isActive;

        //[SerializeField]
        // private LevelPiece[] _pieces;

        [SerializeField]
        private LevelPieceGroup[] _pieceGroups;
        private LevelPieceGroup _currentPieceGroup;

        [SerializeField]
        private int _pieceGroupIndex = 0;

        [SerializeField]
        private Animator _animator;
        private string _currentAnimationStateName;

        public bool isLocked = false;

        //private int arraySize;

        #region GETTERS
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public LevelPieceGroup CurrentPieceGroup
        {
            get
            {
                if (_currentPieceGroup == null) { SetCurrentPieceGroup(); }
                return _currentPieceGroup;
            }


            set => _currentPieceGroup = value;
        }

        public LevelPieceGroup[] PieceGroups { get => _pieceGroups; set => _pieceGroups = value; }
        public int PieceGroupIndex { get => _pieceGroupIndex; set => _pieceGroupIndex = value; }

        private void Start()
        {
            if (_animator == null) { _animator = GetComponent<Animator>(); }
            SetCurrentPieceGroup();

            _currentAnimationStateName = GetCurrentAnimationStateName();
            _animator.Play(_currentAnimationStateName);
        }

        [ExecuteInEditMode]
        private void Update()
        {
            //_animator.Play(anim, 0, 1);
            _animator.Update(Time.deltaTime);
        }

        #endregion

        private void SetAnimationTrigger()
        {
            SetCurrentPieceGroup();
            _animator.SetTrigger("Turn " + (PieceGroupIndex + 1));
        }

        private void FindNextPieceGroupIndex()
        {
            int j = PieceGroupIndex;
            for (int i = 0; i < PieceGroups.Length; i++)
            {
                j++;
                if (j >= PieceGroups.Length)
                {
                    j = 0;
                }

                if (PieceGroups[j].IsActive)
                {
                    PieceGroupIndex = j;
                    Debug.Log("New index is " + j);
                    break;
                }
            }
        }
        private void FindPreviousPieceGroupIndex()
        {
            int j = PieceGroupIndex;
            for (int i = 0; i < PieceGroups.Length; i++)
            {
                j--;
                if (j <= 0)
                {
                    j = PieceGroups.Length - 1;
                }

                if (PieceGroups[j].IsActive)
                {
                    PieceGroupIndex = j;
                    Debug.Log("New index is " + j);
                    break;
                }
            }
        }
        public void NextPieceGroup()
        {
            if (PieceGroups.Length <= 1 || isLocked) { return; }    //Exits the method if there are no other groups or is locked

            PieceGroupIndex++;
            if (PieceGroupIndex >= PieceGroups.Length) { PieceGroupIndex = 0; }

            if (!PieceGroups[PieceGroupIndex].IsActive)
            {
                FindNextPieceGroupIndex();

                SetAnimationTrigger();
            }
            else
            {

                SetAnimationTrigger();
            }
        }
        public void PreviousPieceGroup()
        {
            if (PieceGroups.Length <= 1 || isLocked) { return; }    //Exits the method if there are no other groups or is locked

            PieceGroupIndex--;
            if (PieceGroupIndex < 0) { PieceGroupIndex = PieceGroups.Length - 1; }

            if (!PieceGroups[PieceGroupIndex].IsActive)
            {
                FindPreviousPieceGroupIndex();

                SetAnimationTrigger();
            }
            else
            {
                SetAnimationTrigger();
            }
            //Debug.Log("CounterClockwise Turn Animation...");
        }


        #region EDITOR METHODS
#if UNITY_EDITOR

        public void EditorNextPieceGroup()
        {
            if (PieceGroups.Length <= 1) { return; }    //Exits the method if there are no other groups or is locked

            PieceGroupIndex++;
            if (PieceGroupIndex >= PieceGroups.Length) { PieceGroupIndex = 0; }
            SetAnimationTrigger();

            //Debug.Log("Clockwise Turn Animation...");
        }

        public void EditorPreviousPieceGroup()
        {
            if (PieceGroups.Length <= 1) { return; }    //Exits the method if there are no other groups or is locked

            PieceGroupIndex--;
            if (PieceGroupIndex < 0) { PieceGroupIndex = PieceGroups.Length - 1; }
            SetAnimationTrigger();

            //Debug.Log("CounterClockwise Turn Animation...");
        }

        public void Turn()
        {
            _currentAnimationStateName = GetCurrentAnimationStateName();
            _animator.Play(_currentAnimationStateName, 0, 1);
            _animator.Update(Time.deltaTime);
            //Debug.Log("Switching to animation " + _currentAnimationStateName);
        }
#endif
        #endregion


        private string GetCurrentAnimationStateName()
        {
            return "Roll_Quarter Turn " + (PieceGroupIndex + 1);
        }


        public bool CheckColumn(int col, int size)
        {
            if (_currentPieceGroup == null) { SetCurrentPieceGroup(); }
            if (_currentPieceGroup.GetOnlyPieces().Count == 0) { return false; }

            foreach (LevelPiece piece in _currentPieceGroup.GetOnlyPieces())
            {
                for (int i = 0; i < piece.size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (piece.x + i == col + j)
                        {
                            //Debug.Log("There is a piece at " + piece.x + "+" + i);
                            return true;
                        }
                    }
                }
            }

            return false;   //Return is no piece
        }

        public void SetCurrentPieceGroup()
        {
            CurrentPieceGroup = PieceGroups[PieceGroupIndex];
        }

        public LevelPiece GetPiece(int index)
        {
            return CurrentPieceGroup.GetPiece(index);
        }

        public void SetPiece(int index, LevelPiece piece)
        {
            CurrentPieceGroup.SetPiece(index, piece);
        }

        public void RemovePiece(int index)
        {
            CurrentPieceGroup.RemovePiece(index);
        }

        public void SetLevelPieceArraySize()
        {
            CurrentPieceGroup.SetLevelPieceArraySize();
        }

        public int GetArraySize()
        {
            return CurrentPieceGroup.ArraySize;
        }

        public void DeleteAllPieces()
        {
            CurrentPieceGroup.DeleteAllPieces();
        }

        public LevelPiece[] GetPieces()
        {
            return CurrentPieceGroup.GetPieces();
        }

        public void DeleteChord(int col)
        {
            foreach (LevelPiece p in _currentPieceGroup.Pieces)
            {
                if (p.x == col) { Destroy(p.gameObject); }
            }
        }

        public void LinkPieceGroup()
        {
            CurrentPieceGroup.LinkChildrenNotePieces();

            //StartCoroutine(EditorLinkPieces());
            for (int i = 0; i < PieceGroups.Length; i++)
            {
                PieceGroups[_pieceGroupIndex].LinkChildrenNotePieces();
#if UNITY_EDITOR
                EditorNextPieceGroup();
                Turn();
#endif
            }
        }

        public void RepositionAllPieces()
        {
            CurrentPieceGroup.RepositionPieces();
        }

#if UNITY_EDITOR
        IEnumerator EditorLinkPieces()
        {
            
            for (int i = 0; i < PieceGroups.Length; i++)
            {
                Debug.Log("Doing coroutine " + i);
                PieceGroups[_pieceGroupIndex].LinkChildrenNotePieces();
                EditorNextPieceGroup();
                yield return new WaitForSeconds(0.5f);
            }
        }
        public void DeleteChordImmediate(int col)
        {
            foreach (LevelPiece p in _currentPieceGroup.Pieces)
            {
                if (p != null)
                {
                    if (p.x == col) { p.EraseImmediate(); }
                }
            }
        }
#endif
    }
}
