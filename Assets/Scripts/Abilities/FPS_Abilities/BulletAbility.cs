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
        owner = GetComponent<Health>();
        if (name == "Player") UpdateUI();

        UpgradeFunctions.Add(DamageUp);
        UpgradeFunctions.Add(ForceUp);
        UpgradeFunctions.Add(RateUp);
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
            if (AttackAction.WasPressedThisFrame())
            {
                StartFiring(Wager);
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
        KiteBehavior kite = GetComponent<KiteBehavior>();
        GameObject newBullet = Instantiate(bullet);
        BulletObject bulletObj = newBullet.AddComponent<BulletObject>();
        bulletObj.Power = Wager;
        bulletObj.owner = owner;
        bulletObj.baseDmg = BulletDamage;
        newBullet.transform.position = CastingPoint.position + CastingPoint.forward;

        newBullet.GetComponent<Rigidbody>().AddForce(CastingPoint.forward * BulletForce, ForceMode.Impulse);
        owner.Val -= Cost * Wager;
    }

    public void stopShooting()
    {
        animator.SetBool("IsShooting", false);
    }

    public override void Upgrade()
    {
        PrepButtons("Damage", "Force", "Fire Rate");


        Buttons[0].onClick.AddListener(DamageUp);
        Buttons[1].onClick.AddListener(ForceUp);
        Buttons[2].onClick.AddListener(RateUp);
    }

    void DamageUp()
    {
        BulletDamage += 5f;
 
        Debug.Log("Malice Dmg!");
    }

    void ForceUp()
    {
        BulletForce += 20;
        Debug.Log("Malice Range!");
    }

    void RateUp()
    {
        animator.SetFloat("ShootingSpeed", animator.GetFloat("ShootingSpeed") + .30f);
        Debug.Log("Malice Rate!");
    }


    
}
