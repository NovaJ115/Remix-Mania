using UnityEngine;
using UnityEngine.InputSystem;

public class ClosePause : MonoBehaviour
{
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindFirstObjectByType<MenuManager>();
    }
    private void Update()
    {
        if (menuManager.pauseMenu != null && menuManager.pauseMenu.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                menuManager.pauseAnim.Play("PauseMenu_Close");
                InputManager.playerInput.currentActionMap.Enable();

            }
            
        }
    }
    public void DisableThePauseMenu()
    {
        menuManager.pauseMenu.SetActive(false);
    }
    
}
