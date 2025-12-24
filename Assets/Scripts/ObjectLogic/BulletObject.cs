using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public float Power { get; set; }
    public float baseDmg = 10f;
    public Entity owner;

    private void Start()
    {
        transform.localScale *= Power;
        gameObject.tag = owner.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6) //Enivronment layer
            Destroy(gameObject);

        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        if (otherEntity)
        {
            if (otherEntity.tag == gameObject.tag) return;
            otherEntity.Health -= (Power * (baseDmg + Power));
            Destroy(gameObject);
        }
        
    }
}
