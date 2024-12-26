using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject SpawnObject;

    [SerializeField]
    int cooldown;

    float timer = 0;

    int EnemyNum = 0;
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown) {

            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy()
    {
        GameObject instance = Instantiate(SpawnObject);
        instance.name = EnemyNum.ToString();
        instance.transform.position = gameObject.transform.position + gameObject.transform.forward * 3;
        instance.transform.forward = gameObject.transform.forward;
        EnemyNum++;
    }
}
