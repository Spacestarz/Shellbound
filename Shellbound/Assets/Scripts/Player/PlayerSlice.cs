using UnityEngine;

public class PlayerSlice : MonoBehaviour
{
    static bool sliceMode;

    public static PlayerSlice instance;
    public GameObject caughtObject;
    public static SlicePattern currentSlicePattern;

    static Vector2 targetDirection;
    static Vector2 mouseDirection;

    static float currentSliceTime = 0;
    public static float sliceTickLength = 1f/15;

    static float successRequirement = 0.95f;

    private void Awake()
    {
        sliceMode = false;
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (sliceMode)
        {
            SliceTickTimer();
        }
    }

    public static void SetSliceMode(bool status)
    {
        sliceMode = status;

        if (sliceMode)
        {
            instance.GetComponent<PlayerController>().NullifyMovement();
            currentSlicePattern = instance.caughtObject.GetComponentInChildren<SlicePattern>();
            currentSlicePattern.NextSliceArrow();
        }
        else if(currentSlicePattern != null)
        {
            currentSlicePattern.DestroyArrow();
            currentSlicePattern.ResetPattern();
            ClearCaughtObject();
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
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public static void SetCaughtObject(GameObject obj)
    {
        Debug.Log("Caught sliec");
        instance.caughtObject = obj;
    }


    public static void ClearCaughtObject()
    {
        instance.caughtObject = null;
        currentSlicePattern = null;
    }


    public static void SetTargetDirection(Vector2 dir)
    {
        targetDirection = dir.normalized;
        Debug.Log("targetDir:" + targetDirection);
    }


    static void SliceTickTimer()
    {
        currentSliceTime += Time.deltaTime;

        if (currentSliceTime >= sliceTickLength)
        {
            SliceTick();
        }
    }


    static void SliceTick()
    {
        GetMouseDirection();
        CompareSliceDirection();

        currentSliceTime %= sliceTickLength;
    }


    static void GetMouseDirection()
    {
        mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;
    }


    static void CompareSliceDirection()
    {
        if (Vector2.Dot(mouseDirection, targetDirection) >= 0.94f)
        {
            Debug.Log("Success!");
            currentSlicePattern.NextSliceArrow();
        }
    }
}
