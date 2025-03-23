using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISelectionManager : MonoBehaviour
{
    [Header("Main Menu Variables")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameSettingsCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject bestTimesMenu;

    [Header("In Game Variables")]
    [SerializeField] private GameObject inGameSettingsPanel;
    [SerializeField] private GameObject inGamePausePanel;

    [Header("FirstSelectedOptions")]
    [SerializeField] private GameObject mainMenuFirstSelected;
    [SerializeField] private GameObject gameSettingsFirstSelected;
    [SerializeField] private GameObject settingsFirstSelected;
    [SerializeField] private GameObject creditsFirstSelected;
    [SerializeField] public GameObject inGamePauseFirstSelected;
    [SerializeField] private GameObject inGameSettingsFirstSelected;
    [SerializeField] private GameObject bestTimesMenuFirstSelected;

    

    public void Start()
    {
        if(PlayerPrefs.GetString("MainMenuSection") == "Main")
        {
            if (SceneManager.GetActiveScene().name == "MainMenuV2")
            {
                EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
            }
        }
        
        if (SceneManager.GetActiveScene().name != "MainMenuV2")
        {
            EventSystem.current.SetSelectedGameObject(inGamePauseFirstSelected);
        }
    }

    public void OpenMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameSettingsCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }
    public void OpenGameSettingsMenu()
    {
        mainMenuCanvas.SetActive(false);
        gameSettingsCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(gameSettingsFirstSelected);
    }
    public void OpenSettingsMenu()
    {
        mainMenuCanvas.SetActive(false);
        gameSettingsCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        creditsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(settingsFirstSelected);
    }
    public void OpenCreditsMenu()
    {
        mainMenuCanvas.SetActive(false);
        gameSettingsCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(true);

        EventSystem.current.SetSelectedGameObject(creditsFirstSelected);
    }

   public void CloseInGameSettingsMenu()
    {
        inGameSettingsPanel.SetActive(false);
        inGamePausePanel.SetActive(true);       
        EventSystem.current.SetSelectedGameObject(inGamePauseFirstSelected);
    }

    public void OpenInGameSettingsMenu()
    {
        inGameSettingsPanel.SetActive(true);
        inGamePausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(inGameSettingsFirstSelected);
    }

    public void OpenBestTimesMenu()
    {
        bestTimesMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(bestTimesMenuFirstSelected);
        InputManager.playerInput.currentActionMap.Disable();
    }

    public void CloseBestTimesMenu()
    {
        bestTimesMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        InputManager.playerInput.currentActionMap.Enable();
    }

}
