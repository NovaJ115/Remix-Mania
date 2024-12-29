using UnityEngine;
using UnityEngine.InputSystem;
public class DoorManager : MonoBehaviour
{
    public GameObject textAboveDoor;
    public Animator fadeToBlack;
    void Start()
    {
        textAboveDoor.SetActive(false);
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Body")
        {
            textAboveDoor.SetActive(true);
            //Debug.Log("Collided With Player");
        }
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Body")
        {
            textAboveDoor.SetActive(false);
            //Debug.Log("Collided With Player");
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Body" && Input.GetKeyDown(KeyCode.F))
        {
            fadeToBlack.Play("FadeToBlackScreen");
            Debug.Log("Player Hit F");
            this.gameObject.SetActive(false);
            InputSystem.DisableAllEnabledActions();
        }
    }
}
