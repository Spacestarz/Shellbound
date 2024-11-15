using System;
using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    HealthSystem parentHealth;

    bool sliceCompleted;
    public SlicePoint[] points;

    private void Awake()
    {
        points = GetComponentsInChildren<SlicePoint>();
        parentHealth = GetComponentInParent<HealthSystem>();
    }

    public void ResetSlice()
    {
        sliceCompleted = false;

        foreach (SlicePoint point in points)
        {
            if (point.HasBeenHit())
            {
                point.ResetHit();
            }
        }
    }

    public void ControlSlicePoint(SlicePoint currentPoint)
    {
        Debug.Log("Aiming at: " + Array.IndexOf(points, currentPoint));
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != currentPoint)
            {
                continue;
            }
            //Make sure nothing after this point has been hit (One way)
            if (i + 1 < points.Length && points[i + 1].HasBeenHit())
            {
                Debug.Log("No return");
                ResetSlice();
            }

            //Make sure everything before this point has been hit
            if (i > 0 && points[i - 1].HasBeenHit())
            {
                currentPoint.GetHit();

                //If it's the last point...
                if (i == points.Length - 1 && !sliceCompleted)
                {
                    sliceCompleted = true;
                    parentHealth.TakeDamage(5);

                    Invoke(nameof(ResetSlice), 0.05f);
                }
            }
            //If it's the first point...
            else if (i == 0)
            {
                PlayerSlice.SetCurrentSliceTarget(this);
                currentPoint.GetHit();
            }
        }
    }
}
