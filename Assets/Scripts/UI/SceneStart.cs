using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    public AudioSource sceneStartSound;
    private GameObject music;
    public void Awake()
    {
        music = GameObject.FindWithTag("Music");
    }

    public void PlaySceneStartSound()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            music.GetComponent<MusicRandomizer>().PlayRandomMusic();
        }
        
        sceneStartSound.Play();
    }
}
