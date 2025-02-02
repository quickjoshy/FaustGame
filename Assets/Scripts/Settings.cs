using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float CameraSensitivity = 1;


    private void Start()
    {
        if(FindObjectsByType<Settings>(FindObjectsSortMode.None).Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

}
