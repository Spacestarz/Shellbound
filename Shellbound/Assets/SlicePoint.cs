using UnityEngine;

public class SlicePoint : MonoBehaviour
{
    bool hasBeenHit;
    SliceTarget parent;

    public void GetHit()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        if (!hasBeenHit)
        {
            Debug.Log("Hit!");
            parent = GetComponentInParent<SliceTarget>();
            hasBeenHit = true;
        }
    }

    public void ResetHit()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log("Unhit!");
        hasBeenHit = false;
    }

    public bool HasBeenHit()
    {
        return hasBeenHit;
    }
}
