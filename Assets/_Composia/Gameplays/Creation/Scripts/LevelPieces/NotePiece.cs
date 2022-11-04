using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class NotePiece : NotationPiece
    {
        public Sprite mySprite;
        //public bool isChord = false;
        private string noteString;

        protected bool isActivated = false;
        private List<string> notes = new List<string>();
        private List<string> chordNotes = new List<string>();

        private void Start()
        {
            GetNoteString();
            AddSound();
            AddChord();
        }

        public string GetNoteString()
        {
            if (RolesController.Instance.IsMinorScale)
            {
                noteString = GetNoteStringScale2();
            }
            else
            {
                noteString = GetNoteStringScale1();
            }

            return noteString;
        }

        //MELODY
        public override void PlaySound()
        {
            //InstrumentsController.Instance.CurrentInstrument.GetComponent<Audio_InstrumentSampler>().PlayWwiseMIDIEventWithStop(notes, (TimeManager.Instance.BPS * this.size) + Time.time);
            PlaySound(notes, RolesController.Instance.roles[0].GetComponent<Audio_InstrumentSampler>());  //Melody
        }

        public void PlaySound(List<string> notes, Audio_InstrumentSampler sampler)
        {
            float time = TimeManager.Instance.BPS * this.size;
            sampler.PlayWwiseMIDIEventWithStop(notes, time + Time.time);
            //Debug.Log("Playing note for " + time + " seconds");
        }


        //HARMONY
        public void PlayChord()
        {
            AddChord();
            if(chordNotes.Count == 0) { return; }
            PlaySound(chordNotes, RolesController.Instance.roles[1].GetComponent<Audio_InstrumentSampler>());
            if (PlatformerStats.Instance != null)
            {
                PlatformerStats.Instance.AddHitChord();
            }
        }

        public void AddSound()
        {
            notes.Clear();
            notes.Add(noteString);
        }

        public void AddChord()
        {
            chordNotes.Clear();
            chordNotes.AddRange(FindChordNotes());
        }

        public override List<string> FindChordNotes()
        {
            List<string> names = new List<string>();
            if (RolesController.Instance.HarmonyNotes.Count > 0)
            {
                foreach (NotePiece piece in RolesController.Instance.HarmonyNotes)
                {
                    if (piece.x == x)
                    {
                        names.Add(piece.GetNoteString()); ;
                    }
                }
            }

            return names;
        }

        public override void Restart()
        {
            isActivated = false;
        }

        public override void Activate(bool isPow = false)
        {
            if (!isActivated)
            {
                if (PlatformerStats.Instance != null){ PlatformerStats.Instance.AddHitNote(); }
                PlaySound();
                if (isPow) { PlayChord(); }
                isActivated = true;
            }
        }



        public string GetNoteStringScale1()
        {
            switch (y)
            {
                case 0:
                    return "B3";
                case 1:
                    return "C4";
                case 2:
                    return "D4";
                case 3:
                    return "E4";
                case 4:
                    return "F4";
                case 5:
                    return "G4";
                case 6:
                    return "A4";
                case 7:
                    return "B4";
                case 8:
                    return "C5";
                case 9:
                    return "D5";
                case 10:
                    return "E5";
                case 11:
                    return "F5";   
            }

            return "B3";
        }

        public string GetNoteStringScale2()
        {
            switch (y)
            {
                case 0:
                    return "A#3";
                case 1:
                    return "C4";
                case 2:
                    return "D4";
                case 3:
                    return "D#4";
                case 4:
                    return "F4";
                case 5:
                    return "G4";
                case 6:
                    return "G#4";
                case 7:
                    return "A#4";
                case 8:
                    return "C5";
                case 9:
                    return "D5";
                case 10:
                    return "D#5";
                case 11:
                    return "F5";
            }

            return "A#3";
        }
    }
}
