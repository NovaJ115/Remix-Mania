using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    
    public TextMeshProUGUI progressCounter;
    public int completionAmount;
    public void Start()
    {
        progressCounter.text = PlayerPrefs.GetInt("Progress") + "/" + completionAmount;
    }
}
