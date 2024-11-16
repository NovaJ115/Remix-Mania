using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject theCamera;
    public float zoomSpeed;
    public Animator screenOpenAnim;

    
    
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
        /*if (Input.GetKey(KeyCode.Q))
        {
            theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize -= zoomSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            theCamera.GetComponent<CinemachineCamera>().Lens.OrthographicSize += zoomSpeed;
        }*/
    }

    

}
