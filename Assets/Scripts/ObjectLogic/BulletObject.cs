using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public float Power { get; set; }
    public float baseDmg = 10f;
    public Entity owner;
    [SerializeField]
    private GameObject bulletParticleSystem;

    private void Start()
    {
        transform.localScale = Power * new Vector3(0.1f, 0.1f, 0.1f);
        gameObject.tag = owner.tag;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 6) //Enivronment layer
        {
            bulletParticleSystem.SetActive(true);
            bulletParticleSystem.transform.parent = null;
            bulletParticleSystem.transform.localPosition = transform.position - (transform.forward * 2);
            bulletParticleSystem.transform.localScale = new Vector3(1, 1, 1);
            Destroy(gameObject);
        }

        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        if (otherEntity)
        {
            if (otherEntity.tag == gameObject.tag) return;
            otherEntity.Health -= (Power * (baseDmg + Power));
            Destroy(gameObject);
        }
        
    }
}
