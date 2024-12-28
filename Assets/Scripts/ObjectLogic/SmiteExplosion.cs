using System.Drawing;
using UnityEngine;

public class SmiteExplosion : MonoBehaviour
{

    float Timer = 0f;
    readonly float TimerMax = .3f;
    readonly float ScalingFactor = .1f;
    float Charge;
    bool Expanding = true;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Expanding?");
        if (Expanding)
        {
            if (Timer < TimerMax)
            {
                transform.localScale += Vector3.one * Charge * ScalingFactor * Time.deltaTime;
                Timer += Time.deltaTime;
                //Debug.Log(transform.localScale);
                Debug.LogFormat("Time:{0}", Timer);
            } else
            Expanding = false;
        }
        else {
            if (Timer > 0)
            {
                transform.localScale += Vector3.one * Charge * ScalingFactor * Time.deltaTime;
                Timer -= Time.deltaTime;
                //Debug.Log(transform.localScale);
            }
            else Destroy(gameObject);
        }

    }

    public void SetCharge(float charge) {
        this.Charge = charge;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 3) return;
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        Player player = other.GetComponent<Player>();
        Health hp = other.GetComponent<Health>();
        //ADD SPECIFIC METHODS TO KNOCKBACK THINGS THAT HAVE BEHAVIOR COMPONENTS
        Debug.LogFormat("{0} hit!", other.name);

        if (rb)
        {
            rb.AddForce((other.transform.position - transform.position) * ScalingFactor, ForceMode.Impulse);
        }


        if (player)
        {
            //StartCoroutine(force, 1f);
            player.Knockback(Charge * ScalingFactor);
        }

        if (hp && !CompareTag(other.tag))
        {
            hp.Val -= Charge;
        }
    }
}
