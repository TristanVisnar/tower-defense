using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    // Pathing
    public PathController controller;
    public float moveSpeed;

    // Stats
    public float health = 20f;


    private int positionIndex;

    void Start()
    { 
        // position enemy at the start of the path
        transform.position = controller.path[0];
    }


    void Update()
    {
        FollowPath();

        // destroy this game object
        if(health < 0 || Mathf.Approximately(health,0f))
        {
            Destroy(gameObject);
        }
    }

    void FollowPath()
    {
        if (Vector3.Distance(transform.position, controller.path[positionIndex]) < 0.001f)
        {
            if (positionIndex == controller.path.Length - 1)
            {
                // Came to the end of the path
                GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                gameController.TakeDamage();
                Destroy(gameObject);
            }
            else
            {
                positionIndex++;
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, controller.path[positionIndex], moveSpeed * Time.deltaTime);
    }
}
