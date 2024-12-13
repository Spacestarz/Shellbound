using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RotateCamera : MonoBehaviour
{
    public bool isLocked;

    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;

    public float xRotation;
    public float yRotation;


    void Awake()
    {
        UpdateRotation();

        isLocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Harpoon.hasCaught)
        {
            LockOntoSliceBoard(Harpoon.instance.caughtObject.sliceableObject.sliceBoard);
            ClampRotation(ref xRotation);
        }
        else if (!Harpoon.hasCaught && !isLocked)
        {
            this.DOKill();
            GetMouseInput();
        }
    }

    public void LockOntoSliceBoard(SlicePattern sliceBoard)
    {
        transform.DOLookAt(sliceBoard.transform.position, 0.5f).OnComplete(UpdateRotation);
    }

    private void UpdateRotation()
    {
        xRotation = transform.rotation.eulerAngles.x;
        yRotation = transform.rotation.eulerAngles.y;

        
        if(xRotation <= 0)
        {
            xRotation += 360;
        }

        ClampRotation(ref xRotation);
    }

    void GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        xRotation -= mouseY;
        yRotation += mouseX;

        
        ClampRotation(ref xRotation);
        
        yRotation %= 360;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }


    void ClampRotation(ref float axisRot)
    {
        if (axisRot <= 0)
        {
            axisRot += 360;
        }

        axisRot -= 180;
        axisRot = Mathf.Clamp(axisRot, 155, 205);
        axisRot += 180;
    }


    public IEnumerator SetCameraLock(bool locked)
    {
        this.DOKill();
        yield return new WaitForSecondsRealtime(0.2f);
        ClampRotation(ref xRotation);
        isLocked = locked;
        yield break;
    }
}
