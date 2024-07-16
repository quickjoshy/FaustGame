using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_Summon_Ability : RTS_Ability
{
    [SerializeField]
    List<GameObject> Units;

    [SerializeField]
    GameObject ActiveUnit;

    RTS_Player player;

    Camera m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        player = GetComponent<RTS_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) SpawnUnit(ActiveUnit);
        

    }

    public void SpawnUnit(GameObject unit)
    {

        int cost = unit.GetComponent<Behavior>().Power;
        if (player.Points < cost) return;
        
            player.Points -= cost;
            GameObject clone = Instantiate(unit);
            RaycastHit hit;
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            clone.transform.position = hit.point;
        
    }
}
