using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class ShieldAbility : Attack
{
    Transform CastPoint;
    GameObject shield;
    GameObject wave;
    bool active = false;
    private Health hp;
    private bool onCooldown;
    float Cooldown = 0f;

    // Start is called before the first frame update
    public void Awake()
    {
        base.Start();
        Cost = 1f;
        AbilityName = "Retribution";
        foreach (Transform t in transform)
        {
            if (t.tag == "CastingPoint")
                CastPoint = t;
            break;
        }
        shield = Instantiate( Resources.Load("Shield") as GameObject);
        shield.transform.parent = CastPoint;
        shield.transform.localPosition = new Vector3(0f, 0f, 2.5f);
        shield.transform.localScale = new Vector3(.01f, .01f, 1);
        shield.transform.forward = CastPoint.forward;
        shield.SetActive(false);
        player = FindAnyObjectByType<Player>();
        UpdateUI();
        
    }

    // Update is called once per frame
    public void use(float wager) {
        //wager affects shield size and expansion speed
        active = !active;

        shield.SetActive(active);

        if (!active)
        {
            shield.transform.localScale = new Vector3(.01f, .01f, .4f);
            onShieldDisable();
        }

    }

    public void Update()
    {

        if (Input.GetButtonDown("Attack") && name == "Player") use(Wager);

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
            if (shield.transform.localScale.x < power - .1f)
            {
                shield.transform.localScale += ((power * Time.deltaTime) * new Vector3(1f, .5f, 0f));
            }

            else if (shield.transform.localScale.x > power + .1f)
            {
                shield.transform.localScale -= ((power * Time.deltaTime) * new Vector3(1f, .5f, 0f));
            }

            /* THE FOLLOWING IS CODE FOR THE BEAM ABILITY: SEND DAMAGE EVERY TICK
            tickTimer += Time.deltaTime;
            if (tickTimer >= tickTimerMax) { 
                tickTimer -= tickTimerMax; 
            
            }
            */
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
        wave = Instantiate(Resources.Load("Wave") as GameObject);
        wave.GetComponent<WaveObject>().power = dmgTaken;
        wave.GetComponent<WaveObject>().owner = owner;
        wave.transform.forward = shield.transform.forward;
        wave.transform.localScale = new Vector3(1, 5, 1);
        wave.transform.position = shield.transform.position;
        wave.GetComponent<Rigidbody>().AddForce(wave.transform.forward * 10, ForceMode.Impulse);
        wave.tag = CastPoint.parent.tag;
        hp.Val = hp.Max;
    }
}
