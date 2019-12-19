using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] path;

    public PathCreator creator;
    public float spacing = 0.5f;
    public float resolution = 1;

    public void Awake()
    {
        // calculate path        
        path = creator.path.CalculateEvenlySpacedPoints(spacing, resolution);
    }
}
