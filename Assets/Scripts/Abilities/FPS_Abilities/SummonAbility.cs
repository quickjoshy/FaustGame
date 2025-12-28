using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAbility : Attack
{
    // Start is called before the first frame update
    public List<Transform> turrets;

    [SerializeField]
    int NumTurrets = 5;

    [SerializeField]
    public int NumShots;

    [SerializeField]
    float HealthMult = 1;

    [SerializeField]
    float DmgMult = 1;

    public void Awake()
    {
        base.Start();
        turrets = new List<Transform>(NumTurrets);
        for (int i = 0; i < NumTurrets; i++)
            turrets.Add(null);
        AbilityName = "Communion";
        Cost = DefaultCost;
        foreach (Transform t in transform)
        {
            if (t.tag == "CastingPoint")
                CastingPoint = t;
            break;
        }
        UpdateUI();
    }

    public void BeginSpawn()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(CastingPoint.position, cam.transform.forward, out hit))
        {
            StartCoroutine(SpawnTurret(hit.point));
        }
    }

    public void Alt() {
        RaycastHit hit;
        if (Physics.Raycast(CastingPoint.position, CastingPoint.forward, out hit))
        {
            DirectTurrets(hit.collider.transform.position);
        }
    }
    
    private void DirectTurrets(Vector3 point)
    {
        foreach (Transform t in turrets)
        {
            if (t)
            {
                t.LookAt(point);
            }
        }
    }

    private IEnumerator SpawnTurret(Vector3 SpawnPosition)
    {
        GameObject turret = Instantiate(Resources.Load("FriendlyTurret") as GameObject);
        if (addTurretToArray(turret.transform))
        {
            turret.transform.position = SpawnPosition + new Vector3(0, .5f, 0);
            
            if (burst.isBursting)
            {
                burst.Val -= 15;
                Power = BurstPower;
            }
            else {
                owner.Health -= Power * Cost;
            }
            turret.GetComponent<BulletAbility>().Power = Power;
            turret.GetComponent<BulletAbility>().Cost = HealthMult;
            turret.GetComponent<BulletAbility>().BulletDamage *= DmgMult;
            turret.GetComponent<Entity>().MaxHealth = Power * NumShots * HealthMult;
            turret.GetComponent<Entity>().Health = Power * NumShots * HealthMult;
            yield return new WaitForSeconds(1);
        }

        else
        {
            Debug.Log("Turret destroyed");
            Destroy(turret);
        }
    }

    bool addTurretToArray(Transform NewTurret) {
        for (int i = 0; i < NumTurrets; i++) {
            if (turrets[i] == null)
            {
                turrets[i] = NewTurret;
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (AttackAction.WasPressedThisFrame()) animator.SetTrigger("Summon");
    }
}
