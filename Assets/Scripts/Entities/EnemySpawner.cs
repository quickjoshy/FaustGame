using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject SpawnObject;

    [SerializeField]
    float BaseCooldown;

    [SerializeField]
    WaveManager WaveManager;

    float cooldown;

    float timer = 0;

    int EnemyNum = 0;

    private void Start()
    {
        cooldown = Random.Range(BaseCooldown - 2f, BaseCooldown + 2f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown) {

            SpawnEnemy();
            timer = 0;
        }
    }

    private GameObject SpawnEnemy()
    {
        if (!WaveManager.Spawning) return null;

        WaveManager.OnEnemySpawn();
        GameObject instance = Instantiate(SpawnObject);
        instance.name = EnemyNum.ToString();
        instance.transform.position = gameObject.transform.position + gameObject.transform.forward * 3;
        instance.transform.forward = gameObject.transform.forward;
        EnemyNum++;
        cooldown = Random.Range(BaseCooldown - 2f, BaseCooldown + 2f);
        return instance;
    }
}
