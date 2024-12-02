using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RotateCamera : MonoBehaviour
{
    public static RotateCamera instance;
    public static bool isLocked;

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

        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Harpoon.hasCaught)// && !startedLooking)
        {
            LockOntoSliceBoard(Harpoon.instance.caughtObject.sliceBoard);
        }
        else if (!Harpoon.hasCaught)
        {
            GetMouseInput();
        }
    }

    public static void LockOntoSliceBoard(SlicePattern sliceBoard)
    {
        instance.transform.DOLookAt(sliceBoard.transform.position, 0.5f).OnComplete(UpdateRotation);
    }

    private static void UpdateRotation()
    {
        instance.xRotation = instance.transform.localRotation.eulerAngles.x;
        instance.yRotation = instance.transform.localRotation.eulerAngles.y;
    }

    void GetMouseInput()
    {
        if (!Harpoon.hasCaught && !isLocked)
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
    }

    public static IEnumerator SetCameraLock(bool locked)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        isLocked = locked;
    }
}
