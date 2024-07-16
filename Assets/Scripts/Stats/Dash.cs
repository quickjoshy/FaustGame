using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Stat
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dash")) { startDash();}
        recover();
    }

    void startDash() { 
    
    }
}
