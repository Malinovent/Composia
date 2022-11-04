using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Composia.Arrangement
{
    public class SegmentReelsController : MonoBehaviour, IObserver
    {
        [SerializeField]
        List<SegmentData> _melodySegments;
        [SerializeField]
        List<SegmentData> _harmonySegments;
        [SerializeField]
        List<SegmentData> _bassSegments;
        [SerializeField]
        List<SegmentData> _percussionSegments;

        [SerializeField]
        List<string> _allSegmentPaths = new List<string>();
        string _path;

        private List<SegmentData> _currentSegments = new List<SegmentData>();

        public Segment[] segmentsReels;

        void Awake()
        {
            ArrangementController.Instance.AddObserver(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            FindAllSegmentPaths();
            LoadAllSegments();
        }

        public void FindAllSegmentPaths()
        {
            _allSegmentPaths.Clear();
            _path = Application.dataPath + "/SaveFiles/";

            foreach (string file in Directory.GetFiles(_path))
            {
                if (file.EndsWith(".meta")) { continue; }
                string temp = GetSegmentNameFromPath(file);

                _allSegmentPaths.Add(temp);
            }
        }

        public string GetSegmentNameFromPath(string path)
        {
            string temp = path;
            temp = temp.Replace(_path, "");
            temp = temp.Replace(".json", "");

            return temp;
        }

        public void LoadAllSegments()
        {
            LoadSegment(_melodySegments, "Melody_");
            LoadSegment(_harmonySegments, "Harmony_");
            LoadSegment(_bassSegments, "Bass_");
            LoadSegment(_percussionSegments, "Percussion_");

            
        }

        public void LoadSegmentReels(Mode mode)
        {
            switch (mode)
            {
                case Mode.Melody:
                    _currentSegments = _melodySegments;
                    break;
                case Mode.Harmony:
                    _currentSegments = _harmonySegments;
                    break;
                case Mode.Bass:
                    _currentSegments = _bassSegments;
                    break;
                case Mode.Percussion:
                    _currentSegments = _percussionSegments;
                    break;
            }

            int count = Mathf.Min(segmentsReels.Length, _currentSegments.Count);

            for (int i = 0; i < count; i++)
            {
                segmentsReels[i].SetSegmentData(_currentSegments[i]);
            }
        }

        //GO into the savefiles folder and load the apprioriate Segment
        void LoadSegment(List<SegmentData> segment, string prefix)
        {
            segment.Clear();
            foreach (string path in _allSegmentPaths)
            {
                if (path.Contains(prefix))
                {
                    Debug.Log("Loading path: " + path);
                    segment.Add(SaveLoad.LoadSegment(path));
                }
            }
        }

        public void UpdateData(IObservable o)
        {
            if (o is ArrangementController)
            {
                LoadSegmentReels((o as ArrangementController).Mode);
            }
        }
    }
}