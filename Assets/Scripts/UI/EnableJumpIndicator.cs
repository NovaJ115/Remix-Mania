using UnityEngine;

public class EnableJumpIndicator : MonoBehaviour
{
    public void TurnOnIndicator()
    {
        PlayerPrefs.SetString("EnableJumpIndicator", "True");
    }
    public void TurnOffIndicator()
    {
        PlayerPrefs.SetString("EnableJumpIndicator", "False");
    }
}
