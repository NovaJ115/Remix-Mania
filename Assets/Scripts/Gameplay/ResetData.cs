using UnityEngine;
using TigerForge;

public class ResetData : MonoBehaviour
{
    private PlayerMovementV2 playerMovement;
    private InitiateRemixManager remixManager;

    EasyFileSave theTimerData;

    public void Start()
    {
        theTimerData = new EasyFileSave("timer_data");
        playerMovement = FindFirstObjectByType<PlayerMovementV2>();
    }
    public void ResetTheData()
    {
        playerMovement.timesJumped = 0;
        theTimerData.Add("Level_01_Time", 0);
        theTimerData.Save();
    }

}
