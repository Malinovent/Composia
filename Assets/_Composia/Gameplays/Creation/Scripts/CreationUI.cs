using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Composia
{
    public class CreationUI : Singleton<CreationUI>
    {
        public GameObject panel;

        public List<GameObject> placeholders = new List<GameObject>();

        public GameObject[] measures = new GameObject[4];

        // Start is called before the first frame update
        void Start()
        {
            DisplayPieces();
            AddTextures();
            //DeactivateAllButCurrent(0);
        }

        public void AddTextures()
        {
            for (int i = 0; i < Inventory_Archive.Instance._currentInventory.Count; i++)
            {
                Sprite sprite = Inventory_Archive.Instance._currentInventory[i].GetComponentInChildren<NotePiece>().mySprite;
                if (sprite != null)
                {
                    placeholders[i].GetComponent<Image>().sprite = sprite;
                    placeholders[i].GetComponent<Dragable>().myName = Inventory_Archive.Instance._currentInventory[i].GetComponentInChildren<NotePiece>().type;
                }
            }
        }

        public void HidePanel()
        {
            panel.SetActive(false);
        }

        public void ShowPanel()
        {
            panel.SetActive(true);
        }

        public void DisplayMeasureFeedback(int measureIndex)
        {
            measures[measureIndex].SetActive(true);
        }

        public void HideMeasureFeedback(int measureIndex)
        {
            measures[measureIndex].SetActive(false);
        }

        //Displays the pieces in the panel
        public void DisplayPieces()
        {
            int count = 0;
            foreach (GameObject go in placeholders)
            {
                if(count < Inventory_Archive.Instance._currentInventory.Count) { go.SetActive(true); }
                else { go.SetActive(false); }
                count++;
            }
        }

        public void StopDisplay()
        {
            foreach (GameObject go in placeholders)
            {
                go.SetActive(false);
            }
        }

        //A Piece is added to the inventory display
        public void AddOneToDisplay(NotationPiece p)
        { 
        
        }

        //A piece removed from the inventory display
        public void RemoveOneFromDisplay(NotationPiece p)
        {


        }

        //Displays the contextual menu that pops up when a pieces is selected in the level
        public void DisplayContextualMenu()
        {

        }

        //Start playing music starting at a certain columnIndex
        public void PlaySegement(int col)
        {

        }

        //Goes into the platformer mode
        public void PlayPlatformer()
        {
        }

        //Exits the temple and returns to the world
        public void ExitTemple()
        {

        }

        public void NextRole()
        {
            RolesController.Instance.NextRole();
        }

        public void PreviousRole()
        {
            RolesController.Instance.PreviousRole();
        }

        public void SwitchScale()
        {
            RolesController.Instance.IsMinorScale = !RolesController.Instance.IsMinorScale;
            NotePiece[] pieces = FindObjectsOfType<NotePiece>();
            foreach(NotePiece piece in pieces) { piece.GetNoteString(); }
        }

        private void UpdateMeasureUI(bool isKeyboard)
        {
            foreach (GameObject measure in measures)
            {
                measure.GetComponent<SwitchInputUI>().SwitchUI(isKeyboard);
            }
        }

        public void UpdateInput(bool isKeyboard)
        {
            UpdateMeasureUI(isKeyboard);
            if (isKeyboard) { ActivateAllMeasureInputs(); }
        }

        public void DeactivateAllButCurrent(int measureIndex)
        {
            for (int i = 0; i < measures.Length; i++)
            { 
                if(i == measureIndex) { continue; }
                measures[i].SetActive(false);
            }
        }
        public void ActivateAllMeasureInputs()
        {
            foreach (GameObject go in measures)
            {
                go.SetActive(true);
            }
        }
    }
}
