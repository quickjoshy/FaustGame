using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform Destination;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        if(controller) controller.enabled = false;
        other.transform.position = Destination.position + Destination.forward * 3f;
        other.transform.forward = Destination.forward;
        if(controller) controller.enabled = true;
    }


}
