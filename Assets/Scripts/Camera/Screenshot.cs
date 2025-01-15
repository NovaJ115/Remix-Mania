using UnityEngine;
using System;

public class Screenshot : MonoBehaviour
{
    public bool canScreenshot;
    void Update()
    {
        if(canScreenshot == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ScreenCapture.CaptureScreenshot("screenshot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png", 5);
            }
        }
    }
}
