using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{
    public class LadyBugController : Singleton<LadyBugController>, IObserver
    {
        [System.Serializable]
        public struct GridPosition
        {
            public int x;
            public int y;
        }

        [SerializeField]
        public GridPosition gridPos;
        public float speed = 1;
        public List<Vector2Int> gridPositionEverySecond = new List<Vector2Int>();
        public List<Vector2Int> gridPositionEveryBeat = new List<Vector2Int>();

        public bool isDebugging = false;
        private float timePassed = 0;
        private float beatTimePassed = 0;

        public int currentMeasure = 0;
        private float beat;
        private bool _isMoving = false;
        private Vector3 _initialPos;

        public bool IsMoving { get => _isMoving; set => _isMoving = value; }
        TimeManager timeManager;
        #region OVERRIDES

        private void Awake()
        {
            timeManager = TimeManager.Instance;
            timeManager.AddObserver(this);
            Level.Instance.AddObserver(this);
            _initialPos = this.transform.position;
        }

        public virtual void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        public virtual void FixedUpdate()
        {
            
            DebugTime();
            DebugBeatTime();
        }

        private void Update()
        {
            Move();
        }
        #endregion

        private void Initialize()
        {
            UpdateGridPosition();
            beat = timeManager.BPS;

            speed = speed * timeManager.BPS * 4;
        }

        public virtual void BeatUpdate()
        {
            
        }

        public void Pause()
        {
            IsMoving = false;
        }

        public void Stop()
        {
            this.transform.position = _initialPos;
            IsMoving = false;
        }

        public void Play()
        {
            IsMoving = true;
        }

        public virtual void Move()
        {
            if (IsMoving)
            {
                this.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }

        private void DebugTime()
        {
            if (isDebugging)
            {
                timePassed += Time.deltaTime;
                if (timePassed >= 1)
                {
                    gridPositionEverySecond.Add(UpdateGridPosition());
                    timePassed = 0;
                }
            }
        }

        private void DebugBeatTime()
        {
            if (isDebugging)
            {
                beatTimePassed += Time.deltaTime;

                if (beatTimePassed > beat)
                {
                    gridPositionEveryBeat.Add(UpdateGridPosition());
                    beatTimePassed = 0;
                }
            }
        }

        public Vector2Int UpdateGridPosition()
        {
            Vector2 pos = Level.Instance.WorldToGridCoordinates(this.transform.position);
            gridPos.x = (int)pos.x;
            gridPos.y = (int)pos.y;
            CheckMeasure();
            return new Vector2Int(gridPos.x, gridPos.y);
        }

        public void CheckMeasure()
        {
            if (gridPos.x % Level.Instance.MeasureLength == 0) 
            { 
                currentMeasure = Level.Instance.FindMeasureIndex(gridPos.x); 
                //Debug.Log("Current measure is: " + currentMeasure); 
            }
        }

        public void UpdateData(IObservable o)
        {
            if (o is TimeManager)
            {
                UpdateGridPosition();
            }
        }
    }
}