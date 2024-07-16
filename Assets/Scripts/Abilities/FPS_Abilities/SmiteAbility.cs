using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SmiteAbility : Attack
{
    // Start is called before the first frame update

    bool charging;

    GameObject smite;

    [SerializeField]
    float KnockbackRadius = 3f;

    [SerializeField]
    float KnockbackPower;

    [SerializeField]
    float DamageNumber = 50f;

    //SMITE POSITION NEEDS TO BE .5 * distance
    //SMITE SCALE NEEDS TO BE distance - 1
    //SMITE FORWARD NEEDS TO BE THE END POINT

    public void Awake()
    {
        base.Start();
        Cost = 5f;
        AbilityName = "Damnation";
        foreach (Transform t in transform)
        {
            if (t.tag == "CastingPoint")
                CastingPoint = t;
            break;
        }
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Attack")) animator.SetTrigger("Smite");
    }

    void Fire() {
        RaycastHit hit;
        Vector3 CastPoint = cam.transform.position;
        Vector3 EndPoint;
        smite = Instantiate(Resources.Load("Smite") as GameObject);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 30))
        {
            EndPoint = hit.point;
        }
        else
        {
            EndPoint = cam.transform.position + cam.transform.forward * 30;
        }

        
        float distance = Vector3.Distance(CastPoint, EndPoint);
        Vector3 halfway = (EndPoint + CastPoint)/2;

        smite.transform.localScale = new Vector3(distance-2, .3f * Wager, .3f * Wager);
        smite.transform.right = cam.transform.forward;
        smite.transform.position = halfway + (smite.transform.right);
        owner.Val -= (Cost * Wager);
        Knockback(CastPoint);
        Damage(EndPoint);
    }

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
                player.Knockback(KnockbackPower * Wager);
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
                hp.Val -= DamageNumber * 1.2f * Wager;
                Debug.Log($"{c.name} is at HP: {hp.Val}");
            }
        }

    }

}
