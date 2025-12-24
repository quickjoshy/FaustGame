using System.Drawing;
using UnityEngine;

public class SmiteExplosion : MonoBehaviour
{

    float Timer = 0f;
    readonly float TimerMax = .3f;
    float RadiusMult = 1;
    float Charge;
    float DamageMult;
    int KnockbackMult;
    bool Expanding = true;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Expanding?");
        if (Expanding)
        {
            if (Timer < TimerMax)
            {
                transform.localScale += Vector3.one * Charge * RadiusMult * Time.deltaTime;
                Timer += Time.deltaTime;
                //Debug.Log(transform.localScale);
            } else
            Expanding = false;
        }
        else {
            if (Timer > 0)
            {
                transform.localScale += Vector3.one * Charge * RadiusMult * Time.deltaTime;
                Timer -= Time.deltaTime;
                //Debug.Log(transform.localScale);
            }
            else Destroy(gameObject);
        }

    }

    public void SetValues(float charge, float damageMult, int knockbackMult) {
        this.Charge = charge;
        this.DamageMult = damageMult;
        this.KnockbackMult = knockbackMult;
        Debug.LogFormat("Smite Damage: {0}; DamageMult: {1}", Charge * DamageMult, DamageMult);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 3) return;
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        Player player = other.GetComponent<Player>();
        Entity otherEntity = other.GetComponent<Entity>();
        //ADD SPECIFIC METHODS TO KNOCKBACK THINGS THAT HAVE BEHAVIOR COMPONENTS
        Debug.LogFormat("{0} hit!", other.name);

        if (rb)
        {
            rb.AddForce((other.transform.position - transform.position), ForceMode.Impulse);
        }


        if (player)
        {
            //StartCoroutine(force, 1f);
            player.Knockback(Charge * RadiusMult * KnockbackMult);
        }

        if (otherEntity && !CompareTag(other.tag))
        {
            otherEntity.Health -= Charge * DamageMult;
        }
    }

}
