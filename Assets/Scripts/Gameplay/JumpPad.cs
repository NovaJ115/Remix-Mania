using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPad : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float baseForce;
    public float forceMultiplier;
    public float jumpPadForce { get; private set; }
    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerMovement.rb.linearVelocity = new Vector2(playerMovement.rb.linearVelocity.x, baseForce);
            Debug.Log("AttemptedToJumpPadTest");
        }
    }
    private void OnValidate()
    {
        CalculateValues();
    }
    private void OnEnable()
    {
        CalculateValues();
    }
    public void CalculateValues()
    {
        jumpPadForce = baseForce * forceMultiplier;
    }
}
