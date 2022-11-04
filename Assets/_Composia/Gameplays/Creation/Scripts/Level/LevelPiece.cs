using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Composia
{

	public class LevelPiece : MonoBehaviour {

		public int x;
		public int y;
		public int size;
		public float leftPadding;

		public PieceGroup myGroup = null;

        public void FindCoordinates()
        {
			int oldY = y;
			Vector3 temp = Level.Instance.WorldToGridCoordinates(this.transform.position);

			x = (int)temp.x;
            y = (int)temp.y;

			if(y <= 0) 
			{ 
				y = oldY; 			
			}

			PositionPiece();
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}

        public void MovePieceUp()
		{
			y += 1;
			if (y > 11) { y = 11; }
			PlacePiece();
		}

		public void MovePieceDown()
		{
			y -= 1;
			if (y < 0) { y = 0; }
			PlacePiece();
		}

		public void PlacePiece()
		{
			RolesController.Instance.ReplacePiece(this);
			PositionPiece();
		}

		public void PositionPiece()
		{
			this.transform.position = Level.Instance.GridToWorldCoordinates(x, y);
		}


		public int ReturnPieceIndex()
		{
			int measureLength = Level.Instance.MeasureLength;
			int measureColIndex = x % measureLength;
			int index = measureColIndex + (y * measureLength);

			return index;
		}


#if UNITY_EDITOR
		public void EraseImmediate()
		{
			if (myGroup != null)
			{
				DestroyImmediate(myGroup.gameObject);
			}
			else
			{
				DestroyImmediate(this.gameObject);
			}
		}
#endif

		public void Erase()
		{
			if (myGroup != null)
			{
				Destroy(myGroup.gameObject);
			}
			else 
			{
				Destroy(this.gameObject);
			}
		}
	}
}