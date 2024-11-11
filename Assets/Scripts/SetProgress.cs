using UnityEngine;

public class SetProgress : MonoBehaviour
{
    public void ResetTo10()
    {
        PlayerPrefs.SetInt("Progress", 10);
    }
    public void ResetTo9()
    {
        PlayerPrefs.SetInt("Progress", 9);
    }
}
