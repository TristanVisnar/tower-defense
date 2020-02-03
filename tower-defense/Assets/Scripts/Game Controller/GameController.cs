using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Queue<float> spawnTimes = new Queue<float>();
    public PathController[] pathControllers;
    public GameObject enemyPrefab;

    public float health = 5f;

    public bool playing = false;

    private float timerTime = 0;
    public float playSpeedMultiplier = 1;

    void Start()
    {
        spawnTimes.Enqueue(8);
        spawnTimes.Enqueue(9);
        spawnTimes.Enqueue(10);
    }

    void Update()
    {
        if (!playing) {
            return;        
        }

        timerTime += Time.deltaTime * playSpeedMultiplier;
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

    public void Pause() 
    {
        playing = false;
    }

    public void Play(float speed) 
    {
        playing = true;
        playSpeedMultiplier = speed;
        
    }

    public void TakeDamage()
    {
        health -= 1;
    }
}
