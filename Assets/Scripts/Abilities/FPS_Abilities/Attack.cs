using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public float DefaultCost;
    [SerializeField]
    float cost;
    public float Cost
    { get { return cost; } set { cost = value; } }


    [SerializeField]
    protected Entity owner;

    protected Burst burst;

    [SerializeField]
    protected Transform CastingPoint;

    public string AbilityName;

    public float Power;
    public float BurstPower;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected Camera cam;

    [SerializeField]
    protected Button[] Buttons = new Button[3];

    public InputAction AttackAction;

    public List<Action> UpgradeFunctions = new List<Action>();

    public virtual void Start() {
        owner = GetComponent<Entity>();
        animator = GetComponent<Animator>();
        burst = GetComponent<Burst>();
        CastingPoint = owner.CastingPoint;
    }

    protected void UpdateUI() {
        GameObject.Find("ActiveAbilityText").GetComponent<Text>().text = string.Format("{0}", AbilityName);
    }

    public virtual void OnEnable()
    {
        if (animator)
        {
            animator.SetBool(AbilityName, true);
            cost = DefaultCost;
            Debug.Log(AbilityName);
        }
    }

    public virtual void OnDisable()
    {
        if (animator) animator.SetBool(AbilityName, false);
    }
}
