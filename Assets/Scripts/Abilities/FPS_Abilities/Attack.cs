using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    float DefaultCost;
    [SerializeField]
    float cost;

    [SerializeField]
    public float Cost
    { get { return cost; } set { cost = value; } }

    [SerializeField]
    protected Health owner;

    protected Player player;

    [SerializeField]
    protected Transform CastingPoint;

    public string AbilityName;

    public float Wager;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected Camera cam;

    public virtual void Start() {
        owner = GetComponent<Health>();
        player = FindAnyObjectByType<Player>();
        animator = GetComponent<Animator>();
        CastingPoint = owner.CastingPoint;
        cam = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        DefaultCost = cost;
    }

    protected void UpdateUI() {
        GameObject.Find("ActiveAbilityText").GetComponent<Text>().text = string.Format("{0}", AbilityName);
    }

    public void OnEnable()
    {
        if (animator)
        {
            animator.SetBool(AbilityName, true);
            Debug.Log(AbilityName);
        }
    }

    public void OnDisable()
    {
        if (animator) animator.SetBool(AbilityName, false);
    }

    public void SetBursting(bool value) {
        if (value)
        {
            Wager = 6f;
            Cost = 0f;
        }
        else
        {
            Wager = player.wager;
            Cost = DefaultCost;
        }
    }

}
