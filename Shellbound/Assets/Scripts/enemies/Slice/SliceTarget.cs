using UnityEngine;

public class SliceTarget : MonoBehaviour
{
    public SlicePattern pattern;
    SliceableObject parentSlice;
    public Vector2 direction;
    SpriteRenderer spriteRenderer;

    public GameObject circle;
    Transform circleSpawn;
    Transform circleGoal;

    private void Awake()
    {
        pattern = GetComponentInParent<SlicePattern>();
        parentSlice = transform.parent.GetComponentInParent<SliceableObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circle = transform.GetChild(0).GetChild(0).gameObject;

        circleSpawn.position = circle.transform.localPosition;
        circleGoal.position = circle.transform.localPosition *= -1;
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
        circle.transform.position = Vector2.Lerp(circleSpawn.localPosition, circleGoal.localPosition, a/b);
    }
}
