using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BulletAbility : Attack
{

    GameObject bullet;

    float timer = 0f;

    [SerializeField]
    public float timerMax = .2f;

    [SerializeField]
    public int BulletForce = 40;

    [SerializeField]
    public float BulletDamage = 10f;


    public override void Start()
    {
        base.Start();
        AbilityName = "Malice";
        bullet = Resources.Load("Bullet") as GameObject;
        if (name == "Player") UpdateUI();
    }

    public void StartFiring(float wager)
    {
        timer = timerMax;
    }

    void Update()
    {
        if (name == "Player")
        {
            if (AttackAction.WasPressedThisFrame())
            {
                StartFiring(Power);
            }
            if (AttackAction.IsInProgress())
            {
                shoot();
            }
        }
        else
        {
            shoot();
        }

    }

    void shoot()
    {

        if (animator)
        {
            animator.SetBool("IsShooting", true);
            return;
        }

        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            CreateBullet();
            timer = 0f;

        }
    }

    public void CreateBullet()
    {
        GameObject newBullet = Instantiate(bullet);
        BulletObject bulletObj = newBullet.AddComponent<BulletObject>();
        
        bulletObj.owner = owner;
        bulletObj.baseDmg = BulletDamage;
        newBullet.transform.position = CastingPoint.position + CastingPoint.forward;

        newBullet.GetComponent<Rigidbody>().AddForce(CastingPoint.forward * BulletForce, ForceMode.Impulse);
        if (!burst.isBursting)
        {
            bulletObj.Power = Power;
            owner.Health -= Cost * Power;
        }
        else {
            bulletObj.Power = BurstPower;
        }
            
    }

    public void stopShooting()
    {
        animator.SetBool("IsShooting", false);
    }

    
}
