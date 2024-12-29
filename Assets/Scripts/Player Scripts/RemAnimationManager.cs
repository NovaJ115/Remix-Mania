using UnityEngine;

public class RemAnimationManager : MonoBehaviour
{
    public Animator remAnim;
    public PlayerMovement playerMovement;
    public GameObject face;

    public void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        
    }

    public void Update()
    {
        if (playerMovement.isJumping == true)
        {
            remAnim.SetBool("IsJumping", true);
            if (face != null)
            {
                face.SetActive(false);
            }
        }
        else
        {
            remAnim.SetBool("IsJumping", false);
            if (face != null)
            {
                face.SetActive(true);
            }
        }
        if (InputManager.movement.x != 0 && remAnim.GetBool("IsJumping") == false && playerMovement.isGrounded == true)
        {
            remAnim.SetTrigger("StartRunning");
            if (face != null)
            {
                face.SetActive(false);
            }
            //remAnim.ResetTrigger("StartRunning");
        }
        
        if (InputManager.movement == Vector2.zero && playerMovement.isGrounded == true)
        {
            remAnim.SetTrigger("StartIdle");
            if(face != null && remAnim.GetBool("IsJumping") == false)
            {
                face.SetActive(true);
            }
            
            //remAnim.ResetTrigger("StartIdle");
        }
        if(playerMovement.isGrounded == false)
        {
            remAnim.SetBool("IsJumping", true);
        }
        if(playerMovement.isGrounded == true)
        {
            remAnim.SetBool("IsJumping", false);
        }
    }
}
