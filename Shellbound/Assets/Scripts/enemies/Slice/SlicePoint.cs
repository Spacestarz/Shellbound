using UnityEngine;

public class SlicePoint : MonoBehaviour
{
    bool hasBeenHit;
    SliceTarget parent;

    private void Awake()
    {
        parent = GetComponentInParent<SliceTarget>();
        GetComponent<SpriteRenderer>().sortingLayerID = GetComponentInParent<SpriteRenderer>().sortingLayerID;
    }
}
