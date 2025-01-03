using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NewSceneFadeOut : MonoBehaviour
{
    Image screenImage;
    // Start is called before the first frame update
    void Awake()
    {
        screenImage = GetComponent<Image>();
        screenImage.DOColor(Color.clear, 0.5f).OnComplete(Disable);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
