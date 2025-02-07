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
    public int amountToUnlockInverted;
    public int amountToUnlockDarkness;
    public int amountToUnlockUpsideDown;
    public int amountToUnlockReverseGravity;


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
        if (coinManager.pressRToRemix.activeInHierarchy == true && InputManager.remixWasPressed && SceneManager.GetActiveScene().name == "Tutorial")
        {
            coinManager.anim.SetBool("PressedRInTutorial", true);
        }
    }
    public void ResetCooldown()
    {
        cooldown = false;
    }
}
