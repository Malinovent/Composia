using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public abstract class NotationPiece : LevelPiece
    {
        public NoteTypeEnum type;


        public abstract void PlaySound();

        public abstract void Restart();
        public abstract void Activate(bool isPow = false);
        public abstract List<string> FindChordNotes();
              
    }
}
