using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    public static OutroManager instance;

    [SerializeField] Image whiteScreen;

    public static bool outroRunning;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public static void StartOutro(bool victory)
    {
        if (victory)
        {
            MusicManager.instance.StopPlaying();
            Camera.main.GetComponent<RotateCamera>().isLocked = true;
            Vector3 shakeStrength = new(1.25f, 3.75f, 0);
            Camera.main.GetComponent<CameraHandler>().ShakeCamera(4, shakeStrength);
            instance.Invoke(nameof(VictoryWhiteFadeIn), 3f);

            outroRunning = true;
        }
    }

#region Victory
    void VictoryWhiteFadeIn()
    {
        Debug.Log("FADE");
        instance.whiteScreen.color = Color.clear;
        instance.whiteScreen.gameObject.SetActive(true);
        instance.whiteScreen.DOColor(Color.white, 0.75f).OnComplete(InvokeFadeOut);
    }

    void InvokeFadeOut()
    {
        Invoke(nameof(VictoryFadeOut), 1.2f);
    }

    void VictoryFadeOut()
    {
        instance.whiteScreen.DOColor(Color.clear, 1.25f);
    }

    void EnableMovement()
    {

    }
#endregion
}
