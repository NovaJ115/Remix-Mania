using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("allVolume"))
        {
            PlayerPrefs.SetFloat("allVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        //volumeSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        if(volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("allVolume");
        }
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("allVolume", volumeSlider.value);
    }

}
