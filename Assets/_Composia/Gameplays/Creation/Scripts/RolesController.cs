using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Composia
{
    public enum Mode
    { 
        Melody,
        Harmony,
        Bass,
        Percussion
    }

    public class RolesController : Singleton<RolesController>, IObservable
    {

        public List<Role> roles;

        List<IObserver> _observers = new List<IObserver>();

        [SerializeField]
        private Animator _animator;
        [SerializeField][ReadOnly]
        private Role _currentRole;
        [SerializeField][HideInInspector]
        private int _roleIndex = 0;
        [SerializeField][HideInInspector]
        private Mode _mode;

        private List<NotePiece> _harmonyNotes = new List<NotePiece>();
        private const int MEASURES = 4;
        private int _measureLength = 16;

        [SerializeField]
        private bool _isMinorScale;

        public Role CurrentRole 
        {
            get
            { 
                if(_currentRole == null) { SetCurrentInstrument(); }
                return _currentRole;
            }
            set => _currentRole = value; 
        }

        public List<NotePiece> HarmonyNotes { get => _harmonyNotes; set => _harmonyNotes = value; }
        public int MeasureLength { get => _measureLength; set => _measureLength = value; }
        public bool IsMinorScale { get => _isMinorScale; set => _isMinorScale = value; }

        private void Start()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            DetermineMode();
            FindHarmonyNotes();
            MelodyInstrument();
        }

        public int CountHarmonyChords()
        {
            int count = 0;
            int column = -1;
            foreach (NotePiece piece in HarmonyNotes)
            {
                if (piece.x != column)
                {
                    column = piece.x;
                    count++;
                }
            }

            return count;
        }

        public int CountMelodyNotes()
        {
            return roles[0].FindAllPieces().Count;    //Index 0 is the melody role
        }

        private void FindHarmonyNotes()
        {
            //Debug.Log("Getting all pieces");
            HarmonyNotes.Clear();
            foreach (Measure measure in roles[1].measures)
            {
                List<NotePiece> tempList = new List<NotePiece>();
                if (measure.CurrentPieceGroup.Pieces.Length <= 0) { Debug.LogWarning("There no are no pieces!"); return; }

                for (int i = 0; i < measure.CurrentPieceGroup.Pieces.Length; i++)
                {
                    LevelPiece piece = measure.CurrentPieceGroup.Pieces[i];
                    if (piece != null)
                    {
                        tempList.Add((NotePiece)piece);
                    }
                }

                HarmonyNotes.AddRange(tempList);
                HarmonyNotes.RemoveAll(item => item == null);
            }
        }

        public void DetermineMode()
        {
            switch (_roleIndex)
            {
                case 0:
                    _mode = Mode.Melody;
                    break;
                case 1:
                    _mode = Mode.Harmony;
                    break;
                case 2:
                    _mode = Mode.Bass;
                    break;
            }
        }

        public Mode GetMode()
        {
            DetermineMode();
            return _mode;
        }

        public void MelodyInstrument()
        {
            _roleIndex = 0;
            _animator.SetTrigger("MelodyTrigger");
            //Debug.Log("Melody Trigger");
            SetCurrentInstrument();
        }

        public void NextRole()
        {
            if(roles.Count <= 1) { return; } //Backs out if there is nothing to rotate

            _roleIndex++;
            if(_roleIndex >= roles.Count) { _roleIndex = 0; }
            _animator.SetTrigger("NextInstrumentTrigger");
            SetCurrentInstrument();

        }

        public void PreviousRole()
        {
            if (roles.Count <= 1) { return; } //Backs out if there is nothing to rotate

            _roleIndex--;
            if (_roleIndex < 0) { _roleIndex = roles.Count - 1; }
            _animator.SetTrigger("PreviousInstrumentTrigger");
            SetCurrentInstrument();
        }

        public void SetAnimation()
        {
            //string anim = "Switch_Instrument  " + (_instrumentIndex + 1);
            string anim = GetMode().ToString();
            Debug.Log(anim);
            _animator.Play(anim, 0, 1);
            _animator.Update(Time.deltaTime);
        }

        private void SetCurrentInstrument()
        {
            CurrentRole = roles[_roleIndex];
            DetermineMode();
            NotifyObservers();
        }

        public void ReplacePiece(LevelPiece piece)
        {
            Measure measure = FindMeasure(piece.x);
            EraseFromMeasure(piece, measure);
            //measure.CurrentPieceGroup.Pieces
            CopyToMeasure(piece.x, piece.y, piece, measure);
            //PlacePiece(piece, measure);
            
        }

        public void PlacePiece(LevelPiece piece, int measureIndex)
        {
            piece.transform.parent = CurrentRole.measures[measureIndex].CurrentPieceGroup.transform;     
        }

        private void PlacePiece(LevelPiece piece, Measure measure)
        {
            piece.transform.parent = measure.CurrentPieceGroup.transform;
        }

        public void RotateMeasureClockwise(int measureIndex)
        {
            CurrentRole.measures[measureIndex].NextPieceGroup();
        }

        public void RotateMeasureCounterClockwise(int measureIndex)
        {
            CurrentRole.measures[measureIndex].PreviousPieceGroup();
        }


        public int FindMeasureIndex(int col)
        {
            for (int i = 0; i < MEASURES; i++)
            {
                if (col < CurrentRole.measures[0].GetArraySize() * (i + 1))
                {
                    //Debug.Log("Found Measure: " + i);
                    return i;
                }
            }

            Debug.LogWarning("Did not find proper measure!! Returned first one as default");
            return 0;
        }

        public Measure FindMeasure(int col, int measureIndex = 99)
        {
            if (measureIndex != 99)
            {
                return CurrentRole.measures[measureIndex];
            }
            else
            {
                return CurrentRole.measures[FindMeasureIndex(col)];
            }
        }


        public void EraseFromMeasure(int col, int row, Measure measure)
        {
            int measureColIndex = col % MeasureLength;

            int index = measureColIndex + row * MeasureLength;
            //Debug.Log("Erasing object at index: " + index);
            Debug.Log(index);
            measure.RemovePiece(index);
        }

        public void EraseFromMeasure(LevelPiece piece, Measure measure)
        {
            for (int i = 0; i < measure.CurrentPieceGroup.Pieces.Length; i++)
            {
                if (measure.CurrentPieceGroup.Pieces[i] == piece)
                {
                    measure.CurrentPieceGroup.Pieces[i] = null;
                }
            }
        }

        //Copies the data on the level to the measure
        public void CopyToMeasure(int col, int row, LevelPiece piece, Measure measure)
        {
            int measureColIndex = col % MeasureLength;

            int index = measureColIndex + row * MeasureLength;
            //Debug.Log("Plaching object at index: " + index);
            //Debug.Log(index);
            measure.SetPiece(index, piece);
        }

        public void LinkRolesPieceGroups()
        {
            CurrentRole.LinkAllMeasuresPieceGroups();
        }

        public List<LevelPiece> FindPieces(int measureIndex)
        {
            List<LevelPiece> temp = new List<LevelPiece>();
            temp.AddRange(CurrentRole.measures[measureIndex].GetPieces());
            temp.RemoveAll(x => x == null);
            temp = temp.Distinct().ToList();

            List<LevelPiece> finalList = new List<LevelPiece>();
            foreach (LevelPiece p in temp)
            {
                //finalList.Add(new LevelPiece(p.x, p.y, p.size));
            }
            
            return temp;
        }


        #region OBSERVER PATTERN
        public void AddObserver(IObserver o)
        {
            if (!_observers.Contains(o))
            {
                _observers.Add(o);
            }
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            if(!Application.isPlaying)
                GridRuntime.Instance.UpdateValidation();
    

            foreach (IObserver o in _observers)
            {
                o.UpdateData(this);
            }       
        }
        #endregion
    }
}