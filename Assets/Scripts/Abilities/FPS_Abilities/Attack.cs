using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField]
    public float DefaultCost;
    [SerializeField]
    float cost;

    [SerializeField]
    public float Cost
    { get { return cost; } set { cost = value; } }


    [SerializeField]
    protected Health owner;

    [SerializeField]
    protected Player player;

    [SerializeField]
    protected Transform CastingPoint;

    public string AbilityName;

    public float Wager;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected Camera cam;

    [SerializeField]
    protected Button[] Buttons = new Button[3];

    protected InputAction AttackAction;

    public virtual void Start() {
        owner = GetComponent<Health>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        if (player)
        {
            AttackAction = InputSystem.actions.FindAction("Attack");
            Debug.Log(AbilityName + " for player loaded");
        }
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

    public virtual void Upgrade()
    {
        throw new NotImplementedException();
    }

    protected void DisableButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            Buttons[i].gameObject.SetActive(false);
        }
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Attack CurrentSpell = player.GetActiveAttack();
        player.enabled = true;
        player.SetActiveSpell(CurrentSpell);
    }

    protected void PrepButtons(string label1, string label2, string label3) {
        for (int i = 0; i < 3; i++)
        {
            Buttons[i].onClick.RemoveAllListeners();
        }

        Text text = Buttons[0].gameObject.transform.GetChild(0).GetComponent<Text>();
        text.text = label1;

        text = Buttons[1].gameObject.transform.GetChild(0).GetComponent<Text>();
        text.text = label2;

        text = Buttons[2].gameObject.transform.GetChild(0).GetComponent<Text>();
        text.text = label3;
    }
}
