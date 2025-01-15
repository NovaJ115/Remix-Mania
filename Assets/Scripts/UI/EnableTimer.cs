using UnityEngine;

public class EnableTimer : MonoBehaviour
{
    private GameObject timer;
    void Update()
    {
        timer = GameObject.FindWithTag("Timer");
    }
    public void TurnOnTimer()
    {
        timer.GetComponent<Timer>().enableSpeedrunTimer = true;
        PlayerPrefs.SetString("TimerActive", "True");
    }
    public void TurnOffTimer()
    {
        timer.GetComponent<Timer>().enableSpeedrunTimer = false;
        PlayerPrefs.SetString("TimerActive", "False");
    }
}

