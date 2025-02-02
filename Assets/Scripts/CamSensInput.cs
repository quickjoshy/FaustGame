using System;
using UnityEngine;
using UnityEngine.UI;

public class CamSensInput : MonoBehaviour
{
    Settings Settings;
    InputField Input;
    void Start()
    {
        Settings = FindFirstObjectByType<Settings>();
        Input = GetComponentInChildren<InputField>();
        Input.text = Settings.CameraSensitivity.ToString("F2");
    }

    public void SetSens() {
        float sensVal;
        try
        {
            sensVal = float.Parse(Input.text);
        }
        catch(Exception e) {
            sensVal = 1f;
            Debug.Log(e.Message);
        }
        Settings.CameraSensitivity = sensVal;
    }
}
