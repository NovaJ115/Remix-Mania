using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;
    private StatRandomizer statRandomizer;

    public static Vector2 movement;
    public static bool jumpWasPressed;
    public static bool jumpIsHeld;
    public static bool jumpWasReleased;
    public static bool runIsHeld;
    public static bool dashWasPressed;
    public static bool testWasPressed;
    public static bool lookUpIsHeld;
    public static bool lookDownIsHeld;
    public static bool lookLeftIsHeld;
    public static bool lookRightIsHeld;
    public static bool pauseWasPressed;
    public static bool closeTabWasPressed;
    public static bool remixWasPressed;
    public static bool interactWasPressed;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction dashAction;
    private InputAction testAction;
    private InputAction lookUpAction;
    private InputAction lookDownAction;
    private InputAction lookLeftAction;
    private InputAction lookRightAction;
    private InputAction pauseAction;
    private InputAction closeTabAction;
    private InputAction remixAction;
    private InputAction interactAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        statRandomizer = FindFirstObjectByType<StatRandomizer>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        runAction = playerInput.actions["Run"];
        dashAction = playerInput.actions["Dash"];
        lookUpAction = playerInput.actions["LookUp"];
        lookDownAction = playerInput.actions["LookDown"];
        lookLeftAction = playerInput.actions["LookLeft"];
        lookRightAction = playerInput.actions["LookRight"];
        testAction = playerInput.actions["Test"];
        pauseAction = playerInput.actions["Pause"];
        pauseAction = playerInput.actions["Cancel"];
        closeTabAction = playerInput.actions["CloseTab"];
        remixAction = playerInput.actions["Remix"];
        interactAction = playerInput.actions["Interact"];
    }

    private void Update()
    {
        if (!statRandomizer.isInvertedControls)
        {
            movement = moveAction.ReadValue<Vector2>();
        }
        else
        {
            movement = -moveAction.ReadValue<Vector2>();
        }
        

        jumpWasPressed = jumpAction.WasPressedThisFrame();
        jumpIsHeld = jumpAction.IsPressed();
        jumpWasReleased = jumpAction.WasReleasedThisFrame();

        runIsHeld = runAction.IsPressed();

        dashWasPressed = dashAction.WasPressedThisFrame();

        testWasPressed = testAction.WasPressedThisFrame();

        lookUpIsHeld = lookUpAction.IsPressed();
        lookDownIsHeld = lookDownAction.IsPressed();
        lookLeftIsHeld = lookLeftAction.IsPressed();
        lookRightIsHeld = lookRightAction.IsPressed();

        pauseWasPressed = pauseAction.WasPressedThisFrame();

        closeTabWasPressed = closeTabAction.WasPressedThisFrame();

        remixWasPressed = remixAction.WasPressedThisFrame();

        interactWasPressed = interactAction.WasPressedThisFrame();
    }

}
