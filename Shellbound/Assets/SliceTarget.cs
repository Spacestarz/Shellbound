using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    HealthSystem parentHealth;

    bool sliceCompleted; 
    public SlicePoint[] points;

    private void Awake()
    {
        parentHealth = GetComponentInParent<HealthSystem>();
    }

    public void ResetSlice()
    {
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
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != currentPoint)
            {
                continue;
            }
            if (i > 0 && points[i - 1].HasBeenHit())
            {
                currentPoint.GetHit();
                if (i == points.Length - 1)
                {
                    Debug.Log("Slice Completed!");
                    sliceCompleted = true;
                    parentHealth.TakeDamage(5);
                }
            }
            else if (i == 0)
            {
                SliceScript.SetCurrentSliceTarget(this);
                currentPoint.GetHit();
            }
            else
            {
                Debug.Log("Can't slice");
            }
        }
    }
}
