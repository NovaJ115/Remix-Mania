using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAttempt2 : MonoBehaviour
{

    private Rigidbody2D rb;

    public float walkSpeed;
    public float jumpForce;
    public float slideSpeed;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private MovementAttempt2 playerCollision;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<MovementAttempt2>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();

        }
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        Debug.Log(rb.linearVelocity);
        
    }

    private void Walk(Vector2 dir)
    {
        rb.linearVelocity = (new Vector2(dir.x * walkSpeed, rb.linearVelocity.y));
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.linearVelocity += Vector2.up * jumpForce;
    }
    private void WallSlide()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slideSpeed);
    }

}
