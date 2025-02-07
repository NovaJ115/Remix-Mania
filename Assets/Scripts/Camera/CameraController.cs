using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject cinemachine;
    public Animator screenOpenAnim;


    public float zoomSpeed;
    public float lookUpHeight;
    public float lookDownHeight;
    public float lookLeftLength;
    public float lookRightLength;
    public bool allowZoom;
    public bool allowLookUp;
    public bool allowLookDown;
    public bool allowLookLeft;
    public bool allowLookRight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cinemachine = GameObject.FindGameObjectWithTag("Cinemachine");
        screenOpenAnim.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(allowZoom == true)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize -= zoomSpeed;
            }
            if (Input.GetKey(KeyCode.X))
            {
                theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize += zoomSpeed;
            }
        }
        if(allowLookUp == true)
        {
            if (InputManager.lookUpIsHeld)
            {
                cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.y = lookUpHeight;
            }
            
        }
        if (allowLookDown == true)
        {
            if (InputManager.lookDownIsHeld)
            {
                cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.y = -lookDownHeight;
            }
        }
        if (allowLookLeft == true)
        {
            if (InputManager.lookLeftIsHeld)
            {
                cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.x = lookLeftLength;
            }

        }
        if (allowLookRight == true)
        {
            if (InputManager.lookRightIsHeld)
            {
                cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.x = -lookRightLength;
            }
        }

        if (!InputManager.lookUpIsHeld && !InputManager.lookDownIsHeld)
        {
            cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.y = 0;
            
        }
        if(!InputManager.lookLeftIsHeld && !InputManager.lookRightIsHeld)
        {
            cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.x = 0;
        }

    }

    

}
