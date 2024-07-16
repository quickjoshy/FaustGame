using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    public float timerMax = 3f;
    ShieldAbility shieldAbility;
    public float strength = 3f;
    void Start()
    {
        shieldAbility = GetComponent<ShieldAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            shieldAbility.use(strength);
            timer = 0f;
        }
    }
}
