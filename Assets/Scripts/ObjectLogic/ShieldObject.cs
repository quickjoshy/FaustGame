using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    Player player;
    Health hp;
    Health owner;
    public Transform castingPoint;
    GameObject wave;
    bool onCooldown = false;
    float Cooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);
    }

    private void Update()
    {
        if (onCooldown) {
            Cooldown += Time.deltaTime;
            if (Cooldown >= 2.05f)
            {
                onCooldown = false;
                Cooldown = 0f;
            }
        }
    }

    private void OnDisable()
    {
        if (player == null) return;
        hp = GetComponent<Health>();
        int wager = 1;
        if (player != null)
        {
            wager = player.wager;
        }
        float dmgTaken = .5f * wager * (hp.Max - hp.Val);
        Debug.LogFormat("Wave Stats! Damage Dealt: {0}; Damage Taken: {1}; Wager: {2}", dmgTaken, hp.Max - hp.Val, wager);
        if (hp.Val != hp.Max)
        {
            StartCoroutine(ShootWave(dmgTaken));
            hp.Val = hp.Max;
        }
    }

    private IEnumerator ShootWave(float power)
    {
        Vector3 shieldScale = gameObject.transform.localScale;
        if (onCooldown) yield break;
        onCooldown = true;
        wave = Instantiate(Resources.Load("Wave") as GameObject);
        wave.GetComponent<WaveObject>().power = power;
        wave.GetComponent<WaveObject>().owner = owner;
        wave.transform.forward = this.transform.forward;
        wave.transform.localScale = new Vector3(1, 5, 1);
        wave.transform.position = transform.position;
        wave.tag = castingPoint.parent.tag;
        int timer = 0;
        while (timer < 2000)
        {
            yield return new WaitForSeconds(40);
            wave.transform.position += wave.transform.forward * .4f;
            timer += 40;
        }
        DestroyImmediate(wave);
    }
}
