using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerBehavior : Behavior
{
    NavMeshAgent agent;
    Player player;
    
    //EVENTUALLY WILL HAVE TO CHANGE THIS TO SUIT BOTH PLAYER AND ENEMY SUMMONS ALIKE!
    //WILL NEED TO SCAN THE NEAR AREA AND CHOOSE THE CLOSEST OPPOSING ENTITY TO CHASE

    //FOR RANGED/KITE ENEMIES, SET THE DESTINATION TO A PLAYER, THEN WHEN THE DISTANCE IS BELOW A THRESHOLD
    //FIND THE POSITION IN THE OPPOSITE DIRECTION OF THE PLAYER AND SET THAT AS THE DESTINATION


    public override void Start()
    {
        base.Start();
        agent = gameObject.AddComponent<NavMeshAgent>();
        player = FindAnyObjectByType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Vector3.zero;
        if (player) { playerPos = player.transform.position; }
        agent.destination = playerPos;
        transform.LookAt(playerPos);
    }
}
