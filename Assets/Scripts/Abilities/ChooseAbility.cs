using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAbility : MonoBehaviour
{
    public Attack attack;

    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(attack.Upgrade);

    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
        Debug.Log("Button disabled!");
    }
    // Start is called before the first frame update
}
