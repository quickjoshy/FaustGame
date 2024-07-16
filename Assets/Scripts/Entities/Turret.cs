using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    BulletAbility bullet;
    public float power;
    public virtual void Start()
    {
        bullet = GetComponent<BulletAbility>();
    }

    // Update is called once per frame
}
