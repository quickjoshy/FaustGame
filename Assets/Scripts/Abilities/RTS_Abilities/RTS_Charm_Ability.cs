using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RTS_Charm_Ability : RTS_Ability
{
    // Start is called before the first frame update
    Camera m_Camera;

    RTS_Player player;
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        player = GetComponent<RTS_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) SelectEnemy();
    }

    GameObject SelectEnemy() {
        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        if (!hit.collider) return null;
        GameObject enemy = hit.collider.gameObject;
        if (!enemy) return null;

        if (enemy.layer == 3 && enemy.tag == "Enemy") {
            Behavior behavior = enemy.GetComponent<Behavior>();
            if (player.Points >= behavior.Power) {
                player.Points -= behavior.Power;
                enemy.tag = "Player";
                behavior.EnemyFaction = "Enemy";
                return enemy;
            }
        }
        return null;
    }
}
