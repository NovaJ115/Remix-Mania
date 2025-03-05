using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public StatRandomizer statRandomizer;
    //public TextMeshProUGUI coinCounter;
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

    

    public void Start()
    {
        anim.gameObject.SetActive(true);
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
        
        coinsNeeded.text = coinAmount + " / " + amountNeeded;

        if(coinAmount >= amountNeeded)
        {
            pressRToRemix.SetActive(true);
        }
        


        
    }
    

}
