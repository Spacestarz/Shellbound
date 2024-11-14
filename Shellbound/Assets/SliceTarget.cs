using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    public SlicePoint[] points;

    void ResetSlice()
    {
        foreach (SlicePoint point in points)
        {
            if (point.HasBeenHit())
            {
                point.ResetHit();
            }
        }
    }

    void CheckSliceStatus()
    {
        bool allHit = true;
        foreach (SlicePoint point in points)
        {
           if (!point.HasBeenHit())
            {
                allHit = false;
            }
        }

        if (allHit)
        {
            DefeatedSlice();
        }
    }

    void DefeatedSlice()
    {
        Debug.Log("Slice defeated!");
    }
}
