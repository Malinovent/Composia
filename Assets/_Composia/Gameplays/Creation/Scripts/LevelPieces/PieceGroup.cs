using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class PieceGroup : LevelPiece
    {
        public NotePiece[] notePieces;


        public void SetNotePieces()
        {
            foreach (NotePiece note in notePieces)
            {
                note.FindCoordinates();
            }
        }
    }
}
