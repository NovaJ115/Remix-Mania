using UnityEngine;

public class RemSounds : MonoBehaviour
{
    public AudioSource footstep1;
    public AudioSource footstep2;


        public void PlayFootstep1()
    {
        footstep1.Play();
    }

    public void PlayFootstep2()
    {
        footstep2.Play();
    }

}
