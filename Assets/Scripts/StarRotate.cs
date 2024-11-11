using UnityEngine;

public class StarRotate : MonoBehaviour
{
    public float starRotationSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(0f, 0f, starRotationSpeed, Space.Self);
    }
}
