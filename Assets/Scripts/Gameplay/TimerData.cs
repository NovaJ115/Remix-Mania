using UnityEngine;

[System.Serializable]
public class TimerData
{

    public float lvl1HighScore;
    public float lvl2HighScore;

    public TimerData(InitiateRemixManager remixManager)
    {
        lvl1HighScore = remixManager.lvl1HighScoreTime;
        lvl2HighScore = remixManager.lvl2HighScoreTime;
    }
}
