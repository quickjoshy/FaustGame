using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUI : MonoBehaviour
{
    /*
    int minutes = 0;
    int seconds = 0;
    float timer = 0;
    */
    [SerializeField]
    WaveManager WaveManager;

    int RoundNum = 1;
    int KilledEnemies = 0;
    int WaveEnemies = 0;
    int RemainingEnemies;

    Text Text;
    

    private void Start()
    {
        Text = GetComponent<Text>();
    }

    public void UpdateUI() {
        WaveManager.GetWaveData(out RoundNum, out KilledEnemies, out WaveEnemies);
        RemainingEnemies = WaveEnemies - KilledEnemies;
        Text.text = string.Format("Wave: {0}\n {1}/{2}", RoundNum, RemainingEnemies,WaveEnemies);
    }
}
