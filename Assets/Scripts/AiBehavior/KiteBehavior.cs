using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class KiteBehavior : Behavior
{
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    float KiteRadius = 12f;

    float timer = 0f;

    [SerializeField]
    float timerMax = 2f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Target = FindEnemies();
        agent = GetComponent<NavMeshAgent>();
        if(!agent) agent = gameObject.AddComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        //FOR RANGED/KITE ENEMIES, SET THE DESTINATION TO A PLAYER, THEN WHEN THE DISTANCE IS BELOW A THRESHOLD
        //FIND THE POSITION IN THE OPPOSITE DIRECTION OF THE PLAYER AND SET THAT AS THE DESTINATION


        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            timer = 0f;
            Target = FindEnemies();
        }

        if (Target)
        {
            transform.LookAt(Target);
            Kite(Target);
        }
        else Target = FindEnemies();

    }

    private void Kite(Transform target)
    {
        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(transform.position, targetPos);
        //Debug.LogFormat("Distance:{0}/{1}", distance, KiteRadius);
        if (distance < KiteRadius)
        {
            agent.destination = transform.position - (transform.forward * 2);
        }
        else if (distance > KiteRadius + .4f)
        {
            agent.destination = targetPos;
        }
    }
}
