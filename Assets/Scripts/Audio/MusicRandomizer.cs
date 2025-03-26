using UnityEngine;
using TMPro;

public class MusicRandomizer : MonoBehaviour
{
    public AudioSource[] musicVariants;
    public int randomNumber;
    public int chanceForSecretSong;
    public TextMeshProUGUI nowPlayingText;
    public StatRandomizer statRandomizer;
    public int finalRemixSongNumber;
    public int secretSongNumber;

    
    public void PlayRandomMusic()
    {
        if (!statRandomizer.isExtremeModeEnabled)
        {
            var secretSong = Random.Range(0, 100);
            if (secretSong > 100 - chanceForSecretSong)
            {
                randomNumber = secretSongNumber;
            }
            else
            {
                randomNumber = Random.Range(0, musicVariants.Length - 2);
            }
        }
        else
        {
            randomNumber = finalRemixSongNumber;
        }
        if(PlayerPrefs.GetInt("Progress") == statRandomizer.remixManager.completionAmount - 1)
        {
            randomNumber = finalRemixSongNumber;
        }
        musicVariants[randomNumber].Play();
        nowPlayingText.text = musicVariants[randomNumber].name;
        //Debug.Log(randomNumber);
    }
    public void StopMusic()
    {
        musicVariants[randomNumber].Stop();
    }
}
