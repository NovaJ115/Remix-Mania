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
    public bool allowZoom;
    public bool allowLookUp;
    public bool allowLookDown;


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
            if (Input.GetKey(KeyCode.W))
            {
                cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.y = lookUpHeight;
            }
            
        }
        if (allowLookDown == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.y = -lookDownHeight;
            }
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            cinemachine.GetComponent<CinemachinePositionComposer>().Composition.ScreenPosition.y = 0;
        }

    }

    

}
