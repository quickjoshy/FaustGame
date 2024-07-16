using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderObject : WaveObject
{
    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other) { 
        base.OnTriggerEnter(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Debug.Log("Boulder hit the environment");
            Destroy(this);
        }
    }
}
