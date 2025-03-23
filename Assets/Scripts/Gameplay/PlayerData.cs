using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    
    public int timesJumped;

    public PlayerData(PlayerMovementV2 player)
    {
        timesJumped = player.timesJumped;
    }
    
}
