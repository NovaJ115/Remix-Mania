using UnityEngine;
using UnityEngine.UI;

public class GameSettingsMenu : MonoBehaviour
{
    public GameObject easy;
    public GameObject medium;
    public GameObject hard;
    public GameObject timerOn;
    public GameObject timerOff;
    public GameObject jumpIndicatorOn;
    public GameObject jumpIndicatorOff;

    private void Start()
    {
        if(PlayerPrefs.GetString("EasyActive") == "True")
        {
            easy.GetComponent<Image>().color = Color.white;
        }
        else
        {
            easy.GetComponent<Image>().color = Color.grey;
        }
        if (PlayerPrefs.GetString("MediumActive") == "True")
        {
            medium.GetComponent<Image>().color = Color.white;
        }
        else
        {
            medium.GetComponent<Image>().color = Color.grey;
        }
        if (PlayerPrefs.GetString("HardActive") == "True")
        {
            hard.GetComponent<Image>().color = Color.white;
        }
        else
        {
            hard.GetComponent<Image>().color = Color.grey;
        }
        if (PlayerPrefs.GetString("TimerActive") == "True")
        {
            timerOn.GetComponent<Image>().color = Color.white;
            timerOff.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            timerOn.GetComponent<Image>().color = Color.grey;
            timerOff.GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetString("EnableJumpIndicator") == "True")
        {
            jumpIndicatorOn.GetComponent<Image>().color = Color.white;
            jumpIndicatorOff.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            jumpIndicatorOn.GetComponent<Image>().color = Color.grey;
            jumpIndicatorOff.GetComponent<Image>().color = Color.white;
        }
    }
    
    public void ResetAllButtons()
    {
        easy.GetComponent<Image>().color = Color.grey;
        medium.GetComponent<Image>().color = Color.grey;
        hard.GetComponent<Image>().color = Color.grey;
        timerOn.GetComponent<Image>().color = Color.grey;
        timerOff.GetComponent<Image>().color = Color.grey;
        jumpIndicatorOn.GetComponent<Image>().color = Color.grey;
        jumpIndicatorOff.GetComponent<Image>().color = Color.grey;
    }
    public void ResetDifficultyButtons()
    {
        easy.GetComponent<Image>().color = Color.grey;
        medium.GetComponent<Image>().color = Color.grey;
        hard.GetComponent<Image>().color = Color.grey;
        PlayerPrefs.SetString("EasyActive", "False");
        PlayerPrefs.SetString("MediumActive", "False");
        PlayerPrefs.SetString("HardActive", "False");
    }
    public void ResetTimerButtons()
    {
        timerOn.GetComponent<Image>().color = Color.grey;
        timerOff.GetComponent<Image>().color = Color.grey;
    }
    public void ResetJumpIndicatorButtons()
    {
        jumpIndicatorOn.GetComponent<Image>().color = Color.grey;
        jumpIndicatorOff.GetComponent<Image>().color = Color.grey;
    }
}
