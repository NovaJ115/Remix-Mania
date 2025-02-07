using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuCameraAnim : MonoBehaviour
{
    public Animator theCameraAnimator;
    public bool startOnMain;
    public bool startOnLevelSelect;

    public GameObject mainPos;
    public GameObject levelSelectPos;

    void Start()
    {
        if(FindAnyObjectByType<MenuManager>().startOnMainCam == true)
        {
            startOnMain = true;
        }
        else
        {
            startOnMain = false;
        }
        if(FindAnyObjectByType<MenuManager>().startOnLevelSelectCam == true)
        {
            startOnLevelSelect = true;
        }
        else
        {
            startOnLevelSelect = false;
        }
        
        if (startOnMain)
        {
            this.gameObject.transform.position = mainPos.transform.position;
        }
        if (startOnLevelSelect)
        {
            this.gameObject.transform.position = levelSelectPos.transform.position;
        }
    }
    
    public void EndScrollUp()
    {
        InputManager.playerInput.currentActionMap.Enable();
    }
    
    public void DisableAnimator()
    {
        theCameraAnimator.enabled = false;
    }
}
