using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private MenuManager theMenuManager;

    public void Update()
    {
        theMenuManager = FindAnyObjectByType<MenuManager>();
    }
    public void RestartLevel()
    {
        theMenuManager.RestartLevel();
    }
    public void GoToMainMenu(string theMainMenuSection)
    {
        theMenuManager.ReturnToMainMenu(theMainMenuSection);
    }
}
