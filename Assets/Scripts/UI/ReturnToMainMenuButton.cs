using UnityEngine;

public class ReturnToMainMenuButton : MonoBehaviour
{
    private MenuManager menuManager;
    public void Start()
    {
        menuManager = FindFirstObjectByType<MenuManager>();
    }
    public void ReturnToMainMenu()
    {
        menuManager.ReturnToMainMenu("Main");
    }
}
