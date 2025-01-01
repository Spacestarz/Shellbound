using Spine.Unity;
using UnityEngine;
using DG.Tweening;

public class DamageFlash : MonoBehaviour
{
    SpriteRenderer sprite;

    Color originalColor;

    float currentRed;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }


    public void FlashRed()
    {
        SpriteTurnRed();
    }


    void SpriteTurnRed()
    {
        sprite.DOKill();
        sprite.DOColor(Color.red, 0.01f).OnComplete(SpriteReturn);
    }


    void SpriteReturn()
    {
        sprite.DOColor(originalColor, 0.15f);
    }
}
