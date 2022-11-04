using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Composia
{
    public class PlatformerStats : Singleton<PlatformerStats>
    {
        private string _chordsTargetString;
        private string _notesTargetString;

        private int _chordsTarget;
        private int _notesTarget;
        private int _msDelay = 0;

        private int _currentChordsHit = 0;
        private int _currentNotesHit = 0;

        public Text chordsUI;
        public Text notesUI;
        public Text totalDelayUI;



        private void OnEnable()
        {
            GetTargets();
        }

        private void GetTargets()
        {
            _chordsTarget = RolesController.Instance.CountHarmonyChords();
            _notesTarget = RolesController.Instance.CountMelodyNotes();
            CacheStrings();
            UpdateUI();
        }

        private void CacheStrings()
        {
            _chordsTargetString = " / " + _chordsTarget + " chords hit";
            _notesTargetString = " / " + _notesTarget + " notes hit";
        }

        public void UpdateUI()
        {
            chordsUI.text = _currentChordsHit + _chordsTargetString;
            notesUI.text = _currentNotesHit + _notesTargetString;
            totalDelayUI.text = _msDelay + " total MS Delay";
        }

        public void AddHitChord()
        {
            _currentChordsHit++;
            UpdateUI();
        }

        public void AddHitNote()
        {
            _currentNotesHit++;
            UpdateUI();
        }


    }
}