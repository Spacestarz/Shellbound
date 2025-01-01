using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MouseAnimator : MonoBehaviour
{
    public Image[] images;

    Vector2 startPos;
    Vector2 endPos;

    RectTransform mouseImgPos;

    private void Awake()
    {
        mouseImgPos = transform.GetChild(0).GetComponent<RectTransform>();

        if (images.Length > 1)
        {
            images[0].enabled = true;
            images[1].enabled = false;
        }
    }

    void Click()
    {
        foreach (var image in images)
        {
            image.enabled = !image.enabled;
        }
    }

    public void Slice(SliceTarget currentArrow)
    {
        SetPositions(currentArrow);
    }

    void SetPositions(SliceTarget currentArrow)
    {
        startPos = currentArrow.direction * 150;
        endPos = startPos * -1;
        mouseImgPos.anchoredPosition = startPos;
    }

    void AnimateMouse()
    {
    }

    void MoveAnchorPos()
    {
        mouseImgPos.DOAnchorPos(endPos, 0.75f);
    }

    void FadeIn()
    {
        if (GetComponentInChildren<TextMeshProUGUI>().alpha < 1)
        {
            GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.4f);
        }
        images[0].DOFade(1, 04f);
    }
}

/*
 * kebabpizza, vitlökssås + pommes
 * kebabtallrik, ingen sallad ingen sås
 */