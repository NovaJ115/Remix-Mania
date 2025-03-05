using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableCurrentRemixesText : MonoBehaviour
{
    
    void Update()
    {
        if(this.gameObject.name != SceneManager.GetActiveScene().name)
        {
            this.gameObject.SetActive(false);
        }
    }
}
