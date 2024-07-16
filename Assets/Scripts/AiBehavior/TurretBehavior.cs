using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TurretBehavior : Behavior
{
    // Start is called before the first frame update

    float timer = 0f;

    [SerializeField]
    float timerMax = 3f;

    public override void Start()
    {
        base.Start();
        Target = FindEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerMax) {
            timer = 0f;
            Target = FindEnemies();
        }

        transform.LookAt(Target);
    }
}
