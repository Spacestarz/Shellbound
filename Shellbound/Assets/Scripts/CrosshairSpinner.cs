using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CrosshairSpinner : MonoBehaviour
{
    [HideInInspector] public bool isSpinning;
    Image image;
    Color originalColor;

    bool isResetting;
    bool startedTurningRed;

    float spinRate = 540;
    float maxSpinRate = 540;
    float accelerationRate = 5400;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    void Update()
    {
        if(isSpinning)
        {
            Rotate();

            if (!startedTurningRed)
            {
                TurnRed();
            }
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
        image.DOColor(originalColor, 0.15f);

        if(startedTurningRed)
        {
            startedTurningRed = false;
        }
    }


    void ResetSpinRate()
    {
        spinRate = 180;
        isResetting = false;
    }

    void TurnRed()
    {
        startedTurningRed = true;
        image.DOKill();
        image.DOColor(Color.red, 0.15f);
    }
}
