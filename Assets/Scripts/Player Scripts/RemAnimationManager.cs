using UnityEngine;

public class RemAnimationManager : MonoBehaviour
{
    public Animator remAnim;
    public PlayerMovement playerMovement;
    

    public void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        
    }

    public void Update()
    {
        if (playerMovement.isJumping == true)
        {
            remAnim.SetBool("IsJumping", true);
        }
        else
        {
            remAnim.SetBool("IsJumping", false);
        }
        if (InputManager.movement.x != 0 && remAnim.GetBool("IsJumping") == false)
        {
            remAnim.SetTrigger("StartRunning");

            //remAnim.ResetTrigger("StartRunning");
        }
        
        if (InputManager.movement == Vector2.zero)
        {
            remAnim.SetTrigger("StartIdle");
            //remAnim.ResetTrigger("StartIdle");
        }
        
    }
}
