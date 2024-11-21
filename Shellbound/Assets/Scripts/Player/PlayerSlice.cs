using UnityEngine;

public class PlayerSlice : MonoBehaviour
{
    static bool sliceMode;
    static bool isSlicing;
    static bool inSliceArea;

    public static bool activatedThisFrame;
    public static bool deactivatedThisFrame;

    static SliceTarget currentSliceTarget;

    private void Start()
    {
        sliceMode = false;
    }

    private void LateUpdate()
    {
        if (activatedThisFrame)
        {
            Debug.Log("activated");
            activatedThisFrame = false;
        }
        else if (deactivatedThisFrame)
        {
            Debug.Log("deactivated");
            deactivatedThisFrame = false;
        }
    }

    public static void SliceRayCast()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Sliceable")) && hit.collider.CompareTag("SliceTarget"))
        {
            hit.collider.gameObject.GetComponent<SlicePoint>().CheckIfHittable();

            if (!inSliceArea)
            {
                inSliceArea = true;
            }
        }
        else if(inSliceArea)
        {
            //If the player leaves the slice area mid-slice...
            inSliceArea = false;

            if (currentSliceTarget != null)
            {
                currentSliceTarget.ResetSlice();
            }
        }
    }

    public static void SetSliceMode(bool status)
    {
        sliceMode = status;//!sliceMode;
        isSlicing = false;
        inSliceArea = false;

        if (currentSliceTarget != null)
        {
            currentSliceTarget.ResetSlice();
        }

        ToggleCursor();

        if (sliceMode)
        {
            activatedThisFrame = true;
        }
        else
        {
            deactivatedThisFrame = true;
        }
    }

    public static bool SliceMode()
    {
        return sliceMode;
    }

    public static void SetIsSlicing(bool status)
    {
        isSlicing = status;

        if (!isSlicing && currentSliceTarget)
        {
            currentSliceTarget.ResetSlice();
        }
    }

    static void ToggleCursor()
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

    public static void SetCurrentSliceTarget(SliceTarget sliceTarget)
    {
        if (currentSliceTarget != null)
        {
            ClearCurrentSliceTarget();
        }
        currentSliceTarget = sliceTarget;
    }

    public static void ClearCurrentSliceTarget()
    {
        currentSliceTarget = null;
    }
}
