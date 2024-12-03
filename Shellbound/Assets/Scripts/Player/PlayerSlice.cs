using UnityEngine;

public class PlayerSlice : MonoBehaviour
{
    static bool sliceMode;
    static Camera mainCam;

    public static PlayerSlice instance;
    public SliceableObject caughtObject;
    public static SlicePattern currentSlicePattern;

    static Vector2 mouseMovement;

    static Vector2 targetDirection;
    static Vector2 mouseDirection;

    static float sliceTickTime = 0;
    static float sliceTickLength = 0.033f;

    static float sliceTime = 0;
    static float sliceTimeLimit = 1.25f;

    static float requiredDotProduct = 0.8f;

    static float currentMagnitude = 0;
    static float requiredMagnitude = 5;

    static int successfulTicks = 0;
    static int requiredTicks = 3;

    private void Awake()
    {
        mainCam = Camera.main;

        sliceMode = false;

        if (instance == null)
        {
            instance = this;
        }
    }

    private void LateUpdate()
    {
        if (sliceMode)
        {
            Timer();
        }
    }

    public static void SetSliceMode(bool status)
    {
        sliceMode = status;
        sliceTime = 0;
        sliceTickTime = 0;

        successfulTicks = 0;
        currentMagnitude = 0;

        if (sliceMode)
        {
            instance.GetComponent<PlayerController>().NullifyMovement();
            currentSlicePattern = instance.caughtObject.GetComponentInChildren<SlicePattern>();
            currentSlicePattern.NextSliceArrow();

            mainCam.GetComponent<RotateCamera>().isLocked = true;
        }
        else if (currentSlicePattern != null)
        {
            currentSlicePattern.DestroyArrow();
            currentSlicePattern.ResetPattern();
        }

        SetCursor();
    }


    public static bool SliceMode()
    {
        return sliceMode;
    }


    static void SetCursor()
    {
        if (sliceMode)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public static void SetCaughtObject(SliceableObject obj)
    {
        instance.caughtObject = obj;
        SetSliceMode(true);
    }


    public static void ClearCaughtObject()
    {
        instance.caughtObject = null;
        currentSlicePattern = null;
    }


    public static void SetTargetDirection(Vector2 dir)
    {
        targetDirection = dir.normalized;

        //If only one axis is 0 (orthogonal)
        if (targetDirection.x != 0 ^ targetDirection.y != 0)
        {
            requiredDotProduct = 0.9f;
            requiredMagnitude = 17f;
        }
        else // (Diagonal)
        {
            requiredDotProduct = 0.8f;
            requiredMagnitude = 13f;
        }
    }


    static void Timer()
    {
        sliceTickTime += Time.deltaTime;
        sliceTime += Time.deltaTime;

        if (sliceTickTime >= sliceTickLength)
        {
            SliceTick();
        }

        if (sliceTime >= sliceTimeLimit)
        {
            FailSlice();
        }
    }


    static void SliceTick()
    {
        GetMouseDirection();
        CompareSliceDirection();

        sliceTickTime %= sliceTickLength;
    }


    static void GetMouseDirection()
    {
        mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDirection = mouseMovement.normalized;
    }


    static void CompareSliceDirection()
    {
        if (Vector2.Dot(mouseDirection, targetDirection) >= requiredDotProduct)
        {
            SuccessfulTick();
        }
        else
        {
            successfulTicks = 0;
        }

        if (currentSlicePattern != null)
        {
            currentSlicePattern.spawnedArrow.TurnRed(successfulTicks, requiredTicks);
        }
    }

    static void SuccessfulTick()
    {
        successfulTicks++;

        if (successfulTicks >= requiredTicks)
        {
            CompleteSlice();
        }
    }

    static void CompleteSlice()
    {
        currentSlicePattern.spawnedArrow.CompleteSlice();
        currentSlicePattern.NextSliceArrow();
        successfulTicks = 0;

        sliceTime = 0;
    }

    static void FailSlice()
    {
        currentSlicePattern.FailPattern();
        ClearCaughtObject();
        SetSliceMode(false);
    }
}
