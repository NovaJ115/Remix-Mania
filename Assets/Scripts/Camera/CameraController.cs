using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject theCamera;
    public float zoomSpeed;
    public Animator screenOpenAnim;

    public bool allowZoom;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenOpenAnim.gameObject.SetActive(true);
        //screenOpenAnim.Play("ScreenTransitionOpen");

        var height = theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize;
        //var width = height * theCamera.GetComponent<CinemachineCamera>().
        
        
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
        
    }

    

}
