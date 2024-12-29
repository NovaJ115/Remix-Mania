using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterBuilding : MonoBehaviour
{
    public string theSceneName;
    public Animator remFullPortrait;
    public void EnterDoor(string sceneName)
    {
        sceneName = theSceneName;
        SceneManager.LoadScene(sceneName);
    }
    public void StartTheDialogue()
    {
        FindFirstObjectByType<TextBoxDialogue>().StartDialogue();
    }
    public void RemPortraitSlideIn()
    {
        remFullPortrait.Play("RemPortraitSlideIn");
    }
}
