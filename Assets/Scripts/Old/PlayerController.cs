using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    [Header("Horizontal Movement")]
    public float moveSpeed;
    float horizontalMovement;
    [Header("Ground")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(.5f, .05f);
    public LayerMask groundLayer;
    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    public int jumpsRemaining;
    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    
    void Start()
    {
        
    }


    void Update()
    {
        GroundCheck();

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        
        Gravity();
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
                
            }
        }
        
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize,0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    public void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y - maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    
}
