using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector2 posOrigin = new Vector2();
    Vector2 tempPos = new Vector2();
    public void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
        this.transform.Rotate(0, rotationSpeed, 0f, Space.Self);
    }
}
