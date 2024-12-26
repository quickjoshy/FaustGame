using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int minutes = 0;
    int seconds = 0;

    Text Text;
    float timer = 0;

    private void Start()
    {
        Text = GetComponent<Text>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        minutes = (int)timer / 60;
        seconds = (int)timer % 60;
        Text.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
