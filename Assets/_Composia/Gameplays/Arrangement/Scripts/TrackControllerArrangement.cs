using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Composia.Arrangement
{
    [RequireComponent(typeof(Track))]
    public class TrackControllerArrangement : MonoBehaviour, IObserver, IRestartable
    {
        public Outline[] segmentOutlineObjects = new Outline[4];
        [SerializeField]
        private Track _track;

        private ArrangementController ac;

       // private int _segmentInPlayIndex = 0;
       // private int _beatIndex;

        private int _segmentOutlineIndex = 0;

        public Color normalOutline = Color.white;
        public Color filledOutline = Color.red;

        public float normalLineWidth = 3;
        public float selectedLineWitdh = 7;

        public int SegmentOutlineIndex { get => _segmentOutlineIndex; set => _segmentOutlineIndex = value; }
        public Track Track { get => _track; set => _track = value; }

        public TrackControllerArrangement(ArrangementController ac)
        {
            this.ac = ac;
        }

        private void Awake()
        {
            // _normalOutline = trackOutlineObjects[0].OutlineColor;
            Track = GetComponent<Track>();
            this.ac = ArrangementController.Instance;
            ac.AddRestartable(this);
            AddFillOutlines();
           
        }

        public void UpdateData(IObservable o)
        {           
            Track.TryPlaySound(ac.SegmentIndex, ac.CurrentBeat, ac.BPS);
        }

        public void AddTrack(int index, Segment segment)
        {
            Track.AddSegment(index, segment);
            AddFillOutline(index);
        }

        public void AddTrack(Segment segment)
        {
            Track.AddSegment(SegmentOutlineIndex, segment);
            AddFillOutline(SegmentOutlineIndex);
        }

        public void RemoveSegment(int index)
        {
            Track.RemoveSegment(index);
        }


        public void ShiftOutlineObject(int index)
        {          
            if (!AddFillOutline(SegmentOutlineIndex)) { segmentOutlineObjects[SegmentOutlineIndex].enabled = false; }
            segmentOutlineObjects[SegmentOutlineIndex].OutlineWidth = normalLineWidth;

            SegmentOutlineIndex += index;
            
            if (SegmentOutlineIndex >= segmentOutlineObjects.Length) 
            { 
                SegmentOutlineIndex = 0; 
            }
            else if (SegmentOutlineIndex < 0) 
            { 
                SegmentOutlineIndex = segmentOutlineObjects.Length - 1; 
            }

            segmentOutlineObjects[SegmentOutlineIndex].enabled = true;
            segmentOutlineObjects[SegmentOutlineIndex].OutlineWidth = selectedLineWitdh;
        }

        public void RemoveOutlines()
        {
            foreach (Outline o in segmentOutlineObjects)
            {
                o.enabled = false;
            }

            AddFillOutlines();
        }

        public void AddFillOutlines()
        {
            for (int i = 0; i < segmentOutlineObjects.Length; i++)
            {            
                AddFillOutline(i);               
            }
        }

        public bool AddFillOutline(int index)
        {
            if (Track._segmentsData.Length < index + 1) { return false; }
            //if (Track._segmentsData[index].myName == null) { return false; }

            if (Track._segmentsData[index].myName != null)
            {
                segmentOutlineObjects[index].enabled = true;
                segmentOutlineObjects[index].OutlineColor = filledOutline;
                return true;
            }
            else 
            {
                segmentOutlineObjects[index].OutlineColor = normalOutline;
                return false;
            }
        }

        public void ResetOutlineIndex()
        {
            SegmentOutlineIndex = 0;
        }

        public int GetLastOutlineIndex()
        {
            return SegmentOutlineIndex;
        }

        public void Restart()
        {
            Track.Restart();
        }
    }
}