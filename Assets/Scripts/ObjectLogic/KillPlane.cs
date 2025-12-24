using UnityEngine;

public class KillPlane : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.gameObject.GetComponent<Entity>();
        Debug.Log("Kill plane!");
        if (entity) entity.Health = 0;
    }
}
