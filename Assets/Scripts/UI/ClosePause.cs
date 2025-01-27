using UnityEngine;
using UnityEngine.InputSystem;

public class ClosePause : MonoBehaviour
{
    public GameObject pauseMenu;

    
    private void Update()
    {
        if (pauseMenu != null && pauseMenu.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {

                CloseThePauseMenu();
            }
            
        }
    }
    public void DisableThePauseMenu()
    {
        pauseMenu.transform.parent.gameObject.SetActive(false);
    }

    public void CloseThePauseMenu()
    {
        pauseMenu.GetComponent<Animator>().Play("PauseMenu_Close");
        InputManager.playerInput.currentActionMap.Enable();
    }
    
}
