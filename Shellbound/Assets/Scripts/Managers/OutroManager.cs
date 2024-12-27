using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    HealthSystem caller;

    public static OutroManager instance;
    public static bool outroRunning;

    [SerializeField] Image whiteScreen;

    void Start()
    {
        if (instance == null || instance != this)
        {
            Destroy(instance);
            instance = this;
        }
    }


    public static void StartOutro(bool victory, HealthSystem _caller)
    {
        if (victory)
        {
            instance.caller = _caller;
            Vector3 shakeStrength = new(1.25f, 3.75f, 0);
            Camera.main.GetComponent<CameraHandler>().ShakeCamera(4, shakeStrength);
            instance.Invoke(nameof(VictoryWhiteFadeIn), 3);

            outroRunning = true;
        }
    }

    #region Victory
    void VictoryWhiteFadeIn()
    {
        Debug.Log("FADE");
        instance.whiteScreen.color = Color.clear;
        instance.whiteScreen.gameObject.SetActive(true);
        instance.whiteScreen.DOColor(Color.white, 0.5f).OnComplete(InvokeFadeOut);
    }

    void InvokeFadeOut()
    {
        Invoke(nameof(VictoryFadeOut), 0.75f);
        instance.caller.dead();
    }

    void VictoryFadeOut()
    {
        instance.whiteScreen.DOColor(Color.clear, 1.25f);
    }
    #endregion
}
