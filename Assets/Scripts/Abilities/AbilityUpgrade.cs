using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUpgrade : MonoBehaviour
{
    public Attack Attack;
    public int intVal;
    public float floatVal;
    public float Increase;

    public List<Attack> AttackChoices = new List<Attack>();

    [SerializeField]
    protected Button[] Buttons = new Button[3];

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (!player) return;
            for (int i = 0; i < 3; i++) {
                while (true) {
                    int r = Random.Range(0, player.attacks.Length);
                    Debug.Log("R: " + r);
                    Attack nominee = player.attacks[r];
                    if (!AttackChoices.Contains(nominee)) { 
                            AttackChoices.Add(nominee);
                        Debug.Log("Nominated " + nominee.AbilityName);
                        break;
                        }
                    }
            }
            Time.timeScale = 0f;
            Debug.Log("Time Slow!");
        

        Cursor.lockState = CursorLockMode.Confined;

        for (int i = 0; i < 3; i++) {
            Text text = Buttons[i].gameObject.transform.GetChild(0).GetComponent<Text>();
            text.text = AttackChoices[i].AbilityName;
            Buttons[i].GetComponent<ChooseAbility>().attack = AttackChoices[i];
            Buttons[i].gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }
}
