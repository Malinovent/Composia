using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RenderJumpPossibility : MonoBehaviour
{
    private JumpQuickTest _jumpQuickTest { get { return GetComponentInParent<JumpQuickTest>(); } }
    [HideInInspector]
    public LineRenderer lr { get { return GetComponent<LineRenderer>(); } }

    [Header("Actual angle")]
    public float velocity;
    public float angle;

    [Header("Clean Vertex")]
    public int resolution;
    public Vector3 previousPos;

    [Header("Physics")]
    public float gravity;
    public float radiantAngle;

    private void Awake()
    {
        gravity = Mathf.Abs(Physics.gravity.y);
        //RenderLine();
    }

    //private void OnValidate()
    //{
    //    if (lr != null && Application.isPlaying)
    //    {
    //        RenderLine();
    //    }
    //}

    //private void Start()
    //{
    //    RenderLine();
    //}

    public void CheckJump(float f_angle, float f_velocity)
    {
        //if (_jumpQuickTest._rigidbody.velocity == Vector3.zero)
        //{
        angle = f_angle;
        velocity = f_velocity;
        previousPos = _jumpQuickTest.transform.position;
        RenderLine();
        //}
    }

    public void RenderLine()
    {
        lr.positionCount = (resolution + 1);
        lr.SetPositions(CalculateArcArray());
    }

    private Vector3[] CalculateArcArray()
    {
        Vector3[] _arcArray = new Vector3[resolution + 1];

        radiantAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radiantAngle)) / gravity;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            _arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return _arcArray;
    }

    private Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = (t * maxDistance);
        float y = x * Mathf.Tan(radiantAngle) - 
            ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radiantAngle) * Mathf.Cos(radiantAngle)));
        Vector3 arc = new Vector3(x,y,0);
        return previousPos + arc;
    }
}
