using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    public static OutroManager instance;

    public Image whiteScreen;

    public static bool isRunning;
    public static bool sushiIsHooked;

    public Crowd_attacks spikeAttack;

    GameObject boss;

    public AudioClip bossRoar;

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

        isRunning = false;
        boss = GameObject.Find("MantisShrimp");
    }

    public static void StartOutro(bool victory)
    {
        isRunning = true;
        instance.spikeAttack.gameObject.SetActive(false);

        if (victory)
        {
            MusicManager.instance.StopPlaying();
            UrchinSpawner.RemoveAllFromList();

            Camera.main.GetComponent<RotateCamera>().isLocked = true;
            Camera.main.GetComponentInParent<PlayerController>().enabled = false;

            Vector3 shakeStrength = new(1.25f, 2.75f, 0);
            Camera.main.GetComponent<CameraHandler>().ShakeCamera(4, shakeStrength);

            instance.Invoke(nameof(VictoryWhiteFadeIn), 3f);

        }
        else
        {
            instance.FailureBossRoar();

            instance.FailureFadeAudio();

            instance.FailureFallOver();
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

    public static void SetSushiHooked()
    {
        sushiIsHooked = true;
    }
    #endregion

    #region Failure
    void FailureBossRoar()
    {
        Camera.main.GetComponentInParent<PlayerController>().enabled = false;
        instance.boss.GetComponent<NavMeshAgent>().enabled = false;
        instance.boss.GetComponent<Boss1_AI>().enabled = false;
        MusicManager.instance.soundFXSource.pitch = 0.8f;
        MusicManager.instance.soundFXSource.PlayOneShot(instance.bossRoar);
        instance.boss.GetComponentInChildren<Animator>().SetTrigger("NewPhase");
    }

    void FailureFadeAudio()
    {
        MusicManager.instance.musicSource.DOFade(0, 5f);
        MusicManager.instance.musicSource.DOPitch(0.25f, 5f);

        instance.Invoke(nameof(FailureBlackFadeIn), 3);
    }

    void FailureFallOver()
    {
        Vector3 camRot = Camera.main.transform.localEulerAngles;
        Vector3 camPos = Camera.main.transform.localPosition;

        Camera.main.transform.DOLocalRotate(new Vector3(camRot.x, camRot.y, -90), 3f);
        Camera.main.transform.DOLocalMove(new Vector3(camPos.x, 0.35f, camPos.z), 1.5f);
    }

    void FailureBlackFadeIn()
    {
        MusicManager.instance.musicSource.Stop();
        
        instance.whiteScreen.color = Color.clear;
        instance.whiteScreen.gameObject.SetActive(true);
        instance.whiteScreen.DOColor(Color.black, 1.5f);
        Invoke(nameof(SwitchScene), 1.8f);
    }

    void SwitchScene()
    {
        DOTween.KillAll();
        MusicManager.instance.musicSource.volume = MusicManager.instance.startVolume;
        MusicManager.instance.musicSource.pitch = 1;
        MusicManager.instance.SetSong(3);

        SceneController.instance.LoadScene("GameOverScreen");
    }
    #endregion
}
