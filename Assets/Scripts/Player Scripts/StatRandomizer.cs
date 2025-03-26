using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatRandomizer : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStatsV2 randomStats;
    public PlayerMovementV2 playerMovement;
    public InitiateRemixManager remixManager;
    public DeathBox deathBox;
    public GameObject darknessScreen;
    public GameObject theCamera;
    public GameObject jumpAmountIndicator;
    
    [Header("Min/Max Values")]
    public int minSpeed;
    public int maxSpeed;
    public int minJumps;
    public int maxJumps;
    public int minWallJumps;
    public int maxWallJumps;
    public int extremeWallJumpsLeft;
    public int baseSpeed;
    public int baseJumps;
    public int gravityScaleAmount;

    [Header("Current Values")]
    public int currentChanceOfRemixVariable;

    public float currentSpeed;
    public int currentJumps;
    public bool isInvertedControls;
    public bool isUpsideDown;
    public bool isDarkness;
    public bool isReverseGravity;
    public int currentWallJumpsLeft;
    public bool isReverseWallSlide;
    public bool isIncreasedGravity;

    [Header("Text Objects")]
    public TextMeshProUGUI speedTxt;
    public TextMeshProUGUI jumpsTxt;
    public TextMeshProUGUI variable01Txt;
    public TextMeshProUGUI variable02Txt;
    public TextMeshProUGUI variable03Txt;
    public TextMeshProUGUI variable04Txt;

    [Header("Possible Remixes")]
    public bool speedEnabled;
    public bool jumpsEnabled;
    public bool invertedControlsEnabled;
    public bool upsideDownEnabled;
    public bool darknessEnabled;
    public bool reverseGravityEnabled;
    public bool limitedWallJumpsEnabled;
    public bool reverseWallSlideEnabled;
    public bool increasedGravityEnabled;
    public bool timerEnabled;

    [Header("Locked Icons")]
    public GameObject lock01;
    public GameObject lock02;
    public GameObject lock03;
    public GameObject lock04;

    [Header("Difficulty Settings")]
    public bool isEasyModeEnabled;
    public bool isMediumModeEnabled;
    public bool isHardModeEnabled;
    public bool isExtremeModeEnabled = false;

    public int easyPercent;
    public int mediumPercent;
    public int hardPercent;
    public int extremePercent;

    [Header("Timer")]
    public float secondsToStart;
    public float finalRemixSecondsToStart;
    public float timeLeft;

    public void Start()
    {
        deathBox = FindFirstObjectByType<DeathBox>();
        remixManager = FindFirstObjectByType<InitiateRemixManager>();
        if (PlayerPrefs.GetString("Difficulty") == "Easy")
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
        //Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            speedEnabled = false;
            jumpsEnabled = false;
            invertedControlsEnabled = false;
            darknessEnabled = false;
            upsideDownEnabled = false;
            reverseGravityEnabled = false;
            limitedWallJumpsEnabled = false;
            reverseWallSlideEnabled = false;
            increasedGravityEnabled = false;
            timerEnabled = false;
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
        //Tutorial
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (remixManager != null)
            {
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockSpeedAndJump)
                {
                    speedEnabled = true;
                    jumpsEnabled = true;
                }
            }
        }
        //Level 1
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (remixManager != null)
            {
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockSpeedAndJump)
                {
                    speedEnabled = true;
                    jumpsEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix1)
                {
                    invertedControlsEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix2)
                {
                    darknessEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix3)
                {
                    upsideDownEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix4)
                {
                    reverseGravityEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") == remixManager.completionAmount - 1)
                {
                    ExtremeModeEnabled();
                    isExtremeModeEnabled = true;
                }
            }
        }
        //Level 2
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (remixManager != null)
            {
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockSpeedAndJump)
                {
                    speedEnabled = true;
                    jumpsEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix1)
                {
                    reverseWallSlideEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix2)
                {
                    limitedWallJumpsEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix3)
                {
                    increasedGravityEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") >= remixManager.amountToUnlockRemix4)
                {
                    timerEnabled = true;
                }
                if (PlayerPrefs.GetInt("Progress") == remixManager.completionAmount - 1)
                {
                    ExtremeModeEnabled();
                    isExtremeModeEnabled = true;
                }
            }
        }
        playerMovement.moveStats = randomStats;
        #endregion

        #region UI Functionality

        #region Speed and Jumps
        if (speedEnabled)
        {
            currentSpeed = Random.Range(minSpeed, maxSpeed + 1);
            randomStats.maxWalkSpeed = currentSpeed;
            speedTxt.text = "Speed: " + currentSpeed;
        }
        else
        {
            currentSpeed = baseSpeed;
            randomStats.maxWalkSpeed = currentSpeed;
            if (speedTxt != null)
            {
                speedTxt.text = "Speed: " + currentSpeed;
                speedTxt.color = Color.grey;
            }

        }

        if (jumpsEnabled)
        {
            currentJumps = Random.Range(minJumps, maxJumps + 1);
            randomStats.numberOfJumpsAllowed = currentJumps;
            if (jumpsTxt != null)
            {
                jumpsTxt.text = "Jumps: " + currentJumps;
            }

        }
        else
        {
            currentJumps = baseJumps;
            randomStats.numberOfJumpsAllowed = currentJumps;
            if (jumpsTxt != null)
            {
                jumpsTxt.text = "Jumps: " + currentJumps;
                jumpsTxt.color = Color.grey;
            }

        }
        #endregion

        #region Level 01
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (invertedControlsEnabled)
            {
                if (lock01 != null)
                {
                    lock01.SetActive(false);
                }
                var randomNumberInvertedControls = Random.Range(1, 100);
                if (randomNumberInvertedControls > 100 - currentChanceOfRemixVariable)
                {
                    isInvertedControls = true;
                    variable01Txt.text = ("Inverted Controls: On");
                    variable01Txt.color = Color.green;
                }
                else
                {
                    isInvertedControls = false;
                    variable01Txt.text = ("Inverted Controls: Off");
                    variable01Txt.color = Color.red;
                }
            }
            else
            {
                if (variable01Txt != null)
                {
                    variable01Txt.gameObject.SetActive(false);
                    lock01.SetActive(true);
                }

            }

            if (darknessEnabled)
            {
                lock02.SetActive(false);
                var randomNumberDarkness = Random.Range(1, 100);
                if (randomNumberDarkness > 100 - currentChanceOfRemixVariable)
                {
                    isDarkness = true;
                    darknessScreen.SetActive(true);
                    variable02Txt.text = ("Darkness: On");
                    variable02Txt.color = Color.green;
                }
                else
                {
                    isDarkness = false;
                    darknessScreen.SetActive(false);
                    variable02Txt.text = ("Darkness: Off");
                    variable02Txt.color = Color.red;
                }
            }
            else
            {
                if (variable02Txt != null)
                {
                    variable02Txt.gameObject.SetActive(false);
                    lock02.SetActive(true);
                }

            }
            if (upsideDownEnabled)
            {
                lock03.SetActive(false);
                var randomNumberUpsideDown = Random.Range(1, 100);
                if (randomNumberUpsideDown > 100 - currentChanceOfRemixVariable)
                {
                    isUpsideDown = true;
                    variable03Txt.text = ("Upside-Down Screen: On");
                    variable03Txt.color = Color.green;
                    theCamera.transform.Rotate(0, 180f, 180f);
                    jumpAmountIndicator.transform.Rotate(0, 180f, 180f);
                }
                else
                {
                    isUpsideDown = false;
                    if (variable03Txt != null)
                    {
                        variable03Txt.text = ("Upside-Down Screen: Off");
                        variable03Txt.color = Color.red;
                    }

                    theCamera.transform.Rotate(0, 0, 0);
                }
            }
            else
            {
                if (variable03Txt != null)
                {
                    variable03Txt.gameObject.SetActive(false);
                    lock03.SetActive(true);
                }

            }
            if (reverseGravityEnabled)
            {
                lock04.SetActive(false);
                var randomNumberReverseGravity = Random.Range(1, 100);
                if (randomNumberReverseGravity > 100 - currentChanceOfRemixVariable)
                {
                    isReverseGravity = true;
                    variable04Txt.text = ("Reverse Gravity: On");
                    playerMovement.GetComponent<Transform>().Rotate(0, 180f, 180f);
                    variable04Txt.color = Color.green;
                }
                else
                {
                    isReverseGravity = false;
                    if (variable04Txt != null)
                    {
                        variable04Txt.text = ("Reverse Gravity: Off");
                        variable04Txt.color = Color.red;
                    }
                    playerMovement.GetComponent<Transform>().Rotate(0, 0, 0);
                }
            }
            else
            {
                if (variable04Txt != null)
                {
                    variable04Txt.gameObject.SetActive(false);
                    lock04.SetActive(true);
                }

            }
        }
        #endregion

        #region Level 02
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (limitedWallJumpsEnabled)
            {
                playerMovement.isLimitedWallJumps = true;
                if(isExtremeModeEnabled != true)
                {
                    currentWallJumpsLeft = Random.Range(minWallJumps, maxWallJumps + 1);
                }
                else
                {
                    currentWallJumpsLeft = extremeWallJumpsLeft;
                }
                
                randomStats.limitedWallJumpAmount = currentWallJumpsLeft;
                variable02Txt.text = "Wall Jumps Left: " + currentWallJumpsLeft;
                if (lock01 != null)
                {
                    lock01.SetActive(false);
                }
            }
            else
            {

                playerMovement.isLimitedWallJumps = false;
                if (variable02Txt != null)
                {
                    variable02Txt.gameObject.SetActive(false);
                }
                if (lock01 != null)
                {
                    lock01.SetActive(true);
                }
            }
            if (reverseWallSlideEnabled)
            {
                
                if (lock02 != null)
                {
                    lock02.SetActive(false);
                }
                var randomNumberReverseWallSlide = Random.Range(1, 100);
                if (randomNumberReverseWallSlide > 100 - currentChanceOfRemixVariable)
                {
                    isReverseWallSlide = true;
                    playerMovement.isReverseWallSlide = true;
                    variable01Txt.text = "Reverse Wall Slide: ON";
                    variable01Txt.color = Color.green;
                }
                else
                {
                    playerMovement.isReverseWallSlide = false;
                    variable01Txt.text = "Reverse Wall Slide: OFF";
                    variable01Txt.color = Color.red;
                }

            }
            else
            {
                playerMovement.isReverseWallSlide = false;
                if (variable01Txt != null)
                {
                    variable01Txt.gameObject.SetActive(false);
                }
                if (lock02 != null)
                {
                    lock02.SetActive(true);
                }
            }

            if (increasedGravityEnabled)
            {
                
                if (lock03 != null)
                {
                    lock03.SetActive(false);
                }
                var randomNumberIncreasedGravity = Random.Range(1, 100);
                if (randomNumberIncreasedGravity > 100 - currentChanceOfRemixVariable)
                {
                    isIncreasedGravity = true;
                    playerMovement.GetComponent<Rigidbody2D>().gravityScale = gravityScaleAmount;
                    variable03Txt.text = "Increased Gravity: ON";
                    variable03Txt.color = Color.green;
                }
                else
                {
                    playerMovement.GetComponent<Rigidbody2D>().gravityScale = 1;
                    variable03Txt.text = "Increased Gravity: OFF";
                    variable03Txt.color = Color.red;
                }
            }
            else
            {
                playerMovement.GetComponent<Rigidbody2D>().gravityScale = 1;
                if (variable03Txt != null)
                {
                    variable03Txt.gameObject.SetActive(false);
                }
                if (lock03 != null)
                {
                    lock03.SetActive(true);
                }
            }
            if (timerEnabled)
            {
                variable04Txt.text = "Time Left: " + timeLeft;
                if (PlayerPrefs.GetInt("Progress") == remixManager.completionAmount - 1)
                {
                    timeLeft = finalRemixSecondsToStart;
                }
                else
                {
                    timeLeft = secondsToStart;
                }
                
                if (lock04 != null)
                {
                    lock04.SetActive(false);
                }
            }
            else
            {
                if (variable04Txt != null)
                {
                    variable04Txt.gameObject.SetActive(false);
                }
                if (lock04 != null)
                {
                    lock04.SetActive(true);
                }
            }
        }
        #endregion

        #endregion
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level2")
        {
            if (limitedWallJumpsEnabled)
            {
                playerMovement.isLimitedWallJumps = true;
                randomStats.limitedWallJumpAmount = currentWallJumpsLeft;
                if (currentWallJumpsLeft < 0)
                {
                    currentWallJumpsLeft = 0;
                }
                variable02Txt.text = "Wall Jumps Left: " + currentWallJumpsLeft;
                if (lock01 != null)
                {
                    lock01.SetActive(false);
                }
            }
            else
            {
                playerMovement.isLimitedWallJumps = false;
                if (jumpsTxt != null)
                {
                    jumpsTxt.text = "Jumps: " + currentJumps;

                }
                if (lock01 != null)
                {
                    lock01.SetActive(true);
                }
            }
            if (timerEnabled)
            {
                timeLeft -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(timeLeft / 60);
                int seconds = Mathf.FloorToInt(timeLeft % 60);
                int milliseconds = Mathf.FloorToInt(timeLeft * 1000) % 1000;
                string theMilliseconds = milliseconds.ToString();
                //timerText.text = string.Format("{0:00}:{1:00}:{10:00}", minutes, seconds, miliseconds);
                //timerText.text = elapsedTime.ToString(format: @"mm/:ss/:ff");
                variable04Txt.text = "Time Left: " + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + theMilliseconds.Remove(theMilliseconds.Length - 1);
                if(timeLeft <= 0)
                {
                    timeLeft = 0;
                    deathBox.Lose();
                    timerEnabled = false;
                }
            }
            
        }
    }

    public void ExtremeModeEnabled()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
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

            currentChanceOfRemixVariable = extremePercent;
        }
        if (SceneManager.GetActiveScene().name == "Level2")
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
            reverseWallSlideEnabled = false;
            increasedGravityEnabled = true;

            currentChanceOfRemixVariable = extremePercent;
        }


    }

    public void EasyModeEnabled()
    {
        currentChanceOfRemixVariable = easyPercent;
        minJumps = 3;
    }
    public void MediumModeEnabled()
    {
        currentChanceOfRemixVariable = mediumPercent;
        minJumps = 2;
    }
    public void HardModeEnabled()
    {
        currentChanceOfRemixVariable = hardPercent;
        minJumps = 2;
    }

    
}
