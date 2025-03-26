using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class DoorManager : MonoBehaviour
{
    public GameObject textAboveDoor;
    public Animator fadeToBlack;
    public string sceneToTransitionTo;

    private bool isInDoor;
    private PlayerInput playerInput;
    void Start()
    {
        textAboveDoor.SetActive(false);
        playerInput = FindFirstObjectByType<PlayerInput>();
    }

    void Update()
    {
        if(isInDoor && InputManager.interactWasPressed)
        {
            if(sceneToTransitionTo == "MainMenuV2")
            {
                PlayerPrefs.SetString("MainMenuSection", "Main");
            }
            fadeToBlack.gameObject.SetActive(true);
            fadeToBlack.GetComponent<EnterBuilding>().theSceneName = sceneToTransitionTo;
            PlayerPrefs.SetInt("Progress", 0);
            fadeToBlack.Play("FadeToBlackScreen");
            Debug.Log("Player Hit F");
            this.gameObject.SetActive(false);
            InputSystem.DisableAllEnabledActions();
        }
        if (playerInput.currentControlScheme == "Keyboard")
        {
            textAboveDoor.GetComponent<TextMeshProUGUI>().text = "Press <sprite name=f>";
        }
        if (playerInput.currentControlScheme == "Gamepad")
        {
            textAboveDoor.GetComponent<TextMeshProUGUI>().text = "Press <sprite name=fw>";
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Body")
        {
            textAboveDoor.SetActive(true);
            isInDoor = true;
            //Debug.Log("Collided With Player");
        }
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Body")
        {
            textAboveDoor.SetActive(false);
            isInDoor = false;
            //Debug.Log("Collided With Player");
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        /*if(other.gameObject.tag == "Body")
        {
            Debug.Log("Collided With Player");
            if (InputManager.interactWasPressed)
            {
                Debug.Log("Player Hit F");
            }
        }*/
        
        /*if (other.gameObject.tag == "Body" && InputManager.interactWasPressed)
        {
            
            fadeToBlack.GetComponent<EnterBuilding>().theSceneName = sceneToTransitionTo;
            PlayerPrefs.SetInt("Progress", 0);
            fadeToBlack.Play("FadeToBlackScreen");
            Debug.Log("Player Hit F");
            this.gameObject.SetActive(false);
            InputSystem.DisableAllEnabledActions();
        }*/
    }
}
