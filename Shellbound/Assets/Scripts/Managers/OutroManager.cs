using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    public static OutroManager instance;

    public Image whiteScreen;

    public static bool isRunning;

    public Crowd_attacks spikeAttack;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public static void StartOutro(bool victory)
    {
        instance.spikeAttack.gameObject.SetActive(false);
        UrchinSpawner.RemoveAllFromList();
        
        if (victory)
        {
            isRunning = true;
            
            MusicManager.instance.StopPlaying();

            Camera.main.GetComponent<RotateCamera>().isLocked = true;
            Camera.main.GetComponentInParent<PlayerController>().enabled = false;
            
            Vector3 shakeStrength = new(1.25f, 2.75f, 0);
            Camera.main.GetComponent<CameraHandler>().ShakeCamera(4, shakeStrength);
            
            instance.Invoke(nameof(VictoryWhiteFadeIn), 3f);

        }
    }

#region Victory
    void VictoryWhiteFadeIn()
    {
        instance.whiteScreen.color = Color.clear;
        instance.whiteScreen.gameObject.SetActive(true);
        instance.whiteScreen.DOColor(Color.white, 0.75f).OnComplete(InvokeFadeOut);
    }

    void InvokeFadeOut()
    {
        PlayerSlice.instance.GetComponent<PlayerController>().harpoonTime = true;
        Invoke(nameof(VictoryFadeOut), 1.2f);
    }

    void VictoryFadeOut()
    {
        PlayerSlice.instance.GetComponent<PlayerController>().harpoonTime = true;
        instance.whiteScreen.DOColor(Color.clear, 1.25f).OnComplete(EnableMovement);
    }

    void EnableMovement()
    {
        isRunning = false;
        PlayerSlice.instance.GetComponent<PlayerController>().harpoonTime = true;
        Camera.main.GetComponentInParent<PlayerController>().enabled = true;
        Camera.main.GetComponent<RotateCamera>().isLocked = false;
    }
    #endregion
}
