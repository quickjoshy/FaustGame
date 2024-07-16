using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject : MonoBehaviour
{
    public float power { get; set; }
    public Health owner { get; set; }


    protected virtual void OnTriggerEnter(Collider other)
    {
        Health enemyHp = other.gameObject.GetComponent<Health>();
        if (enemyHp && enemyHp.tag != gameObject.tag)
        {
            enemyHp.Val -= power;
            Debug.LogFormat("Wave Collision with {0} for {1} damage", other.gameObject.name, power);
        }
    }

}
