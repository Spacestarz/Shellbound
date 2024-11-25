using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    HealthSystem parentHealth;
    public SlicePattern pattern;

    bool sliceCompleted;
    public SlicePoint[] points;

    public Vector2 direction;

    private void Awake()
    {
        points = GetComponentsInChildren<SlicePoint>();
        parentHealth = GetComponentInParent<HealthSystem>();
        pattern = GetComponentInParent<SlicePattern>();
    }
}
