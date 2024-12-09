using UnityEngine;
using DG.Tweening;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
   
    void Awake()
    {
        Application.targetFrameRate = 120;
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
        Debug.Log("E");
        Camera.main.DOShakePosition(0.2f, new Vector3(0.2f, 1, 0), 10, 45, true, ShakeRandomnessMode.Harmonic);
    }

    public void ShakeCamera(Vector3 direction)
    {
        Camera.main.DOShakePosition(0.2f, new Vector3(0.2f, 1, 0), 10, 45, false, ShakeRandomnessMode.Harmonic);
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
