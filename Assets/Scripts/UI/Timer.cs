using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTime;
    public float elapsedTime;
    public float milliseconds;
    public bool gameIsOn;
    public bool enableSpeedrunTimer = false;
    
    // Update is called once per frame
    void Update()
    {
        if(enableSpeedrunTimer == true)
        {
            if (gameIsOn == true)
            {
                elapsedTime += Time.deltaTime;
            }
            if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
            {
                gameIsOn = true;
                timerText.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                gameIsOn = false;
                timerText.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            timerText.transform.parent.gameObject.SetActive(false);
        }
        
        
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        milliseconds = Mathf.FloorToInt(elapsedTime * 1000) % 1000;
        string theMilliseconds = milliseconds.ToString();
        //timerText.text = string.Format("{0:00}:{1:00}:{10:00}", minutes, seconds, miliseconds);
        //timerText.text = elapsedTime.ToString(format: @"mm/:ss/:ff");
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + theMilliseconds.Remove(theMilliseconds.Length - 1);

        
        if (SceneManager.GetActiveScene().name == "Win")
        {
            finalTime = GameObject.Find("FinalTime").GetComponent<TextMeshProUGUI>();
            if (enableSpeedrunTimer == true)
            {
                finalTime.text = "Final Time : " + timerText.text;
            }
            else
            {
                finalTime.text = "";
            }

            //Debug.Log("OnWinScreen");
        }
        if (SceneManager.GetActiveScene().name == "MainMenuV2")
        {
            elapsedTime = 0;
        }

    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        for(int i = 0; i < Object.FindObjectsByType<Timer>(FindObjectsSortMode.None).Length; i++)
        {
            if(Object.FindObjectsByType<Timer>(FindObjectsSortMode.None)[i] != this)
            {
                if (Object.FindObjectsByType<Timer>(FindObjectsSortMode.None)[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        

    }
    
}
