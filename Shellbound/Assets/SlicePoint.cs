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
        if (!hasBeenHit)
        {
            parent.ControlSlicePoint(this);
        }
    }

    public void GetHit()
    {
        Debug.Log("Hit!");
        GetComponent<SpriteRenderer>().enabled = true;
        hasBeenHit = true;
    }

    public void ResetHit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Unhit!");
        hasBeenHit = false;
    }

    public bool HasBeenHit()
    {
        return hasBeenHit;
    }
}
