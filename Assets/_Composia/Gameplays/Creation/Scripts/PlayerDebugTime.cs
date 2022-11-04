using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Composia
{
    public class PlayerDebugTime : MonoBehaviour, IRestartable
    {
        private float _myCurrentTime = 0;
        private float _nextDistance = 0;
        private float _msDelay = 0;
        private bool _hasStarted = false;
        private int _index = 0;
        private int _maxIndex = 0;

        //References
        public Transform playerObject;
        public Text uiText;

        //Lists
        private List<float> _recordedDistances = new List<float>();

        public List<float> actualTime = new List<float>();
        public List<float> expectedTimes = new List<float>();

        private void Start()
        {
            CalculateExpectedTimes();
            TimeManager.Instance.AddRestartable(this);
            _maxIndex = Level.Instance.TotalColumns;
            UpdateUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (_hasStarted)
            {
                _myCurrentTime += Time.deltaTime;
            }

            if (playerObject.position.x >= _nextDistance)
            {
                _nextDistance += 1;
                //Update Debug
                actualTime.Add(_myCurrentTime);
                _recordedDistances.Add(playerObject.position.x);

                CalculateMSDelay();
                _index++;
                _hasStarted = true;
            }
        }

        private void CalculateExpectedTimes()
        {
            for (int i = 0; i < Level.Instance.TotalColumns; i++)
            {
                expectedTimes.Add(i * TimeManager.Instance.SixteenthBPS);
            }  
        }

        private void CalculateMSDelay()
        {
            if(_index >= _maxIndex) { return; }
            _msDelay = expectedTimes[_index] - actualTime[_index];
            _msDelay *= 1000;
            UpdateUI();
        }

        private void UpdateUI()
        {
            //Debug.Log("Updating UI: " + _index);
            uiText.text = _msDelay.ToString();
        }

        public void Restart()
        {
            _index = 0;
            _myCurrentTime = 0;
            _hasStarted = false;
            _msDelay = 0;
            UpdateUI();
            actualTime.Clear();
            _recordedDistances.Clear();
            _nextDistance = 0;
            UpdateUI();
        }
    }
}
