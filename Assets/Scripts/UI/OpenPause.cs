using UnityEngine;

public class OpenPause : MonoBehaviour
{
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindFirstObjectByType<MenuManager>();
    }
    private void Update()
    {
        if (menuManager.pauseMenu != null && menuManager.pauseMenu.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                menuManager.pauseMenu.SetActive(true);

            }

        }
    }
}
