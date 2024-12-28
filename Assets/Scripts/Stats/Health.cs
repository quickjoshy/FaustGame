using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    public Transform CastingPoint;
    public RectTransform HealthBar;
    private void Start()
    {
        if (CastingPoint == null) CastingPoint = transform;
    }

    public override float Val
    {
        get { 
                return base.Val; 
        }
        set { base.Val = value;
            if (base.Val <= 0)
                Kill();
            if (HealthBar) {
                HealthBar.localScale = new Vector3(Val/Max, 1, 1);
            }
        }
    }

    


    protected virtual void Kill() {
        Player player = FindAnyObjectByType<Player>();
        RTS_Player Rts_Player = FindAnyObjectByType<RTS_Player>();
        if (player && gameObject.tag == "Enemy") player.OnEnemyKill((int)Max);
        if (Rts_Player && gameObject.tag == "Enemy") Rts_Player.OnEnemyKill((int)Max);
        Destroy(gameObject);
    }

    // Update is called once per frame
}
