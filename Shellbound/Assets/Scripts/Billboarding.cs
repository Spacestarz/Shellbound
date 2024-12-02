using UnityEngine;

public class Billboarding : MonoBehaviour
{
    public bool rotateXAxis = true;
    public bool rotateYAxis = false;
    public bool rotateZAxis = true;

    float defaultXAxis = 0;
    float defaultYAxis = 0;
    float defaultZAxis = 0;

    Vector3 cameraDir;

    private void Awake()
    {
        defaultXAxis = Quaternion.identity.eulerAngles.x;
        defaultYAxis = Quaternion.identity.eulerAngles.y;
        defaultZAxis = Quaternion.identity.eulerAngles.z;
    }

    void LateUpdate()
    {
        cameraDir = Camera.main.transform.forward;

        if (!rotateXAxis)
        {
            cameraDir.x = defaultXAxis;
        }
        if (!rotateYAxis)
        {
            cameraDir.y = defaultYAxis; 
        }
        if (!rotateZAxis)
        {
            cameraDir.z = defaultZAxis;
        }

        //rotates the sprite to face the camera
        if (cameraDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraDir);
        }
    }
}
