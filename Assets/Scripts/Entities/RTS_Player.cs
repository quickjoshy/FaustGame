using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RTS_Player : MonoBehaviour
{

    [SerializeField]
    RTS_Ability ActiveAbility;

    [SerializeField]
    List<RTS_Ability> abilities;

    [SerializeField]
    float xSpeed;

    [SerializeField]
    float ySpeed;

    [SerializeField]
    float MinRadius;

    [SerializeField]
    float MaxRadius;

    
    public int Points {
        get { return _points;  }
        set { _points = value;
            PointsText.text = string.Format("Blood Points: {0}", value);
        }
    }

    [SerializeField]
    private int _points = 0;

    Transform PlayerTrans;

    Text PointsText;

    Vector3 playerPos;
    float x;
    float y;

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerTrans = FindAnyObjectByType<Player>().transform;
        PointsText = GameObject.Find("PointsText").GetComponent<Text>();
        Points = Points;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = PlayerTrans.position;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        int input;
        int.TryParse(Input.inputString, out input);
        Debug.Log($"Input: {input}");
        if (input > 0 && input <= abilities.Count) {
            SetActiveSpell(input - 1);
        }

    }

    private void FixedUpdate()
    {
        transform.LookAt(playerPos);
        transform.RotateAround(playerPos, Vector3.up, x * Time.deltaTime * xSpeed);
        if (!(Vector3.Distance(transform.position, playerPos) <= MinRadius && y > 0) && !(Vector3.Distance(transform.position, playerPos) >= MaxRadius && y < 0))
        {
            transform.position += transform.forward * Time.deltaTime * ySpeed * y;
        }

        
    }

    public void OnEnemyKill(int enemyHp) {
        Points += enemyHp;
    }

    public void SetActiveSpell(int index) {
        foreach (RTS_Ability ability in abilities) {
            ability.enabled = false;
        }

        abilities[index].enabled = true;
        ActiveAbility = abilities[index];
        Debug.Log($"New Active RTS Ability: {index}");
    }
}
