using UnityEngine;
using DG.Tweening;

public class CameraHandler : MonoBehaviour
{
    Vector3 defaultPosition;
    Transform orientation;

    public bool isDisorientedFOV;

    void Awake()
    {
        Application.targetFrameRate = 120;
        defaultPosition = transform.localPosition;
        if(Camera.main.GetComponent<RotateCamera>())
        {
            orientation = Camera.main.GetComponent<RotateCamera>().orientation;
        }
    }

    private void Update()
    {
        if(isDisorientedFOV)
        {
            PulseFOV();
        }

    }

    void PulseFOV()
    {
        Camera.main.fieldOfView = 60 + Mathf.Sin(Time.time * 4) * 2;
    }

    public void ShakeCamera(float duration, Vector3 strength)
    {
        this.DOKill();
        Camera.main.DOShakePosition(duration, strength, 10, 45, true, ShakeRandomnessMode.Harmonic).OnComplete(ResetPosition);
    }

    public void ShakeCameraSlice(Vector3 direction)
    {
        Vector3 dirToMove = Vector3.zero;
        if(direction.x < 0)
        {
            dirToMove -= orientation.right;
        }
        else if(direction.x > 0)
        {
            dirToMove += orientation.right;
        }

        if(direction.y < 0)
        {
            dirToMove -= orientation.up;
        }
        else if (direction.y > 0)
        {
            dirToMove += orientation.up;
        }

        transform.DOMove(transform.position +  0.3f * dirToMove, 0.1f).OnComplete(ResetPosition);
    }

    void ResetPosition()
    {
        transform.DOLocalMove(defaultPosition, 0.1f);
    }


    public void ChangeFOV(float duration)
    {
        Camera.main.DOFieldOfView(80, duration).OnComplete(ResetFOV);
    }


    public void ResetFOV()
    {
        Camera.main.DOFieldOfView(60, 0.4f);
    }

    public void WeakBossRoar()
    {
        this.DOKill();
        Camera.main.DOShakePosition(2f, new Vector3(0.2f, 0.2f, 0.2f), 30, 45, true, ShakeRandomnessMode.Harmonic);
    }

    public void IntroBossRoar(IntroManager calledFrom)
    {
        Invoke(nameof(IntroZoomOut), 1.25f);
        calledFrom.InvokeRestoreUIElements();
        Camera.main.DOShakePosition(2f, new Vector3(1,1,1), 40, 90, true, ShakeRandomnessMode.Harmonic).OnComplete(ResetPosition).OnComplete(calledFrom.ResetSpatialBlend);
    }

    void IntroZoomOut()
    {
        Camera.main.DOFieldOfView(60, 1.4f);
    }
}
