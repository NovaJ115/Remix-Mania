using UnityEngine;

public class SetProgress : MonoBehaviour
{
    public int progressAmount;
    public void ResetProgressToAmount()
    {
        PlayerPrefs.SetInt("Progress", progressAmount);
    }
    
}
