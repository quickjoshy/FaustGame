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
    private Entity shieldEntity;
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

    private void DisableShield() {
        shield.transform.localScale = new Vector3(.01f, .01f, .4f);
        onShieldDisable();
    }

    // Update is called once per frame
    public void use(float wager) {
        //wager affects shield size and expansion speed
        active = !active;

        if (!shield) return;
        shield.SetActive(active);

        if (!active)
        {
            DisableShield();
        }

    }

    public void Update()
    {

        if (name == "Player" && AttackAction.WasPressedThisFrame()) use(Power);

        if (onCooldown)
        {
            Cooldown += Time.deltaTime;
            if (Cooldown >= 2.05f)
            {
                onCooldown = false;
                Cooldown = 0f;
            }
        }

        float power = Power * (1 + (.1f * Power));
        if (active)
        {
            if (burst.isBursting) {
                Power = 6;
            }
            else
                owner.Health -= Cost * Time.deltaTime * Power;

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
        shieldEntity = shield.GetComponent<Entity>();

        if (animator) {
            animator.SetTrigger("ShieldWave");
            return;
        }
        if (shieldEntity.Health < shieldEntity.MaxHealth)
        {
            ShootWave();
            
        }
    }

    public void ShootWave()
    {
        Vector3 shieldScale = gameObject.transform.localScale;
        if (burst.isBursting) {
            Power = 6;
        }
        float power = .5f * Power * (shieldEntity.MaxHealth - shieldEntity.Health);
        if (onCooldown || power == 0) return;
        onCooldown = true;
        wave = Instantiate(WavePrefab);
        wave.GetComponent<WaveObject>().power = power;
        wave.GetComponent<WaveObject>().owner = owner;
        wave.transform.forward = shield.transform.forward;
        wave.transform.localScale = new Vector3(1, 1, 1) * WaveSize;
        wave.transform.position = shield.transform.position;
        wave.GetComponent<Rigidbody>().AddForce(wave.transform.forward * 10, ForceMode.Impulse);
        wave.tag = CastingPoint.tag;
        shieldEntity.Health = shieldEntity.MaxHealth;
    }

}
