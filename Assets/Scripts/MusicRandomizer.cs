using UnityEngine;
using TMPro;

public class MusicRandomizer : MonoBehaviour
{
    public AudioSource[] musicVariants;
    public int randomNumber;
    public int chanceForSecretSong;
    public TextMeshProUGUI nowPlayingText;
    public StatRandomizer statRandomizer;

    
    public void PlayRandomMusic()
    {
        if (!statRandomizer.isExtremeModeEnabled)
        {
            var secretSong = Random.Range(0, 100);
            if (secretSong > 100 - chanceForSecretSong)
            {
                randomNumber = 4;
            }
            else
            {
                randomNumber = Random.Range(0, musicVariants.Length - 2);
            }
        }
        else
        {
            randomNumber = 3;
        }
        musicVariants[randomNumber].Play();
        nowPlayingText.text = musicVariants[randomNumber].name;
        Debug.Log(randomNumber);
    }
    public void StopMusic()
    {
        musicVariants[randomNumber].Stop();
    }
}
