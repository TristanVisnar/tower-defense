using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField]
    public Path path;
    public float pathWidth = 10f;

    public void CreatePath() 
    {
        path = new Path(transform.position);    
    }

    public void Reset()
    {
        CreatePath();
    }
}
