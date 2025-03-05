using UnityEngine;

public class DestroyAfterPlaying : MonoBehaviour
{
    
    void Update()
    {
        if(this.gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            Destroy(this.gameObject);
        }
    }
}
