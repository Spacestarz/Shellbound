using UnityEngine;

public class Billboarding : MonoBehaviour
{
    public bool rotateYAxis = false;
    Vector3 cameraDir; 

    void LateUpdate()
    {
        cameraDir = Camera.main.transform.forward;

        if (!rotateYAxis)
        {
            cameraDir.y = 0; 
        }

        //rotates the sprite to face the camera
        if (cameraDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraDir);
        }
    }
}
