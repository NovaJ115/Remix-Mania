using UnityEngine;
using UnityEngine.InputSystem;
public class DoorManager : MonoBehaviour
{
    public GameObject textAboveDoor;
    public Animator fadeToBlack;
    public string sceneToTransitionTo;

    private bool isInDoor;
    void Start()
    {
        textAboveDoor.SetActive(false);
    }

    void Update()
    {
        if(isInDoor && InputManager.interactWasPressed)
        {
            fadeToBlack.GetComponent<EnterBuilding>().theSceneName = sceneToTransitionTo;
            PlayerPrefs.SetInt("Progress", 0);
            fadeToBlack.Play("FadeToBlackScreen");
            Debug.Log("Player Hit F");
            this.gameObject.SetActive(false);
            InputSystem.DisableAllEnabledActions();
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
