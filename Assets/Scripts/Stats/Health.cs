using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    public override float Val
    {
        get { 
                return base.Val; 
        }
        set { base.Val = value;
            if (base.Val <= 0)
                kill();
        }
    }

    public Transform CastingPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    protected void kill() {
        Player player = FindAnyObjectByType<Player>();
        RTS_Player Rts_Player = FindAnyObjectByType<RTS_Player>();
        if (player && gameObject.tag == "Enemy") player.OnEnemyKill((int)Max);
        if (Rts_Player && gameObject.tag == "Enemy") Rts_Player.OnEnemyKill((int)Max);
        Destroy(gameObject);
    }

    // Update is called once per frame
}
