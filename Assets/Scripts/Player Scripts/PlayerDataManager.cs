using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerMovementV2 playerMovement;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerMovement);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerMovement.timesJumped = data.timesJumped;
    }

    public void Start()
    {
        LoadPlayer();
    }

    public void Update()
    {
        SavePlayer();
    }

}
