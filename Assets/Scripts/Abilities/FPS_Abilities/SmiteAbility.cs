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
    float RadiusMult;

    [SerializeField]
    int Range = 60;

    float Charge = 0;

    [SerializeField]
    bool Charging = false;

    [SerializeField]
    GameObject ExplosionPrefab;

    LayerMask RayMask;

    //SMITE POSITION NEEDS TO BE .5 * distance
    //SMITE SCALE NEEDS TO BE distance - 1
    //SMITE FORWARD NEEDS TO BE THE END POINT


    //REMEMBER TO REIMPLEMENT KNOCKBACK UPGRADES


    public void Awake()
    {
        base.Start();
        AbilityName = "Damnation";
        RayMask = LayerMask.GetMask("Environment");
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
            if (!burst.isBursting)
                owner.Health -= (Cost * Power * Time.deltaTime);
            else
                Power = 6;
            Charge += Time.deltaTime * Power * (1 + .03f * Power);
            

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

        smite.transform.localScale = new Vector3(distance-2, .3f * Power, .3f * Power);
        smite.transform.right = cam.transform.forward;
        smite.transform.position = halfway + (smite.transform.right);
        SpawnExplosion(EndPoint);
        Charge = 0f;
    }

    private void SpawnExplosion(Vector3 Point)
    {
        GameObject explosionObject = Instantiate(ExplosionPrefab, Point, Quaternion.identity);
        explosionObject.tag = tag;
        SmiteExplosion explosionScript = explosionObject.GetComponent<SmiteExplosion>();
        explosionScript.SetValues(Charge, DamageMult, KnockbackMult, RadiusMult);
        
    }

}
