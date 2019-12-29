using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Queue<float> spawnTimes = new Queue<float>();
    public PathController[] pathControllers;
    public GameObject enemyPrefab;

    public float health = 5f;

    private float timerTime = 0;

    void Start()
    {
        spawnTimes.Enqueue(8);
        spawnTimes.Enqueue(9);
        spawnTimes.Enqueue(10);
    }

    void Update()
    {
        timerTime += Time.deltaTime;
        if(spawnTimes.Count == 0)
        {
            return;
        }
        while(spawnTimes.Peek() <= timerTime)
        {
            spawnTimes.Dequeue();
            enemyPrefab.GetComponent<EnemyBase>().controller = pathControllers[0];
            Instantiate(enemyPrefab);
        }

    }

    public void TakeDamage()
    {
        health -= 1;
    }
}
