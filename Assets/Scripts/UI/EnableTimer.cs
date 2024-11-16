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
    }
    public void TurnOffTimer()
    {
        timer.GetComponent<Timer>().enableSpeedrunTimer = false;
    }
}

