using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public AudioSource deathNoise;
    public GameObject deathScreen;

    public void Start()
    {
        deathNoise = this.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameObject.FindWithTag("Body"))
        {
            Lose();
            Debug.Log("EnteredDeathBox");
        }
    }
    public void Lose()
    {
        deathNoise.Play();
        deathScreen.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
    }
}
