using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    Enemi_Health parentHealth;
    public SlicePattern pattern;
    public Vector2 direction;


    private void Awake()
    {
        parentHealth = GetComponentInParent<Enemi_Health>();
        pattern = GetComponentInParent<SlicePattern>();
    }

    public void CompleteSlice()
    {
        parentHealth.TakeDamage(1);
        pattern.PlayAudio("finish");
        pattern.spawnedSliceAnimation.PlayAnimation();
    }
}
