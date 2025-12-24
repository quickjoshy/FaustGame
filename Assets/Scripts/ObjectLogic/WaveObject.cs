using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject : MonoBehaviour
{
    public float power { get; set; }
    public Entity owner { get; set; }


    protected virtual void OnTriggerEnter(Collider other)
    {
        Entity enemyEntity = other.gameObject.GetComponent<Entity>();
        if (enemyEntity && enemyEntity.tag != gameObject.tag)
        {
            enemyEntity.Health -= power;
            Debug.LogFormat("Wave Collision with {0} for {1} damage", other.gameObject.name, power);
        }
    }

}
