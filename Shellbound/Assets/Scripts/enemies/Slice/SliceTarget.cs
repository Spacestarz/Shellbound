using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    public SlicePattern pattern;
    SliceableObject parentSlice;
    public Vector2 direction;


    private void Awake()
    {
        pattern = GetComponentInParent<SlicePattern>();
        parentSlice = transform.parent.GetComponentInParent<SliceableObject>();
    }

    public void CompleteSlice()
    {
        parentSlice.GetComponent<SliceableObject>().SingleSlice();
        pattern.PlayAudio("finish");
        pattern.spawnedSliceAnimation.PlayAnimation();
    }
}
