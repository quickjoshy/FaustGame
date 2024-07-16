using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public float Power { get; set; }
    float baseDmg = 10f;
    public Health owner;

    private void Start()
    {
        transform.localScale *= Power;
        gameObject.tag = owner.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health hp = other.gameObject.GetComponent<Health>();
        if (hp)
        {
            if (hp.tag == gameObject.tag) return;
            hp.Val -= (Power * (baseDmg + Power));
        }
        Destroy(gameObject);
    }
}
