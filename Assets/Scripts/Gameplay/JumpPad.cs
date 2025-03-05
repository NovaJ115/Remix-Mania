using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public PlayerMovementV2 playerMovement;
    public float bounceStrength;
    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovementV2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Body")
        {
            playerMovement.BouncePad(bounceStrength);
        }
    }
}
