using UnityEngine;

public class LevelSelectCamera : MonoBehaviour
{
    public Camera theCamera;
    public GameObject theRoomCameraPos;
    public float theRoomCameraSize;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Body")
        {
            theCamera.transform.position = theRoomCameraPos.transform.position;
            theCamera.orthographicSize = theRoomCameraSize;
            
        }
    }
}
