using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public StatRandomizer statRandomizer;
    public TextMeshProUGUI coinCounter;
    public TextMeshProUGUI coinsNeeded;
    public GameObject pressRToRemix;
    
    public int amountNeeded;
    public int coinAmount;
    public float minSpinSpeed;
    public float maxSpinSpeed;

    public Animator anim;

    public int easyAmountNeeded;
    public int mediumAmountNeeded;
    public int hardAmountNeeded;

    private bool cooldown = false;

    public void Start()
    {
        anim.gameObject.SetActive(true);
        cooldown = false;
    }
    public void Update()
    {
        if (statRandomizer.isEasyModeEnabled)
        {
            amountNeeded = easyAmountNeeded;
        }
        if (statRandomizer.isMediumModeEnabled)
        {
            amountNeeded = mediumAmountNeeded;
        }
        if (statRandomizer.isHardModeEnabled)
        {
            amountNeeded = hardAmountNeeded;
        }
        coinCounter.text = "Records: " + coinAmount ;
        coinsNeeded.text = " / " + amountNeeded;

        if(coinAmount >= amountNeeded)
        {
            pressRToRemix.SetActive(true);
        }
        if (pressRToRemix.activeInHierarchy == true && Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name == "Level1" && cooldown == false)
        {

            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
            if (PlayerPrefs.GetInt("Progress") == 11)
            {
                anim.SetBool("PressedRForWin", true);
            }
            else
            {
                if (PlayerPrefs.GetInt("Progress") == 10)
                {
                    anim.SetBool("PressedRForFinalRemix", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
                else
                {
                    anim.SetBool("PressedR", true);
                    int newProgress = PlayerPrefs.GetInt("Progress") + 1;
                    PlayerPrefs.SetInt("Progress", newProgress);
                }
                
            }
            
            
        }
        if (pressRToRemix.activeInHierarchy == true && Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name == "Tutorial")
        {
            anim.SetBool("PressedRInTutorial", true);
        }


        
    }
    public void ResetCooldown()
    {
        cooldown = false;
    }

}
