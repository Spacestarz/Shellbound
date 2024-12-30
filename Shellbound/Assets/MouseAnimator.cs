using UnityEngine;
using UnityEngine.UI;

public class MouseAnimator : MonoBehaviour
{
    public enum Anim
    {
        Click,
        Slice
    }

    public Anim anim;

    public Image[] images;

    void Start()
    {
        images[0].enabled = true;
        images[1].enabled = false;
    }

    void Click()
    {
        foreach(var image in images)
        {
            image.enabled = !image.enabled;
        }
    }
}
