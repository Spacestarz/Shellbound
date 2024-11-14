using UnityEngine;

public class SliceScript : MonoBehaviour
{
    static bool sliceMode;
    static bool isSlicing;

    static SliceTarget currentSliceTarget;

    private void Start()
    {
        sliceMode = false;
    }

    public void SliceRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.CompareTag("SliceTarget"))
        {
            hit.collider.gameObject.GetComponent<SlicePoint>().CheckIfHittable();
        }

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }

    public void ToggleSliceMode()
    {
        sliceMode = !sliceMode;
        isSlicing = false;

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
            currentSliceTarget = null;
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
}
