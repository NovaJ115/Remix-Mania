using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    private string currentScene;
    public GameObject pauseMenu;
    

    public void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
    }
    public void PlayLevel1Easy()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetString("Difficulty", "Easy");
        ResetRemixesCompleted();
    }
    public void PlayLevel1Medium()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetString("Difficulty", "Medium");
        ResetRemixesCompleted();
    }
    public void PlayLevel1Hard()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetString("Difficulty", "Hard");
        ResetRemixesCompleted();
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ResetRemixesCompleted()
    {
        PlayerPrefs.SetInt("Progress", 0);
        //SceneManager.LoadScene("Level1");

    }
    private void Update()
    {
        if(pauseMenu != null)
        {
            if (pauseMenu.activeInHierarchy == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        if (pauseMenu != null && pauseMenu.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pauseMenu.SetActive(true);
                Debug.Log("ActionHappened");
            }

        }
        /*if (pauseMenu != null && pauseMenu.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(false);
            }

        }*/

    }
}
