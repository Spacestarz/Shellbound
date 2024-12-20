using UnityEngine;

public class Billboarding : MonoBehaviour
{
    public bool rotateXAxis = true;
    public bool rotateYAxis = false;
    public bool rotateZAxis = true;

    float defaultXAxis;
    float defaultYAxis;
    float defaultZAxis;

    Vector3 cameraDir;

    private void Awake()
    {
        defaultXAxis = transform.eulerAngles.x;
        defaultYAxis = transform.eulerAngles.y;
        defaultZAxis = transform.eulerAngles.z;
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
