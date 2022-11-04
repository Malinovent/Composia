using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;
    public float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        CalculateOffset();
    }

    private void CalculateOffset()
    {
        offset = this.transform.position - target.position;
    }
    private void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
