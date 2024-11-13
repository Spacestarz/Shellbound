using UnityEngine;

public class SliceScript : MonoBehaviour
{
    public static bool sliceMode;

    private void Start()
    {
        sliceMode = false;
    }
    
    public void ToggleSliceMode()
    {
        sliceMode = !sliceMode;
        CursorToggle();
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
