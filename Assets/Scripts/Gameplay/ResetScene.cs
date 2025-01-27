using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public void ResetTheScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);
        SceneManager.LoadScene(currentScene);
        
    }
}
