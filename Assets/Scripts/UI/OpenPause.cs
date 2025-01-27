using UnityEngine;
using UnityEngine.InputSystem;

public class OpenPause : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        if(pauseMenu != null && pauseMenu.activeInHierarchy == true)
        {
            pauseMenu.transform.parent.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (pauseMenu != null && pauseMenu.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OpenThePauseMenu();
            }

        }
    }
    public void OpenThePauseMenu()
    {
        pauseMenu.transform.parent.gameObject.SetActive(true);
        pauseMenu.GetComponent<Animator>().Play("PauseMenu_Open");
        InputManager.playerInput.currentActionMap.Disable();
    }

}
