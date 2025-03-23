using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ClosePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public bool pauseOpen = false;
    public UISelectionManager selectionManager;

    public void Start()
    {
        selectionManager = FindFirstObjectByType<UISelectionManager>();
    }

    private void Update()
    {
        if (pauseMenu != null && pauseMenu.activeInHierarchy == true)
        {
            if (pauseOpen == true)
            {
                if(InputManager.pauseWasPressed)
                {
                    
                    CloseThePauseMenu();
                    pauseOpen = false;
                }
                
            }
            
        }
    }
    public void DisableThePauseMenu()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
        pauseMenu.transform.parent.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectionManager.inGamePauseFirstSelected) ;
    }
    public void PauseIsOpen()
    {
        pauseOpen = true;
    }
    public void CloseThePauseMenu()
    {
        pauseMenu.GetComponent<Animator>().Play("PauseMenu_Close");
        InputManager.playerInput.currentActionMap.Enable();
        
        //Debug.Log("PauseWasClosed");
    }
    
}
