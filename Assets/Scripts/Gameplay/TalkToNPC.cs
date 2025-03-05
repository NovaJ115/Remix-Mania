using UnityEngine;
using Unity.Cinemachine;

public class TalkToNPC : MonoBehaviour
{
    private PlayerMovementV2 playerMovement;
    public UniversalDialogueManager dialogueManager;
    public GameObject textAboveNPC;
    public GameObject dialogueBox;
    public GameObject theCamera;
    public float originalOrthograthicSize;
    public float newOrthographicSize;
    public float zoomSpeed;
    public float cameraYChange;
    public float cameraXChange;

    public bool startMoving = false;

    public bool playerFacingRight;

    private bool isInHitbox;

    public void Start()
    {
        startMoving = false;
        Debug.Log(theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize);
        playerMovement = FindFirstObjectByType<PlayerMovementV2>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Body")
        {
            textAboveNPC.SetActive(true);
            isInHitbox = true;
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Body")
        {
            if(textAboveNPC != null)
            {
                textAboveNPC.SetActive(false);
            }
            isInHitbox = false;
        }
    }
    public void Update()
    {
        if (isInHitbox && InputManager.interactWasPressed)
        {
            if (!playerMovement.isFacingRight)
            {
                playerMovement.Turn(playerFacingRight);
            }
            
            InputManager.playerInput.currentActionMap.Disable();
            startMoving = true;
            theCamera.GetComponent<CinemachinePositionComposer>().TargetOffset.y = cameraYChange;
            theCamera.GetComponent<CinemachinePositionComposer>().TargetOffset.x = cameraXChange;
            textAboveNPC.SetActive(false);
            dialogueBox.SetActive(true);
            dialogueManager.StartDialogue(dialogueManager.gameObject.GetComponent<TextBoxDialogue>().dialogue);
        }
        if (startMoving)
        {
            theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize = Mathf.Lerp(theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize, newOrthographicSize, zoomSpeed * Time.deltaTime);
            //Debug.Log("ShouldBeMoving");
        }

        if (dialogueManager.dialogueOver)
        {
            dialogueBox.SetActive(false);
            InputManager.playerInput.currentActionMap.Enable();
            theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize = Mathf.Lerp(theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize, originalOrthograthicSize, zoomSpeed * Time.deltaTime);
            theCamera.GetComponent<CinemachinePositionComposer>().TargetOffset.y = 0;
            theCamera.GetComponent<CinemachinePositionComposer>().TargetOffset.x = 0;
        }

    }
    


}
