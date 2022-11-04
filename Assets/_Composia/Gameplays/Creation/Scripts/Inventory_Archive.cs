using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Composia.LevelCreator;

namespace Composia
{
    public class Inventory_Archive : Singleton<Inventory_Archive>
    {   

        public List<GameObject> _currentInventory = new List<GameObject>();
        public List<GameObject> _fullInventoryList = new List<GameObject>();

        public void RemoveFromInventory(GameObject go)
        {
            _currentInventory.Remove(go);
        }

        public void AddToInventory(GameObject go)
        {
            _currentInventory.Add(go);
        }

        //Adds an object from the full list to the current list using the NoteTypeEnum
        public void AddToInventory(NoteTypeEnum type)
        {
            _currentInventory.Add(ReturnFullInventoryObject(type));
        }

        public GameObject ReturnInventoryObject(NoteTypeEnum type)
        {
            Debug.Log("Returing " + type);
            if(_currentInventory.Count <= 0) { return null; }
            foreach (GameObject go in _currentInventory)
            {
                LevelPiece g = go.GetComponent<LevelPiece>();
                if (g is PieceGroup)
                {
                    if (g.GetComponent<PieceGroup>().notePieces[0].type == type)
                    {
                        return go;
                    }
                }
                else if ((g as NotePiece).type == type)
                {       
                    return go;
                }
            }

            Debug.LogError("Object is not in inventory!");
            return null;
        }

        public GameObject ReturnFullInventoryObject(NoteTypeEnum type)
        {
            foreach (GameObject go in _fullInventoryList)
            {
                if (go.GetComponent<PieceGroup>())
                {
                    if (go.GetComponent<PieceGroup>().notePieces[0].type == type)
                    {
                        return go;
                    }
                }
                else if (go.GetComponent<NotePiece>().type == type)
                {
                    return go;
                }
            }

            Debug.LogError("Object is not in inventory!" + type);
            return null;
        }
    }
}