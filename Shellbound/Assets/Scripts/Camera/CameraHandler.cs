using UnityEngine;
using DG.Tweening;

public class CameraHandler : MonoBehaviour
{
    Vector3 defaultSpot;

    void Awake()
    {
        defaultSpot = transform.localPosition;
        Application.targetFrameRate = 120;
        Debug.Log(defaultSpot);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ShakeCamera();
        }
    }

    public void ShakeCamera()
    {
        this.DOKill();
        Camera.main.DOShakePosition(0.4f, new Vector3(0.2f, 1, 0), 10, 45, true, ShakeRandomnessMode.Harmonic).OnComplete(ResetPosition);
    }

    public void ShakeCameraSlice(Vector3 direction)
    {
        transform.DOLocalRotate(transform.localEulerAngles + direction, 0.2f).OnComplete(ResetPosition);
        //Camera.main.DOShakePosition(0.15f, new Vector3(direction.x, -direction.y) * 0.5f, 5, 25, false, ShakeRandomnessMode.Harmonic);
    }

    void ResetPosition()
    {
        transform.DOLocalMove(defaultSpot, 0.2f);
    }

    public void ChangeFOV(float duration)
    {
        Camera.main.DOFieldOfView(80, duration).OnComplete(ResetFOV);
    }

    void ResetFOV()
    {
        Camera.main.DOFieldOfView(60, 0.4f);
    }
}
