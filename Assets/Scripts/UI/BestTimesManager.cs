using UnityEngine;
using TMPro;
using TigerForge;

public class BestTimesManager : MonoBehaviour
{

    [SerializeField] private GameObject interactText;
    public TextMeshProUGUI lvl1BestTime;
    public TextMeshProUGUI lvl2BestTime;
    public TextMeshProUGUI timesJumped;
    public float lvl1BestTimeNumber;
    public float lvl2BestTimeNumber;
    private bool isNearAltar;
    private UISelectionManager selectionManager;
    private PlayerMovementV2 playerMovement;

    EasyFileSave theTimerData;

    public void Start()
    {
        theTimerData = new EasyFileSave("timer_data");
        isNearAltar = false;
        interactText.SetActive(false);
        selectionManager = FindFirstObjectByType<UISelectionManager>();
        playerMovement = FindFirstObjectByType<PlayerMovementV2>();
        //TimerData data = SaveSystem.LoadTimer();
        //lvl1BestTimeNumber = data.lvl1HighScore;
        //lvl2BestTimeNumber = data.lvl2HighScore;
        //Debug.Log(lvl1BestTimeNumber);

    }

    public void Update()
    {
        if(isNearAltar && InputManager.interactWasPressed)
        {
            selectionManager.OpenBestTimesMenu();
        }
        if (theTimerData.Load())
        {
            lvl1BestTime.text = "Stage 1 : " + theTimerData.GetFloat("Level_01_Time").ToString();
            Debug.Log(theTimerData.GetFloat("Level_01_Time"));
        }
        
        /*
        int minutes = Mathf.FloorToInt(lvl1BestTimeNumber / 60);
        int seconds = Mathf.FloorToInt(lvl1BestTimeNumber % 60);
        int milliseconds = Mathf.FloorToInt(lvl1BestTimeNumber * 1000) % 1000;
        string theMilliseconds = milliseconds.ToString();
        string lvl1FinalTime = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + theMilliseconds.Remove(theMilliseconds.Length - 1);
        if(lvl2BestTimeNumber != 0)
        {
            int minutes2 = Mathf.FloorToInt(lvl2BestTimeNumber / 60);
            int seconds2 = Mathf.FloorToInt(lvl2BestTimeNumber % 60);
            int milliseconds2 = Mathf.FloorToInt(lvl2BestTimeNumber * 1000) % 1000;
            string theMilliseconds2 = milliseconds2.ToString();
            string lvl2FinalTime = minutes2.ToString("00") + ":" + seconds2.ToString("00") + "." + theMilliseconds2.Remove(theMilliseconds.Length - 1);
            lvl2BestTime.text = "Stage 2 : " + lvl2FinalTime;
        }
        
        lvl1BestTime.text = "Stage 1 : " + lvl1FinalTime;
        */
        timesJumped.text = "Times Jumped : " + playerMovement.timesJumped;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Body")
        {
            interactText.SetActive(true);
            isNearAltar = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Body")
        {
            interactText.SetActive(false);
            isNearAltar = false;
        }
    }
    
    /*public void WriteOutTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt(time * 1000) % 1000;
        string theMilliseconds = milliseconds.ToString();
        string theFinalTime = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + theMilliseconds.Remove(theMilliseconds.Length - 1);
    }*/

}
