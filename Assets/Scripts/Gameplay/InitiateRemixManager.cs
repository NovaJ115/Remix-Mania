using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TigerForge;

public class InitiateRemixManager : MonoBehaviour
{

    private CoinManager coinManager;
    
    public TextMeshProUGUI progressCounter;
    [Header("Variables")]
    public int completionAmount;
    public int amountToUnlockSpeedAndJump;
    public int amountToUnlockRemix1;
    public int amountToUnlockRemix2;
    public int amountToUnlockRemix3;
    public int amountToUnlockRemix4;

    public Timer timer;
    public float lvl1HighScoreTime;
    public float lvl2HighScoreTime;

    [Header("Lvl 2 Variables")]
    [SerializeField] private GameObject wallJumpText;

    private bool cooldown = false;
    private string theFinalTime;

    EasyFileSave theTimerData;


    void Start()
    {
        theTimerData = new EasyFileSave("timer_data");
        timer = FindFirstObjectByType<Timer>();
        coinManager = FindFirstObjectByType<CoinManager>();
        if(progressCounter != null)
        {
            progressCounter.text = PlayerPrefs.GetInt("Progress") + "/" + completionAmount;
        }
        cooldown = false;
        Debug.Log("Progress : " + PlayerPrefs.GetInt("Progress"));
    }
    void Update()
    {
        
        timer = FindFirstObjectByType<Timer>();
        //Level 1
        if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Level1" && cooldown == false)
        {
            
            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
            if (PlayerPrefs.GetInt("Progress") == completionAmount-1)
            {
                lvl1HighScoreTime = timer.elapsedTime;

                theTimerData.Add("Level_01_Time", lvl1HighScoreTime);
                theTimerData.Save();
                /*
                
                TimerData data = SaveSystem.LoadTimer();
                float highScore = data.lvl1HighScore;
                if (lvl1HighScoreTime < highScore)
                {
                    SaveSystem.SaveTimer(this);
                }
                WriteOutTime();
                */
                PlayerPrefs.SetString("Lvl1BestTime", theFinalTime);
                coinManager.anim.SetBool("PressedRForWin", true);
            }
            else
            {
                if (PlayerPrefs.GetInt("Progress") == completionAmount-2)
                {
                    coinManager.anim.SetBool("PressedRForFinalRemix", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
                else
                {
                    coinManager.anim.SetBool("PressedR", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
            }
        }
        //Tutorial
        if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Tutorial")
        {
            coinManager.anim.SetBool("PressedRInTutorial", true);
        }
        //Level 2
        if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Level2" && cooldown == false)
        {

            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
            if (PlayerPrefs.GetInt("Progress") == completionAmount - 1)
            {
                /*
                lvl2HighScoreTime = timer.elapsedTime;
                TimerData data = SaveSystem.LoadTimer();
                float highScore2 = data.lvl2HighScore;
                if (lvl2HighScoreTime < highScore2)
                {
                    SaveSystem.SaveTimer(this);
                }else if (highScore2 == 0)
                {
                    SaveSystem.SaveTimer(this);
                }
                WriteOutTime();
                PlayerPrefs.SetString("Lvl2BestTime", theFinalTime);
                */
                coinManager.anim.SetBool("PressedRForWin", true);
            }
            else
            {
                if (PlayerPrefs.GetInt("Progress") == completionAmount - 2)
                {
                    coinManager.anim.SetBool("PressedRForFinalRemix", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
                else
                {
                    coinManager.anim.SetBool("PressedR", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
            }

        }
        if(wallJumpText != null)
        {
            if (PlayerPrefs.GetInt("Progress") == 0)
            {
                wallJumpText.SetActive(true);
            }
            else
            {
                wallJumpText.SetActive(false);
            }
        }
        
        //Level 3
        if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Level3" && cooldown == false)
        {
            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
            coinManager.anim.SetBool("PressedR", true);
            int newProgress = PlayerPrefs.GetInt("Progress") + 1;
            PlayerPrefs.SetInt("Progress", newProgress);
        }
    }
    public void ResetCooldown()
    {
        cooldown = false;
    }

    public void WriteOutTime()
    {
        int minutes = Mathf.FloorToInt(timer.elapsedTime / 60);
        int seconds = Mathf.FloorToInt(timer.elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt(timer.elapsedTime * 1000) % 1000;
        string theMilliseconds = milliseconds.ToString();
        theFinalTime = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + theMilliseconds.Remove(theMilliseconds.Length - 1);
    }

}
