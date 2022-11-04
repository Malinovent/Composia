using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia
{

    public class GridRuntime : Singleton<GridRuntime>, IObserver
    {
        public List<GridTileInfo> gridTiles;

        [SerializeField][ReadOnly]
        private List<Vector2Int> _melodyGrid = new List<Vector2Int>();
        [SerializeField][ReadOnly]
        private List<Vector2Int> _harmonyGrid = new List<Vector2Int>();
        [SerializeField][ReadOnly]
        private List<Vector2Int> _bassGrid = new List<Vector2Int>();

        Mode currentMode;

        private void Start()
        {
            SubscribeAsObserver();

            ActivateTiles();
        }

        public void SubscribeAsObserver()
        {
            RolesController.Instance.AddObserver(this);
        }

        public void AddValidation(Vector2Int position)
        {
            switch (RolesController.Instance.GetMode())
            {
                case Mode.Melody:
                    AddValidation(position, _melodyGrid);
                    break;
                case Mode.Harmony:
                    AddValidation(position, _harmonyGrid);
                    break;
                case Mode.Bass:
                    AddValidation(position, _bassGrid);
                    break;
            }
        }

        private void AddValidation(Vector2Int position, List<Vector2Int> currentList)
        {
            if (currentList.Contains(position))
            {
                Debug.LogWarning("Spot is already valid!");
                return;
            }

            currentList.Add(position);
        }

        public void RemoveValidation(Vector2Int position)
        {
            switch (currentMode)
            {
                case Mode.Melody:
                    RemoveValidation(position, _melodyGrid);
                    break;
                case Mode.Harmony:
                    RemoveValidation(position, _harmonyGrid);
                    break;
                case Mode.Bass:
                    RemoveValidation(position, _bassGrid);
                    break;
            }
        }

        private void RemoveValidation(Vector2Int position, List<Vector2Int> currentList)
        {
            if (currentList.Contains(position))
            {
                currentList.Remove(position);
            }
        }

        private void ActivateTiles()
        {
            switch (currentMode)
            {
                case Mode.Melody:
                    ActivateTiles(_melodyGrid);
                    break;
                case Mode.Harmony:
                    ActivateTiles(_harmonyGrid);
                    break;
                case Mode.Bass:
                    ActivateTiles(_bassGrid);
                    break;
            }
        }

        private void ActivateTiles(List<Vector2Int> modeTiles)
        {
            foreach (GridTileInfo gt in gridTiles)
            {
                if (modeTiles.Contains(gt.ReturnGridPos()))
                {
                    gt.gameObject.SetActive(true);
                }
                else { gt.gameObject.SetActive(false); }
            }
        }

        private void ValidateTiles()
        {
            switch (currentMode)
            {
                case Mode.Melody:
                    ValidateTiles(_melodyGrid);
                    break;
                case Mode.Harmony:
                    ValidateTiles(_harmonyGrid);
                    break;
                case Mode.Bass:
                    ValidateTiles(_bassGrid);
                    break;
            }
        }

        private void ValidateTiles(List<Vector2Int> modeTiles)
        {
            foreach (GridTileInfo gt in gridTiles)
            {
                if (modeTiles.Contains(gt.ReturnGridPos()))
                {
                    gt.IsValidPlacement = true;
                }
                else { gt.IsValidPlacement = false; }
            }

        }

        public void UpdateValidation()
        {
            currentMode = RolesController.Instance.GetMode();
            ValidateTiles();
        }

        public void UpdateData(IObservable o)
        {
            currentMode = (o as RolesController).GetMode();
            if (Application.isPlaying)
            {
                //Debug.Log("Activating Tiles!");
                ActivateTiles();
            }
        }
    }
}
