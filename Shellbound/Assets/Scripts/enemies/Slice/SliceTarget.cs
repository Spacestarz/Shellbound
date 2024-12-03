using Unity.VisualScripting;
using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    public SlicePattern pattern;
    SliceableObject parentSlice;
    public Vector2 direction;
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        pattern = GetComponentInParent<SlicePattern>();
        parentSlice = transform.parent.GetComponentInParent<SliceableObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CompleteSlice()
    {
        parentSlice.GetComponent<SliceableObject>().SingleSlice();
        pattern.PlayAudio("finish");
        pattern.spawnedSliceAnimation.PlayAnimation();
    }

    public void TurnRed(float a, float b)
    {
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, a/b);
    }

}
