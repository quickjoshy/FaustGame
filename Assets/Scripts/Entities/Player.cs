using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity
{
    Transform m_transform;
    CharacterController m_CharacterController;
    GameObject m_Camera;
    Transform GroundCheck;

    public int Souls;

    [SerializeField]
    WaveManager WaveManager;

    [SerializeField]
    float GroundCheckRadius;
    Rigidbody Rb;

    int JumpHeight = 1;

    [SerializeField]
    Attack activeAttack;

    public int wager = 1;

    float yRotation = 0f;

    [SerializeField]
    float yVelocity = 0f;

    LayerMask GroundLayer;

    [SerializeField]
    float speed;
    Burst burst;
    int jumps;

    [SerializeField]
    int maxJumps;

    public UnityEngine.UI.Image wagerIcon;

    [SerializeField]
    Text SoulDisplay;

    [SerializeField]
    Sprite[] wagerIcons = new Sprite[6];

    [SerializeField] Image LeftCost;

    [SerializeField] Image RightCost;

    [SerializeField]
    Stat[] stats = new Stat[4];

    public Attack[] attacks = new Attack[4];
    private bool falling = true;

    Vector3 KnockbackForce;

    float kb = 0f;

    [SerializeField]
    float CamSens = 1;

    [SerializeField]
    Settings Settings;

    InputAction JumpAction;
    InputAction MoveAction;
    InputAction LookAround;
    InputAction BurstAction;
    InputAction Attack1;
    InputAction Attack2;
    InputAction Attack3;
    InputAction Attack4;
    InputAction WagerUp;
    InputAction WagerDown;
    InputAction QuitAction;
    InputAction AttackAction;
    //put player stats as components on player object, can use GetComponents<Stat>() later to find all stats
    //also GetComponent(typeof(activeStat)); I THINK

    protected override void Start()
    {
        base.Start();
        AttackAction = InputSystem.actions.FindAction("Attack");
        Settings = FindFirstObjectByType<Settings>();
        if (Settings)
            CamSens = Settings.CameraSensitivity * .15f;
        else
            CamSens = .15f;

        JumpAction = InputSystem.actions.FindAction("Jump");
        MoveAction = InputSystem.actions.FindAction("Move");
        LookAround = InputSystem.actions.FindAction("Look");
        BurstAction = InputSystem.actions.FindAction("Burst");
        Attack1 = InputSystem.actions.FindAction("Attack1");
        Attack2 = InputSystem.actions.FindAction("Attack2");
        Attack3 = InputSystem.actions.FindAction("Attack3");
        Attack4 = InputSystem.actions.FindAction("Attack4");
        WagerUp = InputSystem.actions.FindAction("WagerUp");
        WagerDown = InputSystem.actions.FindAction("WagerDown");
        QuitAction = InputSystem.actions.FindAction("Quit");
        stats = GetComponents<Stat>();
        wagerIcons = Resources.LoadAll<Sprite>("dice");
        m_transform = GetComponent<Transform>();
        burst = gameObject.GetComponent<Burst>();
        Rb = gameObject.GetComponent<Rigidbody>();
        m_CharacterController = gameObject.GetComponent<CharacterController>();
        m_Camera = GameObject.Find("PlayerCamera");
        GroundCheck = GameObject.Find("GroundCheck").transform;
        GroundLayer = LayerMask.GetMask("Environment");
        m_Camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        EnableSpell();
        GameObject.Find("ActiveAbilityText").GetComponent<Text>().text = string.Format("{0}", attacks[0].AbilityName);

        foreach (Attack attack in attacks) {
            attack.AttackAction = AttackAction;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (BurstAction.WasPressedThisFrame()) {
            if (burst.Val >= burst.Max)
            {
                burst.isBursting = true;
            }
        }

        if (burst.isBursting)
        {
            burst.Val -= (Time.deltaTime * burst.Drain);
            wagerIcon.sprite = wagerIcons[5];
            if (burst.Val == 0)
            {
                wagerIcon.sprite = wagerIcons[(int)wager - 1];
                burst.isBursting = false;
            }
        }

        if (QuitAction.WasPerformedThisFrame()) {
            //
        }

        else
        {

            if (WagerUp.WasPressedThisFrame())
            {
                wager++;
                wager = Mathf.Clamp(wager, 1, 5);
                wagerIcon.sprite = wagerIcons[(int)wager - 1];
                UpdateCostGraphic(activeAttack, wager);
                activeAttack.Power = wager;
            }
            if (WagerDown.WasPressedThisFrame())
            {
                wager--;
                wager = Mathf.Clamp(wager, 1, 5);
                wagerIcon.sprite = wagerIcons[(int)wager - 1];
                UpdateCostGraphic(activeAttack, wager);
                activeAttack.Power = wager;
            }
        }
        
        MovementLogic();
        CameraLogic();
        ChangeSpell();
        KnockbackDecay();
    }

    void MovementLogic()
    {
        //Everything below relates to movement
        float moveX = MoveAction.ReadValue<Vector2>().x;
        float moveY = MoveAction.ReadValue<Vector2>().y;
        falling = !Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);
        Vector3 move = m_transform.forward * (moveY) + m_transform.right * moveX;

        m_CharacterController.Move(Time.deltaTime * (move * speed));

        m_CharacterController.Move(Time.deltaTime * (-m_Camera.transform.forward * kb));
        

        //falling logic

        if (JumpAction.WasPressedThisFrame())
        {
            falling = true;
            Jump();
        }

        if (falling)
        {
            yVelocity -= (9f * Time.deltaTime);
            m_CharacterController.Move(new Vector3(0f, yVelocity, 0f) * Time.deltaTime);
            yVelocity = Mathf.Clamp(yVelocity, -50f, 50f);
        }
        else
        {
            yVelocity = 0f;
            jumps = maxJumps;
        }


        
    }

    private void Jump()
    {
        if (jumps <= 0) return;
        yVelocity = JumpHeight * 5;
        jumps--;
        Debug.LogFormat("{0} jumps remaining!", jumps);
    }

    void CameraLogic()
    {

        yRotation += LookAround.ReadValue<Vector2>().y * CamSens;
        yRotation = Mathf.Clamp(yRotation, -90, 90);
        m_Camera.transform.localRotation = Quaternion.Euler(-yRotation, 0f, 0f);

        m_transform.Rotate(0f, LookAround.ReadValue<Vector2>().x * CamSens, 0f);
    }

    public void OnEnemyKill(Entity entity) {
        burst.Val += burst.Regen;
        Health += .25f * entity.MaxHealth;
        Souls += entity.SoulReward;
        if (WaveManager) {
            WaveManager.OnEnemyKill();
        }
        UpdateSoulDisplay();
    }

    private void UpdateSoulDisplay()
    {
        SoulDisplay.text = Souls.ToString();
    }

    public void ChangeSpell() {
        if (Attack1.WasPressedThisFrame()) activeAttack = attacks[0];
        if (Attack2.WasPressedThisFrame()) activeAttack = attacks[1];
        if (Attack3.WasPressedThisFrame()) activeAttack = attacks[2];
        if (Attack4.WasPressedThisFrame()) activeAttack = attacks[3];
        if(Attack1.WasPressedThisFrame() || Attack2.WasPressedThisFrame() || Attack3.WasPressedThisFrame() || Attack4.WasPressedThisFrame())
        {
            EnableSpell();
            UpdateCostGraphic(activeAttack, wager);
        }
    }

    void EnableSpell()
    {
        DisableAllSpells();
        if (!activeAttack) activeAttack = attacks[0];
        activeAttack.enabled = true;
        activeAttack.Power = wager;
        Debug.Log(activeAttack.AbilityName + "IS STARTING ACTIVE");
        GameObject.Find("ActiveAbilityText").GetComponent<Text>().text = string.Format("{0}", activeAttack.AbilityName);
    }

    public void DisableAllSpells()
    {
        foreach (Attack attack in attacks)
        {
            attack.enabled = false;
        }
    }

    public void SetActiveSpell(Attack attack) {
        DisableAllSpells();
        activeAttack = attack;
        activeAttack.Power = wager;
        attack.enabled = true;
        GameObject.Find("ActiveAbilityText").GetComponent<Text>().text = string.Format("{0}", activeAttack.AbilityName);
    }

    void KnockbackDecay() {
        float horizontalDecay = 10f;
        if (!falling) horizontalDecay = 100f;
        kb -= horizontalDecay * Time.deltaTime;
        kb = Math.Clamp(kb, 0, 100);
        
    }

    public void Knockback(Vector3 force) {
        KnockbackForce = force;
    }

    public void Knockback(float power) {
        kb = power;
        
    }

    public Attack GetAttackByName(string attackName) {
        Debug.Log("Looking for attack: " + attackName);
        foreach (Attack attack in attacks) {
            if (attack.AbilityName == attackName) return attack;
        }
        Debug.Log(attackName + " not found!");
        return null;
    }

    public Attack GetActiveAttack() {
        return activeAttack;
    }

    public void ChangeSouls(int change) {
        Souls += change;
        UpdateSoulDisplay();
    }

    public void UpdateCostGraphic(Attack activeAttack, float wager)
    {
        Debug.Log("Updating cost graphic");
        this.activeAttack = activeAttack;
        float costWidth = wager * activeAttack.Cost;
        LeftCost.rectTransform.sizeDelta = new Vector2(costWidth, 15);
        RightCost.rectTransform.sizeDelta = new Vector2(costWidth, 15);


        float halfHealthWidth = HealthImage.rectTransform.rect.width / 2;
        float halfCostWidth = LeftCost.rectTransform.rect.width / 2;
        

        LeftCost.rectTransform.anchoredPosition = new Vector2(HealthImage.rectTransform.anchoredPosition.x - (halfHealthWidth) + halfCostWidth, 10.5f);
        RightCost.rectTransform.anchoredPosition = new Vector2(HealthImage.rectTransform.anchoredPosition.x + (halfHealthWidth) - halfCostWidth, 10.5f);
    }

    protected override void Kill()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override float Health
    {
        get {
            return base.Health;
        }

        set {
            base.Health = value;
            UpdateCostGraphic(activeAttack, wager);
        }
    }

}
