using UnityEngine;

public class Billboarding : MonoBehaviour
{
    public bool rotateYAxis = false;
    Vector3 cameradir; 

    void Update()
    {
        cameradir = Camera.main.transform.forward;
        
        if (!rotateYAxis)
        {
            cameradir.y = 0; 
        }

        //rotates the sprite to face the camera
        if (cameradir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameradir);
        }
    }
}
