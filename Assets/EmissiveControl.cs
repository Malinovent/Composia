using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveControl : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float intensity;
    

    public Color baseColor = Color.white;


    private void OnValidate()
    {
        if (!mat) { return; }

        mat.SetVector("_EmissionColor", new Vector4(baseColor.r, baseColor.g, baseColor.b) * intensity);
    }

    private void Update()
    {
        mat.SetVector("_EmissionColor", new Vector4(baseColor.r, baseColor.g, baseColor.b) * intensity);
    }

}
