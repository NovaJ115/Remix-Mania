using UnityEngine;

public class LevelSelectCameraManager : MonoBehaviour
{
    public Camera theCamera;
    public Vector3 theRoomCameraPos;
    public float moveSpeed;
    public float newOrthographicSize;
    public float orthographicSpeed;
    void Update()
    {
        if(theCamera != null && theRoomCameraPos != null && moveSpeed != 0)
        {
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, theRoomCameraPos, moveSpeed * Time.deltaTime);
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, newOrthographicSize, orthographicSpeed * Time.deltaTime);
        }

    }
}
