using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletAbility : Attack
{

    GameObject bullet;

    float timer = 0f;

    [SerializeField]
    float timerMax;


    public void Awake()
    {
        base.Start();
        AbilityName = "Malice";
        bullet = Resources.Load("Bullet") as GameObject;
        owner = GetComponent<Health>();
        Cost = 1f;
        if (name == "Player") UpdateUI();
    }

    public void StartFiring(float wager)
    {
        timer = timerMax;
        if (player != null)
        {
            if (player.bursting) Cost = 0f;
            else
            {
                Cost = 1f;
            }
        }

    }

    void Update()
    {
        if (name == "Player")
        {
            if (Input.GetButtonDown("Attack"))
            {
                StartFiring(Wager);
            }
            if (Input.GetButton("Attack")) {
                shoot();
            }
        }
        else {
            shoot();
        }

    }

    void shoot() {

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
        bulletObj.Power = Wager;
        bulletObj.owner = owner;
        newBullet.transform.position = CastingPoint.position + CastingPoint.forward;
        newBullet.GetComponent<Rigidbody>().AddForce(CastingPoint.forward * 40, ForceMode.Impulse);
        owner.Val -= Cost * Wager;
    }

    public void stopShooting() {
        animator.SetBool("IsShooting", false);
    }
}
