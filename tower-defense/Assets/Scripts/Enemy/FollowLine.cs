using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowLine : MonoBehaviour
{

    public PathController controller;
    public float moveSpeed;

    private int positionIndex;

    void Start()
    { 
        transform.position = controller.path[0];
    }


    void Update()
    {
        if(Vector3.Distance(transform.position,controller.path[positionIndex]) < 0.001f)
        {
            if(positionIndex == controller.path.Length - 1)
            {
                // Came to the end of the path
                return;
            }
            else
            {
                positionIndex++;
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, controller.path[positionIndex], moveSpeed * Time.deltaTime);
    }

}
