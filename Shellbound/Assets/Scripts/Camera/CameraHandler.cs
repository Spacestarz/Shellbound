using UnityEngine;
using DG.Tweening;

public class CameraHandler : MonoBehaviour
{
    Vector3 defaultPosition;
    Vector3 defaultRotation;
    Transform orientation;

    void Awake()
    {
        Application.targetFrameRate = 120;
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localEulerAngles;
        orientation = Camera.main.GetComponent<RotateCamera>().orientation;
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
        //Camera.main.DOShakePosition(0.15f, new Vector3(direction.x, -direction.y) * 0.5f, 5, 25, false, ShakeRandomnessMode.Harmonic);
    }

    void ResetPosition()
    {
        transform.DOLocalMove(defaultPosition, 0.1f);
    }

    void ResetRotation()
    {
        transform.DOLocalRotate(defaultRotation, 0.2f);
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
