using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldAbility : Attack
{
    [SerializeField]
    GameObject shield;

    [SerializeField]
    GameObject WavePrefab;

    GameObject wave;
    bool active = false;
    private Health hp;
    private bool onCooldown;
    float Cooldown = 0f;

    [SerializeField]
    float ShieldSize = 1;

    [SerializeField]
    float WaveSize = 1;

    // Start is called before the first frame update
    public void Awake()
    {
        base.Start();
        AbilityName = "Retribution";
        shield.transform.parent = CastingPoint;
        shield.transform.localPosition = new Vector3(0f, 0f, 2.5f);
        shield.transform.localScale = new Vector3(.01f, .01f, 1);
        shield.transform.forward = CastingPoint.forward;
        shield.SetActive(false);
        if(GetComponent<Player>())
            UpdateUI();
    }

    // Update is called once per frame
    public void use(float wager) {
        //wager affects shield size and expansion speed
        active = !active;

        if (!shield) return;
        shield.SetActive(active);

        if (!active)
        {
            shield.transform.localScale = new Vector3(.01f, .01f, .4f);
            onShieldDisable();
        }

    }

    public void Update()
    {

        if (name == "Player" && AttackAction.WasPressedThisFrame()) use(Wager);

        if (onCooldown)
        {
            Cooldown += Time.deltaTime;
            if (Cooldown >= 2.05f)
            {
                onCooldown = false;
                Cooldown = 0f;
            }
        }

        float power = Wager * (1 + (.1f * Wager));
        if (active)
        {
            if (player && owner.gameObject != player.gameObject) owner.Val -= Cost * Time.deltaTime;
            else owner.Val -= Cost * Time.deltaTime * Wager;

            if (!shield) return;

            if (shield.transform.localScale.x < (power * ShieldSize) - .1f)
            {
                shield.transform.localScale += ((power * Time.deltaTime) * new Vector3(1f, .5f, 1f));
            }

            else if (shield.transform.localScale.x > (power * ShieldSize) + .1f)
            {
                shield.transform.localScale -= ((power * Time.deltaTime) * new Vector3(1f, .5f, 1f));
            }
        }
    }

    void onShieldDisable() {
        if (player == null) return;
        hp = shield.GetComponent<Health>();

        if (animator) {
            animator.SetTrigger("ShieldWave");
            return;
        }
        if (hp.Val != hp.Max)
        {
            ShootWave();
            
        }
    }

    public void ShootWave()
    {
        Vector3 shieldScale = gameObject.transform.localScale;
        float dmgTaken = .5f * Wager * (hp.Max - hp.Val);
        if (onCooldown || dmgTaken == 0) return;
        onCooldown = true;
        wave = Instantiate(WavePrefab);
        wave.GetComponent<WaveObject>().power = dmgTaken;
        wave.GetComponent<WaveObject>().owner = owner;
        wave.transform.forward = shield.transform.forward;
        wave.transform.localScale = new Vector3(1, 1, 1) * WaveSize;
        wave.transform.position = shield.transform.position;
        wave.GetComponent<Rigidbody>().AddForce(wave.transform.forward * 10, ForceMode.Impulse);
        wave.tag = CastingPoint.tag;
        hp.Val = hp.Max;
    }

    public override void Upgrade()
    {
        PrepButtons("Shield Size", "Wave Size", "Cost");
        Buttons[0].onClick.AddListener(ShieldUp);
        Buttons[1].onClick.AddListener(WaveUp);
        Buttons[2].onClick.AddListener(CostDown);
    }

    void ShieldUp() {

        ShieldSize += 1f;
        DisableButtons();
    
    }

    void WaveUp() {
        WaveSize += .5f;
        DisableButtons();
    }

    void CostDown() {
        DefaultCost *= .6f;
        Cost = DefaultCost;
        DisableButtons();
    }
}
