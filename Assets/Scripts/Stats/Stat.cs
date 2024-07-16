using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float val;
    public virtual float Val
    { get { return val; } set { val = value; }}

    [SerializeField]
    private float max;
    public float Max
    { get { return max; } set { max = value; } }

    [SerializeField]
    private float regen;
    public float Regen
    { get { return regen; } set { regen = value; } }

    [SerializeField]
    private float cost;
    public float Cost
    { get { return cost; } set { cost = value; } }

    [SerializeField]
    private bool recovering = true;

    public bool Recovering
    { get { return recovering; } set { recovering = value;} }

    public virtual void recover() {
        if (!recovering || val == max) return;
        val += (Time.deltaTime * regen);
        val = Mathf.Clamp(val, 0, max);
    }

    private void Update()
    {
        recover();
    }

}
