using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{

    public string EnemyFaction;

    public float AggroDistance;

    [SerializeField]
    protected Transform Target;

    public int Power;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (tag == "Enemy") EnemyFaction = "Player";
        else if (tag == "Player") EnemyFaction = "Enemy";
    }

    public Transform FindEnemies()
    {
        Vector3 TurretLocation = transform.position;
        Collider[] colliders = Physics.OverlapSphere(TurretLocation, AggroDistance);
        float minDistance = 1000f;
        Transform target = null;
        foreach (Collider c in colliders)
        {
            if (c && c.gameObject.tag == EnemyFaction && c.gameObject.layer == 3)
            {
                float distance = Vector3.Distance(TurretLocation, c.transform.position);
                if (distance < minDistance)
                {
                    target = c.transform;
                    minDistance = distance;
                }
            }
        }
        return target;
    }

}
