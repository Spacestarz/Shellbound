using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    HealthSystem parentHealth;
    public SlicePattern pattern;

    public Vector2 direction;

    private void Awake()
    {
        parentHealth = GetComponentInParent<HealthSystem>();
        pattern = GetComponentInParent<SlicePattern>();
    }

    public void CompleteSlice()
    {
        parentHealth.TakeDamage(1);
    }
}
