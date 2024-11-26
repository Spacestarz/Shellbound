using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;

    public float xRotation;
    public float yRotation;

    public GameObject Sliceboard;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  
    }

    void Update()
    {
        GetMouseInput();

      if (Harpoon.hasCaught)
        {
            transform.LookAt(Sliceboard.transform.position);
        }
    }

    void GetMouseInput()
    {
        if (!PlayerSlice.SliceMode())
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation %= 360;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
