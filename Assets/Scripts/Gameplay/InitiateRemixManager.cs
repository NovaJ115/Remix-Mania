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

    [Header("Tutorial Variables")]
    [SerializeField] private GameObject textSet1;
    [SerializeField] private GameObject textSet2;

    [Header("Lvl 2 Variables")]
    [SerializeField] private GameObject wallJumpText;

    private bool cooldown = false;

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
        if (theTimerData.Load())
        {
            Debug.Log("With GetFloat : " + theTimerData.GetFloat("Level_01_Time"));
            Debug.Log("Without GetFloat : " + theTimerData.Load("Level_01_Time"));
            timer = FindFirstObjectByType<Timer>();
            //Level 1
            if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Level1" && cooldown == false)
            {

                Invoke("ResetCooldown", 5.0f);
                cooldown = true;
                if (PlayerPrefs.GetInt("Progress") == completionAmount - 1)
                {

                    lvl1HighScoreTime = timer.elapsedTime;
                    if (theTimerData.GetFloat("Level_01_Time") == 0)
                    {
                        theTimerData.Add("Level_01_Time", lvl1HighScoreTime);
                        theTimerData.Append();
                        Debug.Log("Got First New Time");
                    }
                    else if (lvl1HighScoreTime < theTimerData.GetFloat("Level_01_Time"))
                    {
                        theTimerData.Add("Level_01_Time", lvl1HighScoreTime);
                        theTimerData.Append();
                        Debug.Log("Got New Best Time");
                    }
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
            //Tutorial
            if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Tutorial" && cooldown == false)
            {
                Invoke("ResetCooldown", 5.0f);
                cooldown = true;
                if (PlayerPrefs.GetInt("Progress") == completionAmount - 2)
                {
                    coinManager.anim.SetBool("PressedR", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
                if (PlayerPrefs.GetInt("Progress") == completionAmount - 1)
                {
                    coinManager.anim.SetBool("PressedRInTutorial", true);
                }

            }
            if (textSet1 != null && textSet2 != null)
            {
                if (PlayerPrefs.GetInt("Progress") == 0)
                {
                    textSet1.SetActive(true);
                    textSet2.SetActive(false);
                }
                else
                {
                    textSet1.SetActive(false);
                    textSet2.SetActive(true);
                }
            }
            //Level 2
            if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Level2" && cooldown == false)
            {

                Invoke("ResetCooldown", 5.0f);
                cooldown = true;
                if (PlayerPrefs.GetInt("Progress") == completionAmount - 1)
                {
                    lvl2HighScoreTime = timer.elapsedTime;
                    if (theTimerData.GetFloat("Level_02_Time") == 0)
                    {
                        theTimerData.Add("Level_02_Time", lvl2HighScoreTime);
                        theTimerData.Append();
                        Debug.Log("Got First New Time");
                    }
                    else if (lvl2HighScoreTime < theTimerData.GetFloat("Level_02_Time"))
                    {
                        theTimerData.Add("Level_02_Time", lvl2HighScoreTime);
                        theTimerData.Append();
                        Debug.Log("Got New Best Time");
                    }
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
            if (wallJumpText != null)
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
        
    }
    public void ResetCooldown()
    {
        cooldown = false;
    }


}
