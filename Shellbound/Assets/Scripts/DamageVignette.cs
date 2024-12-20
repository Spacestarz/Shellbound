using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class DamageVignette : MonoBehaviour
{
    static Volume vignette;

    static float currentWeight;

    void Awake()
    {
        vignette = GetComponent<Volume>();
        currentWeight = vignette.weight;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            ShowVignette();
    }


    public static void ShowVignette()
    {
        DOTween.To(() => currentWeight, x => currentWeight = x, 1, 0.1f)
            .OnUpdate(SetWeight).OnComplete(FadeVignette);
    }

    static void FadeVignette()
    {
        DOTween.To(() => vignette.weight, x => currentWeight = x, 0, 0.5f).OnUpdate(SetWeight);
    }

    static void SetWeight()
    {
        vignette.weight = currentWeight;
    }
}
