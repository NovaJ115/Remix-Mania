using UnityEngine;

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
                menuManager.pauseMenu.SetActive(false);

            }

        }
    }
}
