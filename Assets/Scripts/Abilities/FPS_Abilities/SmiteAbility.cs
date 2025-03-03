using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SmiteAbility : Attack
{
    // Start is called before the first frame update

    GameObject smite;

    [SerializeField]
    int KnockbackMult;

    [SerializeField]
    float DamageMult;

    [SerializeField]
    int Range = 60;

    float Charge = 0;

    [SerializeField]
    bool Charging = false;

    [SerializeField]
    GameObject ExplosionPrefab;

    LayerMask RayMask;
    Burst PlayerBurst;

    //SMITE POSITION NEEDS TO BE .5 * distance
    //SMITE SCALE NEEDS TO BE distance - 1
    //SMITE FORWARD NEEDS TO BE THE END POINT


    //REMEMBER TO REIMPLEMENT KNOCKBACK UPGRADES


    public void Awake()
    {
        base.Start();
        AbilityName = "Damnation";
        RayMask = LayerMask.GetMask("Environment");
        PlayerBurst = GetComponent<Burst>();
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (AttackAction.IsPressed())
        {
            animator.SetBool("SmiteCharge", true);
            Charging = true;
        }

        else if (AttackAction.WasReleasedThisFrame())
        {
            animator.SetTrigger("Smite");
            animator.SetBool("SmiteCharge", false);
            Charging = false;
        }

        if (Charging) {
            Charge += Time.deltaTime * Wager * (1 + .03f * Wager);
            if (PlayerBurst && player.bursting)
                PlayerBurst.Val -= 30 * Cost * Time.deltaTime;
            owner.Val -= (Cost * Wager * Time.deltaTime);
        }
    }

    void Fire() {
        RaycastHit hit;
        Vector3 CastPoint = cam.transform.position;
        Vector3 EndPoint;
        smite = Instantiate(Resources.Load("Smite") as GameObject);
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Range, RayMask))
            EndPoint = hit.point;
        else
            EndPoint = cam.transform.position + cam.transform.forward * Range;





        float distance = Vector3.Distance(CastPoint, EndPoint);
        Vector3 halfway = (EndPoint + CastPoint)/2;

        smite.transform.localScale = new Vector3(distance-2, .3f * Wager, .3f * Wager);
        smite.transform.right = cam.transform.forward;
        smite.transform.position = halfway + (smite.transform.right);
        SpawnExplosion(EndPoint);
        /*
        Knockback(EndPoint);
        Damage(EndPoint);
        */
        Charge = 0f;
    }

    private void SpawnExplosion(Vector3 Point)
    {
        GameObject explosionObject = Instantiate(ExplosionPrefab, Point, Quaternion.identity);
        explosionObject.tag = tag;
        SmiteExplosion explosionScript = explosionObject.GetComponent<SmiteExplosion>();
        explosionScript.SetValues(Charge, DamageMult, KnockbackMult);
        
    }

    /*
    void Knockback(Vector3 Point) {
        Collider[] colliders = Physics.OverlapSphere(Point, KnockbackRadius);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer != 3) continue;
            float distance = Vector3.Distance(Point, c.transform.position);
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            Player player = c.GetComponent<Player>();
            Vector3 force = (c.transform.position - Point) * KnockbackPower;
            //ADD SPECIFIC METHODS TO KNOCKBACK THINGS THAT HAVE BEHAVIOR COMPONENTS

            if (rb)
            {
                rb.AddForce(force, ForceMode.Impulse);
            }


            if (player) {
                //StartCoroutine(force, 1f);
                player.Knockback(KnockbackPower * Charge);
            }
            
        }
    }

    void Damage(Vector3 Point) {


        Collider[] colliders = Physics.OverlapSphere(Point, KnockbackRadius);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer != 3) continue;

            Health hp = c.GetComponent<Health>();
            if (hp && !CompareTag(c.tag))
            {
                hp.Val -= Charge;
            }
        }

    }

    */

    public override void Upgrade()
    {
        PrepButtons("Damage", "Knockback", "Cost");


        Buttons[0].onClick.AddListener(DamageUp);
        Buttons[1].onClick.AddListener(ForceUp);
        Buttons[2].onClick.AddListener(CostDown);
    }

    void DamageUp() {
        DamageMult += .3f;
        DisableButtons();
    }

    void ForceUp() {
        KnockbackMult++;
        DisableButtons();
    }

    void CostDown() {
        DefaultCost *= .6f;
        Cost = DefaultCost;
        DisableButtons();
    }

}
