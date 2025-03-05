using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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


    private bool cooldown = false;

    void Start()
    {
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
        //Level 1
        if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Level1" && cooldown == false)
        {
            
            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
            if (PlayerPrefs.GetInt("Progress") == completionAmount-1)
            {
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
}
