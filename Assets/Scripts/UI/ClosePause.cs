using UnityEngine;
using UnityEngine.InputSystem;

public class ClosePause : MonoBehaviour
{
    public GameObject pauseMenu;
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
