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
    static float sliceTickLength = 1f/30;

    static float successRequirement = 0.8f;

    static int succeededTicks = 0;
    public static int requiredTicks = 3;

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
            Debug.DrawLine((Vector2)caughtObject.transform.position+new Vector2(1,1), (Vector2)caughtObject.transform.position+targetDirection + new Vector2(1, 1), Color.red);
            Debug.DrawLine((Vector2)caughtObject.transform.position + new Vector2(1, 1), (Vector2)caughtObject.transform.position+mouseDirection + new Vector2(1, 1), Color.white);
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
        
        if (targetDirection.x != 0 ^ targetDirection.y != 0)
        {
            successRequirement = 0.8f;
        }
        else
        {
            successRequirement = 0.6f;
        }
        Debug.Log(successRequirement);
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
            succeededTicks++;

            if (succeededTicks >= requiredTicks)
            {
                CompleteSlice();
            }
        }
        else
        {
            succeededTicks = 0;
        }

       
    }

    static void CompleteSlice()
    {
        currentSlicePattern.spawnedArrow.CompleteSlice();
        currentSlicePattern.NextSliceArrow();
        succeededTicks = 0;
    }
}
