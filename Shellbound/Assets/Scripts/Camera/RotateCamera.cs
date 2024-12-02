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
        isLocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Harpoon.hasCaught)// && !startedLooking)
        {
            LockOntoSliceBoard(Harpoon.instance.caughtObject.sliceBoard);
        }
        else if (!Harpoon.hasCaught && !isLocked)
        {
            GetMouseInput();
        }
    }

    public void LockOntoSliceBoard(SlicePattern sliceBoard)
    {
        transform.DOLookAt(sliceBoard.transform.position, 0.5f).OnComplete(UpdateRotation);
    }

    private void UpdateRotation()
    {
        xRotation = transform.localRotation.eulerAngles.x;
        yRotation = transform.localRotation.eulerAngles.y;
    }

    void GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -25, 45);
        yRotation %= 360;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public IEnumerator SetCameraLock(bool locked)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        isLocked = locked;
        yield break;
    }
}
