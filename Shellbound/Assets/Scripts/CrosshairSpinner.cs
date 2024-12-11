using UnityEngine;
using DG.Tweening;

public class CrosshairSpinner : MonoBehaviour
{
    [HideInInspector] public bool isSpinning;

    bool isResetting;

    float spinRate = 180;
    float maxSpinRate = 540;
    float accelerationRate = 5400;

    void Update()
    {
        if(isSpinning)
        {
            Rotate();
        }
        else if(!isResetting)
        {
            ResetRotation();
        }
    }


    public void SetSpinning(bool status)
    {
        this.DOKill();
        isSpinning = status;
    }


    void Rotate()
    {
        AccelerateRotation();
        transform.localEulerAngles += new Vector3(0, 0, spinRate) * Time.deltaTime;
    }


    void AccelerateRotation()
    {
        if(spinRate < maxSpinRate)
        {
            spinRate += accelerationRate * Time.deltaTime;
        }
        Mathf.Clamp(spinRate, 180, maxSpinRate);
    }


    public void ResetRotation()
    {
        isResetting = true;
        transform.DORotate(new Vector3(0,0,0), 0.4f).OnComplete(ResetSpinRate);
    }


    void ResetSpinRate()
    {
        spinRate = 180;
        isResetting = false;
    }
}
