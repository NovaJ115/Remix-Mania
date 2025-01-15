using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuCameraAnim : MonoBehaviour
{
    void Start()
    {
        InputManager.playerInput.currentActionMap.Disable();
        
    }

    public void EndScrollUp()
    {
        InputManager.playerInput.currentActionMap.Enable();
    }
    
}
