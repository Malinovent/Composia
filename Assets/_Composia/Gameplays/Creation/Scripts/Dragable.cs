using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Composia
{
    public class Dragable : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        private RectTransform dragRectTransform;
        private Canvas canvas;
       // public string myName = "Quarter Note";
        public NoteTypeEnum myName;
        public RectTransform panel;

        private float maxHeight;
        private RectTransform cursorObject;

        public bool isDebugMode = false;

        public static List<Dragable> dragables = new List<Dragable>();
        public GameObject selectedUI;

        private void Awake()
        {
            selectedUI.SetActive(false);
            dragables.Add(this);
            cursorObject = PlayerController_Creation.Instance.cursor.GetComponent<RectTransform>();
            dragRectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            panel = this.transform.parent.GetComponent<RectTransform>();
            maxHeight = panel.rect.height;
        }

        
        private void Update()
        {

            if (GetWorldSpaceRect(dragRectTransform).Contains(GetWorldSpaceRect(cursorObject).position))
            {
                Debug.Log(myName + " has the mouse!");
                OnControllerInput();
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            if (dragRectTransform.anchoredPosition.y + dragRectTransform.rect.height > maxHeight)
            { 
                dragRectTransform.anchoredPosition = new Vector2(dragRectTransform.anchoredPosition.x, maxHeight - dragRectTransform.rect.height);
            }
        }

        public void OnControllerInput()
        {
            //if(Input)
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                PlayerController_Creation.Instance.CachePiece(myName);
                Debug.Log("cached " + myName);
                DisplaySelectedUI();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PlayerController_Creation.Instance.CachePiece(myName);
            DisplaySelectedUI();
            //Debug.Log("I was pressed");
        }

        private void DisplaySelectedUI()
        {
            foreach (Dragable d in dragables)
            {
                if(d == this) { d.selectedUI.SetActive(true); continue; }
                d.selectedUI.SetActive(false);
            }
        }

        private Rect GetWorldSpaceRect(RectTransform rt)
        {
            Rect r = rt.rect;
            r.center = rt.TransformPoint(r.center);
            r.size = rt.TransformVector(r.size);
            return r;
        }

    }
}
