using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public string currentScene;
    public AudioSource remixSound;
    public GameObject music;
    public GameObject finalRemixText;
    public MenuManager menuManager;
    public void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        music = GameObject.FindWithTag("Music");
    }
    public void RemixTheScene()
    {
        
        Debug.Log(currentScene);
        SceneManager.LoadScene(currentScene);
    }

    public void PlayRemixSound()
    {
        if(music != null)
        {
            music.GetComponent<MusicRandomizer>().StopMusic();
        }
        
        remixSound.Play();
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToWinScreen()
    {
        SceneManager.LoadScene("Win");
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ShowFinalRemixText()
    {
        if(finalRemixText != null)
        {
            finalRemixText.SetActive(true);
        }
    }
    public void GoToEasyLevel()
    {
        menuManager.PlayLevel1Easy();
    }
    public void GoToMediumLevel()
    {
        menuManager.PlayLevel1Medium();
    }
    public void GoToHardLevel()
    {
        menuManager.PlayLevel1Hard();
    }
}
