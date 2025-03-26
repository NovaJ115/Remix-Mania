using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SwitchUIOnControlScheme : MonoBehaviour
{
    private PlayerInput playerInput;
    private TextMeshProUGUI theText;

    [SerializeField]
    [TextArea(3, 10)]
    private string textForKeyboard;
    [SerializeField]
    [TextArea(3, 10)]
    private string textForGamepad;

    void Start()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        theText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard")
        {
            theText.text = textForKeyboard;
        }
        if (playerInput.currentControlScheme == "Gamepad")
        {
            theText.text = textForGamepad;
        }
    }
}
