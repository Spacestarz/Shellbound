using System.Collections;
using UnityEngine;

public class SliceAnimate : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    SliceTarget arrow;

    void Awake()
    {
        arrow = GetComponentInParent<SlicePattern>().spawnedArrow;
        transform.localEulerAngles = arrow.transform.localEulerAngles;
        transform.Rotate(0, 0, 0);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.enabled = false;
    }

    public void PlayAnimation()
    {
        spriteRenderer.enabled = true;
        StartCoroutine(DestroyAfterWait());
    }

    IEnumerator DestroyAfterWait()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
        yield break;
    }
}
