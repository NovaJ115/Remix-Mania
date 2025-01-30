using UnityEngine;

public class LevelSelectCamera : MonoBehaviour
{
    public Camera theCamera;
    public GameObject theRoomCameraPos;
    public float theRoomCameraSize;
    public float moveSpeed = 5f;

    public LevelSelectCameraManager camManager;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Body")
        {
            camManager.moveSpeed = moveSpeed;
            camManager.theRoomCameraPos = theRoomCameraPos.transform.position;
            camManager.newOrthographicSize = theRoomCameraSize;
        }
    }
}
