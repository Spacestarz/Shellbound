using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    SliceableObject parentSlice;
    SpriteRenderer spriteRenderer;
    public SlicePattern pattern;
    public Vector2 direction;

    public GameObject circle;
    Vector2 circleSpawn;
    Vector2 circleGoal;

    private void Awake()
    {
        pattern = GetComponentInParent<SlicePattern>();
        parentSlice = transform.parent.GetComponentInParent<SliceableObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circle = transform.GetChild(0).GetChild(0).gameObject;

        circleSpawn = circle.transform.localPosition;
        circleGoal = circle.transform.localPosition *= -1;
    }

    public void CompleteSlice(Vector2 dir)
    {
        parentSlice.GetComponent<SliceableObject>().SingleSlice();
        pattern.PlayAudio("finish");
        pattern.spawnedSliceAnimation.PlayAnimation();
    }

    public void TurnRed(float a, float b)
    {
        spriteRenderer.color = Color.Lerp(Color.white, Color.green, a/b);
        circle.transform.localPosition = Vector2.Lerp(circleSpawn, circleGoal, a/b);
    }
}
