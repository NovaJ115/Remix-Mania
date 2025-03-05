using UnityEngine;
using UnityEngine.InputSystem;

public class ClosePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public bool pauseOpen = false;
    
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
