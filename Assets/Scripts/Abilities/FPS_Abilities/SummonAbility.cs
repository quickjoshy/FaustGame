using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAbility : Attack
{
    // Start is called before the first frame update
    public Transform[] turrets;

    [SerializeField]
    public int NumShots;

    public void Awake()
    {
        base.Start();
        turrets = new Transform[6];
        AbilityName = "Communion";
        foreach (Transform t in transform)
        {
            if (t.tag == "CastingPoint")
                CastingPoint = t;
            break;
        }
        UpdateUI();
    }

    // Update is called once per frame

    public void BeginSpawn()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(CastingPoint.position, cam.transform.forward, out hit))
        {
            StartCoroutine(SpawnTurret(hit.point));
        }
    }

    public void alt() {
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
            turret.GetComponent<BulletAbility>().Wager = Wager;
            turret.GetComponent<Health>().Max = Wager * NumShots;
            turret.GetComponent<Health>().Val = Wager * NumShots;
            owner.Val -= Wager * Cost * 4;
            Debug.Log(turret);
            yield return new WaitForSeconds(1);
        }

        else
        {
            Debug.Log("Turret destroyed");
            Destroy(turret);
        }
    }

    bool addTurretToArray(Transform NewTurret) {
        for (int i = 0; i < turrets.Length; i++) {
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
        if (Input.GetButtonDown("Attack")) animator.SetTrigger("Summon");
    }
}
