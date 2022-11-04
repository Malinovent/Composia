using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public GameObject rotateObject;
    public float rotateSpeed = 1;
    public Vector3 rotateDirection;

    // Update is called once per frame
    void Update()
    {
        rotateObject.transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
    }
}
