using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    private string currentScene;
    public Animator fadeToBlackAnim;

    public bool startOnMainCam;
    public bool startOnLevelSelectCam;

    public GameObject mainMenuCamera;

    

    public void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        for (int i = 0; i < Object.FindObjectsByType<MenuManager>(FindObjectsSortMode.None).Length; i++)
        {
            if (Object.FindObjectsByType<MenuManager>(FindObjectsSortMode.None)[i] != this)
            {
                if (Object.FindObjectsByType<MenuManager>(FindObjectsSortMode.None)[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(this);
        if (SceneManager.GetActiveScene().name == "ShopTest" && fadeToBlackAnim != null)
        {
            fadeToBlackAnim.Play("FadeFromBlackScreen");
        }
        mainMenuCamera = GameObject.FindWithTag("MainCamera");
        
        if (mainMenuCamera != null && mainMenuCamera.GetComponent<MainMenuCameraAnim>() != null)
        {
            mainMenuCamera.GetComponent<MainMenuCameraAnim>().startOnMain = startOnMainCam;
            mainMenuCamera.GetComponent<MainMenuCameraAnim>().startOnMain = startOnLevelSelectCam;
            
            if (PlayerPrefs.GetString("MainMenuSection") == "Main")
            {
                mainMenuCamera.transform.position = mainMenuCamera.GetComponent<MainMenuCameraAnim>().mainPos.transform.position;
                Debug.Log("StartOnMain");
                InputManager.playerInput.currentActionMap.Disable();
            }
            if (PlayerPrefs.GetString("MainMenuSection") == "LevelSelect")
            {
                mainMenuCamera.GetComponent<Animator>().enabled = false;
                mainMenuCamera.transform.position = mainMenuCamera.GetComponent<MainMenuCameraAnim>().levelSelectPos.transform.position;
                Debug.Log("StartOnLevelSelect");
                InputManager.playerInput.currentActionMap.Enable();
            }
        }
        
    }
    public void ReturnToMainMenu(string mainMenuSection)
    {
        Time.timeScale = 1;
        if (mainMenuSection == "Main")
        {
            PlayerPrefs.SetString("MainMenuSection", "Main");
            SceneManager.LoadScene("MainMenuV2");
        }
        if (mainMenuSection == "LevelSelect")
        {
            PlayerPrefs.SetString("MainMenuSection", "LevelSelect");
            SceneManager.LoadScene("MainMenuV2");
        }
        
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

    

    public void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(InputManager.playerInput.currentActionMap.enabled);
    }

}
