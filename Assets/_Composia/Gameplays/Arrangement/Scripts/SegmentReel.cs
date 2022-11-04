using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Composia.Arrangement
{
    [RequireComponent(typeof(Segment))]
    public class SegmentReel : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        Segment _segment;

        private void Awake()
        {
            _segment = GetComponent<Segment>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ArrangementController.Instance.AddSegment(_segment);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ArrangementUIController.Instance.Activate();
            ArrangementUIController.Instance.SetItems(_segment.Name, _segment.TimeSignature);
            SegmentPlayer.Instance.SegmentData = _segment.SegmentData;
            SegmentPlayer.Instance.CanPlay = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ArrangementUIController.Instance.Deactivate();
            SegmentPlayer.Instance.Restart();
            SegmentPlayer.Instance.CanPlay = false;
        }
    }
}
