using UnityEngine;

public class CurrentRemixesUIManager : MonoBehaviour
{
    public GameObject closeArrow;
    public GameObject openArrow;
    public void ToggleAnim()
    {
        this.GetComponent<Animator>().SetTrigger("ToggleState");
        if(closeArrow.activeInHierarchy == true)
        {
            openArrow.SetActive(true);
            closeArrow.SetActive(false);
        }
        else if (openArrow.activeInHierarchy == true)
        {
            openArrow.SetActive(false);
            closeArrow.SetActive(true);
        }
    }
    public void Update()
    {
        if (InputManager.closeTabWasPressed)
        {
            ToggleAnim();
        }
    }
}

