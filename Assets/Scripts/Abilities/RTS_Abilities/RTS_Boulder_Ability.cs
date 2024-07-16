using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_Boulder_Ability : RTS_Ability
{

    Camera m_Camera;
    GameObject Boulder;
    RTS_Player player;

    [SerializeField]
    float DropHeight = 10f;
    // Start is called before the first frame update
    void Start()
    {

        // MAYBE REWORK ABILITY TO TAKE LONGER TO DROP WITH CIRCLE OUTLINE WHERE IT WILL DROP GETTING SMALLER AS IT GETS CLOSER
         
        m_Camera = GetComponent<Camera>();
        Boulder = Resources.Load("Boulder") as GameObject;
        player = GetComponent<RTS_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) DropBoulder();
    }

    void DropBoulder()
    {
        if (player.Points < Cost) return;
        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        Vector3 target = hit.point;

        GameObject clone = Instantiate(Boulder);
        clone.transform.position = target + new Vector3(0, DropHeight, 0);
        clone.GetComponent<WaveObject>().power = 200f;
        clone.tag = "Player";
        player.Points -= Cost;


        
    }
}
