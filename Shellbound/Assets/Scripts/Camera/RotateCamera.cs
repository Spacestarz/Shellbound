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

    public GameObject introPole1;
    public GameObject introPole2;

    void Awake()
    {
        UpdateRotation();

        //isLocked = false;
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

    public void UpdateRotation()
    {
        xRotation = transform.localEulerAngles.x;
        yRotation = transform.localEulerAngles.y;

        if(xRotation <= 0)
        {
            xRotation += 360;
        }
    }

    void GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SettingsManager.sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SettingsManager.sensitivity;

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

    //  INTRO THINGS=====================================================
#region
    public void IntroStareAtFloor()
    {
        transform.localEulerAngles = new Vector3(90, 45, 0);
    }

    public void IntroLookAtBoss()
    {
        transform.DODynamicLookAt(GameObject.Find("MantisShrimp").transform.position, 3).OnComplete(InvokeOvershoot);
    }

    void InvokeOvershoot()
    {
        GetComponent<CameraHandler>().isDisorientedFOV = false;
        GetComponent<CameraHandler>().ResetFOV();
        Invoke(nameof(IntroLookUpOvershoot), 0.5f);
    }

    void IntroLookUpOvershoot()
    {
        Vector3 overshootVector = new Vector3(0, 75, 0);
        transform.DODynamicLookAt(GameObject.Find("MantisShrimp").transform.position + overshootVector, 0.75f).OnComplete(IntroLookAtBossFast);
    }


    void IntroLookAtBossFast()
    {
        transform.DODynamicLookAt(GameObject.Find("MantisShrimp").transform.position, 0.33f).OnComplete(InvokeLookLeft);
        IntroManager.SpawnFirstSpike();
    }


    void InvokeLookLeft()
    {
        Invoke(nameof(IntroLookLeft), 0.33f);
    }


    void IntroLookLeft()
    {
        transform.DODynamicLookAt(introPole1.transform.position, 0.33f).OnComplete(InvokeLookRight);
    }


    void InvokeLookRight()
    {
        Invoke(nameof(IntroLookRight), 0.33f);
    }

    void IntroLookRight()
    {
        Vector3 rightAmount = new(100, 0, 0);
        transform.DODynamicLookAt(introPole2.transform.position, 0.44f).OnComplete(InvokeLookBackAtBoss);
    }

    void InvokeLookBackAtBoss()
    {
        Invoke(nameof(IntroLookBackAtBoss), 0.3f);
    }

    void IntroLookBackAtBoss()
    {
        transform.DODynamicLookAt(GameObject.Find("MantisShrimp").transform.position, 0.33f).OnComplete(InvokeIntroZoomIn);
    }

    void InvokeIntroZoomIn()
    {
        Invoke(nameof(IntroZoomIn), 0.15f);
    }

    void IntroZoomIn()
    {
        Camera.main.DOFieldOfView(10, 0.15f);
    }

    public void IntroSetRotation()
    {
        xRotation = 360.08f;
        yRotation = transform.localEulerAngles.y;
    }
#endregion
}
