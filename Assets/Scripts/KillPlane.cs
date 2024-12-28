using UnityEngine;

public class KillPlane : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Health Hp = other.gameObject.GetComponent<Health>();
        Debug.Log("Kill plane!");
        if (Hp) Hp.Val = 0;
    }
}
