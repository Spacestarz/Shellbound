using UnityEngine;

public class SlicePoint : MonoBehaviour
{
    bool hasBeenHit;
    SliceTarget parent;

    private void Awake()
    {
        parent = GetComponentInParent<SliceTarget>();
    }
    public void CheckIfHittable()
    {
        parent.ControlSlicePoint(this);
    }

    public void GetHit()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        hasBeenHit = true;
    }

    public void ResetHit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        hasBeenHit = false;
    }

    public bool HasBeenHit()
    {
        return hasBeenHit;
    }
}
