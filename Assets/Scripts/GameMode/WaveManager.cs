using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int Round = 1;

    private int BaseEnemyNum = 10;

    private float GrowthRate = 1.3f;

    private int KillCount = 0;

    private int WaveEnemies = 0;

    private int Spawned = 0;

    public bool Spawning = true;

    [SerializeField]
    SurvivalUI SurvUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WaveEnemies = BaseEnemyNum;
        SurvUI.UpdateUI();
    }

    // Update is called once per frame

    public void OnEnemyKill() {
        KillCount++;
        
        if (KillCount >= WaveEnemies) {
            NextWave();
            KillCount = 0;
            Spawned = 0;
            Spawning = true;
        }
        SurvUI.UpdateUI();
    }

    public void OnEnemySpawn() {
        Spawned++;
        if (Spawned >= WaveEnemies)
            Spawning = false;
    }

    void NextWave() {
        Round++;
        WaveEnemies = (int)(GrowthRate * WaveEnemies);
    }

    public void GetWaveData(out int oRound, out int oKilled, out int oWaveEnemies) {
        oRound = Round;
        oKilled = KillCount;
        oWaveEnemies = WaveEnemies;
    }

}
