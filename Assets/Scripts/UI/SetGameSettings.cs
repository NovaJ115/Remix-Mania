using UnityEngine;

public class SetGameSettings : MonoBehaviour
{
    public string difficulty;
    public string difficultyActiveKey;
    public void SetDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        ResetDifficulty();
        PlayerPrefs.SetString(difficultyActiveKey, "True");
    }

    void ResetDifficulty()
    {
        PlayerPrefs.SetString("EasyActive", "False");
        PlayerPrefs.SetString("MediumActive", "False");
        PlayerPrefs.SetString("HardActive", "False");
    }
}
