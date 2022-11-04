using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class Role : MonoBehaviour
    {
        //public string instrumentName;
        public Measure[] measures;

        private Measure _currentMeasure;
        private int _measureIndex = 0;


        public Measure CurrentMeasure 
        { 
            get => _currentMeasure; 
            set => _currentMeasure = value; 
        }

       

        private void FindMeasure()
        { 
            CurrentMeasure = measures[_measureIndex];
        }

        public List<NotePiece> FindAllPieces()
        {
            List<NotePiece> pieces = new List<NotePiece>();

            foreach (Measure measure in measures)
            {
                pieces.AddRange(measure.CurrentPieceGroup.GetSingleNotePieces());
            }

            return pieces;
        }

        public void LinkAllMeasuresPieceGroups()
        {
            foreach (Measure measure in measures)
            {
                measure.LinkPieceGroup();
            }
        }

        /*
        public void SwapMeasure(Measure otherMeasure, int measureIndex)
        {
            measures[measureIndex] = otherMeasure;
        }*/


    }
}
