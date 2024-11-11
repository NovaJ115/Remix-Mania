using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatRandomizer : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStats randomStats;
    public PlayerMovement playerMovement;
    public GameObject darknessScreen;
    public GameObject theCamera;
    
    [Header("Min/Max Values")]
    public int minSpeed;
    public int maxSpeed;
    public int minJumps;
    public int maxJumps;
    public int baseSpeed;
    public int baseJumps;

    public int chanceOfInvertedControls;
    public int chanceOfDarkness;
    public int chanceOfUpsideDown;
    public int chanceOfReverseGravity;

    [Header("Current Values")]
    public float currentSpeed;
    public int currentJumps;
    public bool isInvertedControls;
    public bool isUpsideDown;
    public bool isDarkness;
    public bool isReverseGravity;
    
    [Header("Text Objects")]
    public TextMeshProUGUI speedTxt;
    public TextMeshProUGUI jumpsTxt;
    public TextMeshProUGUI invertedControlsTxt;
    public TextMeshProUGUI darknessTxt;
    public TextMeshProUGUI upsideDownTxt;
    public TextMeshProUGUI reverseGravityTxt;

    [Header("Possible Remixes")]
    public bool speedEnabled;
    public bool jumpsEnabled;
    public bool invertedControlsEnabled;
    public bool upsideDownEnabled;
    public bool darknessEnabled;
    public bool reverseGravityEnabled;
    
    [Header("Locked Icons")]
    public GameObject invertedControlsLock;
    public GameObject darknessLock;
    public GameObject upsideDownLock;
    public GameObject reverseGravityLock;

    [Header("Difficulty Settings")]
    public bool isEasyModeEnabled;
    public bool isMediumModeEnabled;
    public bool isHardModeEnabled;
    public bool isExtremeModeEnabled = false;

    public int easyPercent;
    public int mediumPercent;
    public int hardPercent;
    public int extremePercent;

    public void Start()
    {
        if(PlayerPrefs.GetString("Difficulty") == "Easy")
        {
            isEasyModeEnabled = true;
            isMediumModeEnabled = false;
            isHardModeEnabled = false;
        }
        if (PlayerPrefs.GetString("Difficulty") == "Medium")
        {
            isMediumModeEnabled = true;
            isEasyModeEnabled = false;
            isHardModeEnabled = false;
        }
        if (PlayerPrefs.GetString("Difficulty") == "Hard")
        {
            isHardModeEnabled = true;
            isEasyModeEnabled = false;
            isMediumModeEnabled = false;
        }
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            speedEnabled = false;
            jumpsEnabled = false;
            invertedControlsEnabled = false;
            darknessEnabled = false;
            upsideDownEnabled = false;
            reverseGravityEnabled = false;
            isEasyModeEnabled = false;
            isMediumModeEnabled = false;
            isHardModeEnabled = false;
            isExtremeModeEnabled = false;
        }
        if(isEasyModeEnabled == true)
        {
            EasyModeEnabled();
        }
        if (isMediumModeEnabled == true)
        {
            MediumModeEnabled();
        }
        if (isHardModeEnabled == true)
        {
            HardModeEnabled();
        }
        if(isExtremeModeEnabled == true)
        {
            ExtremeModeEnabled();
        }
        #region SetProgressionBools
        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            if (PlayerPrefs.GetInt("Progress") >= 1)
            {
                speedEnabled = true;
                jumpsEnabled = true;
            }
            if (PlayerPrefs.GetInt("Progress") >= 2)
            {
                invertedControlsEnabled = true;
            }
            if (PlayerPrefs.GetInt("Progress") >= 4)
            {
                darknessEnabled = true;
            }
            if (PlayerPrefs.GetInt("Progress") >= 6)
            {
                upsideDownEnabled = true;
            }
            if (PlayerPrefs.GetInt("Progress") >= 8)
            {
                reverseGravityEnabled = true;
            }
            if (PlayerPrefs.GetInt("Progress") == 11)
            {
                ExtremeModeEnabled();
                isExtremeModeEnabled = true;
            }
        }
        
        #endregion

        #region UI Functionality
        if (invertedControlsEnabled)
        {
            invertedControlsLock.SetActive(false);
            var randomNumberInvertedControls = Random.Range(1, 100);
            if (randomNumberInvertedControls > 100 - chanceOfInvertedControls)
            {
                isInvertedControls = true;
                invertedControlsTxt.text = ("Inverted Controls: On");
                invertedControlsTxt.color = Color.green;
            }
            else
            {
                isInvertedControls = false;
                invertedControlsTxt.text = ("Inverted Controls: Off");
                invertedControlsTxt.color = Color.red;
            }
        }
        else
        {
            invertedControlsTxt.gameObject.SetActive(false);
            invertedControlsLock.SetActive(true);
        }

        if (darknessEnabled)
        {
            darknessLock.SetActive(false);
            var randomNumberDarkness = Random.Range(1, 100);
            if (randomNumberDarkness > 100 - chanceOfDarkness)
            {
                isDarkness = true;
                darknessScreen.SetActive(true);
                darknessTxt.text = ("Darkness: On");
                darknessTxt.color = Color.green;
            }
            else
            {
                isDarkness = false;
                darknessScreen.SetActive(false);
                darknessTxt.text = ("Darkness: Off");
                darknessTxt.color = Color.red;
            }
        }
        else
        {
            darknessTxt.gameObject.SetActive(false);
            darknessLock.SetActive(true);
        }
        if (upsideDownEnabled)
        {
            upsideDownLock.SetActive(false);
            var randomNumberUpsideDown = Random.Range(1, 100);
            if (randomNumberUpsideDown > 100 - chanceOfUpsideDown)
            {
                isUpsideDown = true;
                upsideDownTxt.text = ("Upside-Down Screen: On");
                upsideDownTxt.color = Color.green;
                theCamera.transform.Rotate(0, 180f, 180f);
            }
            else
            {
                isUpsideDown = false;
                upsideDownTxt.text = ("Upside-Down Screen: Off");
                upsideDownTxt.color = Color.red;
                theCamera.transform.Rotate(0, 0, 0);
            }
        }
        else
        {
            upsideDownTxt.gameObject.SetActive(false);
            upsideDownLock.SetActive(true);
        }
        if (reverseGravityEnabled)
        {
            reverseGravityLock.SetActive(false);
            var randomNumberReverseGravity = Random.Range(1, 100);
            if (randomNumberReverseGravity > 100 - chanceOfReverseGravity)
            {
                isReverseGravity = true;
                reverseGravityTxt.text = ("Reverse Gravity: On");
                playerMovement.GetComponent<Transform>().Rotate(0, 180f, 180f);
                reverseGravityTxt.color = Color.green;
            }
            else
            {
                isReverseGravity = false;
                reverseGravityTxt.text = ("Reverse Gravity: Off");
                playerMovement.GetComponent<Transform>().Rotate(0, 0, 0);
                reverseGravityTxt.color = Color.red;
            }
        }
        else
        {
            reverseGravityTxt.gameObject.SetActive(false);
            reverseGravityLock.SetActive(true);
        }


        if (speedEnabled)
        {
            currentSpeed = Random.Range(minSpeed, maxSpeed);
            randomStats.maxWalkSpeed = currentSpeed;
            speedTxt.text = "Speed: " + currentSpeed;
        }
        else
        {
            currentSpeed = baseSpeed;
            randomStats.maxWalkSpeed = currentSpeed;
            speedTxt.text = "Speed: " + currentSpeed;
            speedTxt.color = Color.grey;
        }

        if (jumpsEnabled)
        {
            currentJumps = Random.Range(minJumps, maxJumps);
            randomStats.numberOfJumpsAllowed = currentJumps;
            jumpsTxt.text = "Jumps: " + currentJumps;
        }
        else
        {
            currentJumps = baseJumps;
            randomStats.numberOfJumpsAllowed = currentJumps;
            jumpsTxt.text = "Jumps: " + currentJumps;
            jumpsTxt.color = Color.grey;
        }
        
        playerMovement.moveStats = randomStats;
        #endregion
    }

    public void ExtremeModeEnabled()
    {
        if (isEasyModeEnabled)
        {
            minJumps = 3;
            maxJumps = 3;
        }
        else
        {
            jumpsEnabled = false;
        }
        speedEnabled = false;
        
        invertedControlsEnabled = true;
        darknessEnabled = true;
        upsideDownEnabled = true;
        reverseGravityEnabled = true;

        chanceOfInvertedControls = extremePercent;
        chanceOfDarkness = extremePercent;
        chanceOfUpsideDown = extremePercent;
        chanceOfReverseGravity = extremePercent;
    }

    public void EasyModeEnabled()
    {
        chanceOfInvertedControls = easyPercent;
        chanceOfDarkness = easyPercent;
        chanceOfUpsideDown = easyPercent;
        chanceOfReverseGravity = easyPercent;
        minJumps = 3;
    }
    public void MediumModeEnabled()
    {
        chanceOfInvertedControls = mediumPercent;
        chanceOfDarkness = mediumPercent;
        chanceOfUpsideDown = mediumPercent;
        chanceOfReverseGravity = mediumPercent;
        minJumps = 2;
    }
    public void HardModeEnabled()
    {
        chanceOfInvertedControls = hardPercent;
        chanceOfDarkness = hardPercent;
        chanceOfUpsideDown = hardPercent;
        chanceOfReverseGravity = hardPercent;
        minJumps = 2;
    }


}
