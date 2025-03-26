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

    }

    public void Update()
    {
        
        if (isNearAltar && InputManager.interactWasPressed)
        {
            selectionManager.OpenBestTimesMenu();
        }
        //Stage 1 Time
        int minutes = Mathf.FloorToInt(theTimerData.GetFloat("Level_01_Time") / 60);
        int seconds = Mathf.FloorToInt(theTimerData.GetFloat("Level_01_Time") % 60);
        int milliseconds = Mathf.FloorToInt(theTimerData.GetFloat("Level_01_Time") * 1000) % 1000;
        string theMilliseconds = milliseconds.ToString();
        string lvl1FinalTime = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + theMilliseconds.Remove(theMilliseconds.Length - 1);

        //Stage 2 Time
        int minutes2 = Mathf.FloorToInt(theTimerData.GetFloat("Level_02_Time") / 60);
        int seconds2 = Mathf.FloorToInt(theTimerData.GetFloat("Level_02_Time") % 60);
        int milliseconds2 = Mathf.FloorToInt(theTimerData.GetFloat("Level_02_Time") * 1000) % 1000;
        string theMilliseconds2 = milliseconds2.ToString();
        string lvl2FinalTime = minutes2.ToString("00") + ":" + seconds2.ToString("00") + "." + theMilliseconds2.Remove(theMilliseconds2.Length - 1);

        if (theTimerData.Load())
        {
            lvl1BestTime.text = "Stage 1 : " + lvl1FinalTime;
            Debug.Log("Lvl 1 time : " + theTimerData.GetFloat("Level_01_Time"));
            lvl2BestTime.text = "Stage 2 : " + lvl2FinalTime;
            Debug.Log("Lvl 2 time : " + theTimerData.GetFloat("Level_02_Time"));
        }
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

}
