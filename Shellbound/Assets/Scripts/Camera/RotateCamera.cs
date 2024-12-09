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

    Sequence sequence;

    void Awake()
    {
        UpdateRotation();
        //transform.localEulerAngles = new Vector3(0, 150, 0);

        isLocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sequence = DOTween.Sequence();
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
        sequence.Append(transform.DOLookAt(sliceBoard.transform.position, 0.5f).OnComplete(UpdateRotation));
        StartCoroutine(nameof(SoManyLogsWhileShitIsHappening));
        //transform.DOLookAt(sliceBoard.transform.position, 0.5f).OnComplete(UpdateRotation);
    }

    private void UpdateRotation()
    {
        xRotation = transform.rotation.eulerAngles.x;//localRotation.eulerAngles.x;
        yRotation = transform.rotation.eulerAngles.y;
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
        if(axisRot < 0)
        {
            axisRot += 360;
        }
        axisRot -= 180;
        axisRot = Mathf.Clamp(axisRot, 155, 205);
        axisRot += 180;
    }

    public IEnumerator SetCameraLock(bool locked)
    {
        sequence.Kill();
        yield return new WaitForSecondsRealtime(0.2f);
        isLocked = locked;
        yield break;
    }

    IEnumerator SoManyLogsWhileShitIsHappening()
    {
        while(isLocked)
        {
            
            yield return null;
        }
        yield break;
    }
}
