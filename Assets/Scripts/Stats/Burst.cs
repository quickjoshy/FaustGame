using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Burst : Stat
{
    public RectTransform burstBar;

    public override float Val { get => base.Val; set {
            burstBar.localScale = new Vector3(base.Val * .1f, 1f, 1f);
            base.Val = value; 
            base.Val = Mathf.Clamp(base.Val, 0f, Max);
        } 
    }

    private void Start()
    {
        burstBar.localScale = new Vector3(base.Val * .1f, 1f, 1f);
    }

    [SerializeField]
    private float drain;
    public float Drain { get { return drain; } set { drain = value; } }
}
