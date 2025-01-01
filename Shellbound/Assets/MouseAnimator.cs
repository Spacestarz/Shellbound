using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MouseAnimator : MonoBehaviour
{
    SliceTarget currentArrow;

    public Image[] images;

    public bool showMouseAnim;
    bool isMidAnimation;

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

    public void Update()
    {
        if(showMouseAnim && !isMidAnimation)
        {
            Slice();
        }
    }

    void Click()
    {
        foreach (var image in images)
        {
            image.enabled = !image.enabled;
        }
    }

    public void StartMouseAnim(SliceTarget currArrow)
    {
        currentArrow = currArrow;
        showMouseAnim = true;
    }

    public void Slice()
    {
        SetPositions();
        AnimateMouse();
    }

    void SetPositions()
    {
        startPos = currentArrow.direction.normalized * -150;
        endPos = startPos * -1;

        mouseImgPos.anchoredPosition = startPos;
    }

    void AnimateMouse()
    {
        isMidAnimation = true;
        FadeIn();
        MoveAnchorPos();

        if (images[0].color.a < 1)
        {
            Invoke(nameof(FadeOutImage), 0.69f);
        }
        Invoke(nameof(Loop), 0.8f);
    }


    void FadeIn()
    {
        if (transform.parent.GetComponentInChildren<TextMeshProUGUI>().color.a < 0.1f)
        {
            transform.parent.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.2f);
        }
        images[0].DOFade(1, 0.1f);

    }

    void MoveAnchorPos()
    {
        mouseImgPos.DOAnchorPos(endPos, 0.7f);
    }

    void FadeOutImage()
    {
        images[0].DOFade(0, 0.1f);
    }

    void FadeOutText()
    {
        transform.parent.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 0.4f);
    }

    void Loop()
    {
        isMidAnimation = false;
        mouseImgPos.anchoredPosition = startPos;
    }

    public void EndMouseAnim()
    {
        showMouseAnim = false;

        FadeOutImage();
        FadeOutText();
    }
}

/*
 * kebabpizza, vitlökssås + pommes
 * kebabtallrik, ingen sallad ingen sås
 */