using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float cooldownLenght = 1f;
    public float range = 10f;
    public float damage = 1f;

    private float cooldownCounter = 0f;
    private bool onCooldown = false;


    public void Update()
    {
        if (!onCooldown)
        {
            GameObject enemy = FindClosestEnemy();
            if (enemy == null)
            {
                // no enemies
                return;
            }

            if((enemy.transform.position - transform.position).magnitude < range)
            {
                enemy.GetComponent<EnemyBase>().health -= damage;
                cooldownCounter = cooldownLenght;
                onCooldown = true;
            }
        }
        else
        {
            cooldownCounter -= Time.deltaTime;
            if(cooldownCounter < 0f)
            {
                onCooldown = false;
            }
        }
        
       
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        if (gos.Length <= 0)
        {
            return null;
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
