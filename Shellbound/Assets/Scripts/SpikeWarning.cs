using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpikeWarning : MonoBehaviour
{
    public static SpikeWarning instance;
    Image gradient;

    Color originalColor;
    
    float defaultFadeTime = 1f;

    void Awake()
    {
        instance = this;

        gradient = GetComponent<Image>();

        originalColor = gradient.color;
        gradient.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    public void StartFadeIn()
    {
        gradient.DOKill();
        float fadeTime = (originalColor.a - gradient.color.a) * defaultFadeTime;
        gradient.DOFade(originalColor.a, fadeTime);
    }

    public void StartFadeOut()
    {
        gradient.DOKill();
        float fadeTime = gradient.color.a * defaultFadeTime;
        gradient.DOFade(0, fadeTime);
    }
}
