using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Composia;

[ExecuteInEditMode]
public class SnapToGridTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 gridCoord = Level.Instance.WorldToGridCoordinates(transform.position);
        transform.position = Level.Instance.GridToWorldCoordinates((int)gridCoord.x, (int)gridCoord.y);
    }

    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = (Level.Instance.IsInsideGridBounds(transform.position)) ? Color.green : Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one * Level.Instance.CellSize);
        Gizmos.color = oldColor;
    }
}
