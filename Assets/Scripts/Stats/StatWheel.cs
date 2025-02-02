using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatWheel : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cursor;

    private void Start()
    {
        cursor = GameObject.Find("Cursor");
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("StatWheel")) { confirm(); }
        cursor.transform.position = Input.mousePosition;
    }

    void confirm() {
        Debug.Log("Stat Confirmed!");
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }
}
