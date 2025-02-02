using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNav : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<Button> Buttons;
    InputAction Down;
    InputAction Up;
    int SelectedButton = 0;
    int NumButtons;
    void Start()
    {
        Buttons = GetComponentsInChildren<Button>(true).ToList<Button>();
        Down = InputSystem.actions.FindAction("Attack1");
        Up = InputSystem.actions.FindAction("Attack3");
        NumButtons = Buttons.Count;
        Buttons[0].Select();
    }

    private void Update()
    {
        if (Down.WasPressedThisFrame())
        {
            SelectButton(1);
            
        }
        else if (Up.WasPressedThisFrame())
        {
            SelectButton(-1);
        }
    }

    void SelectButton(int change) {
        SelectedButton += change;
        if (SelectedButton >= NumButtons) SelectedButton = 0;
        else if (SelectedButton < 0) SelectedButton = NumButtons - 1;
        Buttons[SelectedButton].Select();
    }

}
