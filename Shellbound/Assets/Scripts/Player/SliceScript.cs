using UnityEngine;

public class SliceScript : MonoBehaviour
{
    static bool sliceMode;
    static bool isMidSlice;

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
            Debug.Log("Whammy!");
            hit.collider.gameObject.GetComponent<SlicePoint>().GetHit();
        }

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }

    public void ToggleSliceMode()
    {
        sliceMode = !sliceMode;
        isMidSlice = false;

        CursorToggle();
    }

    public static bool SliceMode()
    {
        return sliceMode;
    }

    public void ToggleIsMidSlice()
    {
        isMidSlice = !isMidSlice;
    }

    void CursorToggle()
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


}
