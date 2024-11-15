using UnityEngine;

public class PlayerSlice : MonoBehaviour
{
    static bool sliceMode;
    static bool isSlicing;
    static bool inSliceArea;

    static SliceTarget currentSliceTarget;

    private void Start()
    {
        sliceMode = false;
    }

    public void SliceRayCast()
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
        else
        {
            //If the player leaves the slice area mid-slice...
            if (inSliceArea)
            {
                inSliceArea = false;

                if (currentSliceTarget != null)
                {
                    currentSliceTarget.ResetSlice();
                }
            }
        }
    }

    public void ToggleSliceMode()
    {
        sliceMode = !sliceMode;
        isSlicing = false;
        inSliceArea = false;

        if (currentSliceTarget != null)
        {
            currentSliceTarget.ResetSlice();
        }

        ToggleCursor();
    }

    public static bool SliceMode()
    {
        return sliceMode;
    }

    public void ToggleIsSlicing()
    {
        isSlicing = !isSlicing;

        if (!isSlicing && currentSliceTarget)
        {
            currentSliceTarget.ResetSlice();
        }
    }

    void ToggleCursor()
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
        currentSliceTarget = sliceTarget;
    }

    public static void ClearCurrentSliceTarget()
    {
        currentSliceTarget = null;
    }
}
