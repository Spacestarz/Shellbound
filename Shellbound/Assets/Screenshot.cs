using System;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public int multiplier = 2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string name = Application.productName + date + ".png";
            ScreenCapture.CaptureScreenshot(name, multiplier);
            Debug.Log("Screenshot saved to " + name);
        }
    }
}
