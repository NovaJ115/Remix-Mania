using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
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
        Debug.Log("Did Function");
        theMenuManager.ReturnToMainMenu(theMainMenuSection);
        Debug.Log("Finished Function");
    }
}
