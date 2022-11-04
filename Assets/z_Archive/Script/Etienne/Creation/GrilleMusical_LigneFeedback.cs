using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrilleMusical_LigneFeedback : MonoBehaviour
{
    //Manage feedback for lineRenderer
    public GameObject _lineRenderer;

    public void GenerateGridVisuals(List<Vector2> positions)
    {
        List<float> Xpositions = new List<float>();
        List<float> Ypositions = new List<float>();

        foreach (Vector2 pos in positions)
        {
            //Check if positions already exist
            if (!Xpositions.Contains(pos.x))
                Xpositions.Add(pos.x);
            if (!Ypositions.Contains(pos.y))
                Ypositions.Add(pos.y);
        }

        //Generate positions for lineRenderer
        foreach (float x in Xpositions)
        {
            Vector3 startPos = new Vector3(x, Ypositions[0],0);
            Vector3 endPos = new Vector3(x, Ypositions[Ypositions.Count - 1], 0);

            //New lineRenderer           
            LineRenderer newLineRenderer = Instantiate(_lineRenderer).GetComponent<LineRenderer>();
            newLineRenderer.name = "_lnrX_" + x;
            newLineRenderer.transform.parent = this.transform;

            //Add it
            newLineRenderer.SetPosition(0, startPos);
            newLineRenderer.SetPosition(1, endPos);
        }

        foreach (float y in Ypositions)
        {
            Vector3 startPos = new Vector3(Xpositions[0], y, 0);
            Vector3 endPos = new Vector3(Xpositions[Xpositions.Count - 1], y, 0);

            //New lineRenderer
            LineRenderer newLineRenderer = Instantiate(_lineRenderer).GetComponent<LineRenderer>();
            newLineRenderer.name = "_lnrY_" + y;
            newLineRenderer.transform.parent = this.transform;

            //Add it
            newLineRenderer.SetPosition(0, startPos);
            newLineRenderer.SetPosition(1, endPos);
        }
    }
}
