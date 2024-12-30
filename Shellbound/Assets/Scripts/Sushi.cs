using UnityEngine;
using DG.Tweening;

public class Sushi : MonoBehaviour
{
    float startY;
    float bobSpeed = 1.25f;
    float bobHeight = 0.5f;

    bool hasMoved;
    bool fadeStarted;

    HookableObject hookableObj;
    AudioSource source;
    float startVolume;

    float shakeStr;

    void Awake()
    {
        startY = transform.position.y;

        hookableObj = GetComponent<HookableObject>();
        source = GetComponent<AudioSource>();
        startVolume = source.volume;

        shakeStr = 0.025f;
    }

    void Update()
    {
        if (!hookableObj.isCaught)
        {
            BobUpAndDown();
        }
        else if (transform.position.y != startY && !hasMoved)
        {
            MoveUp();
        }
    }

    void BobUpAndDown()
    {
        var pos = transform.position;
        var newY = startY + bobHeight * Mathf.Sin(Time.time * bobSpeed);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    void MoveUp()
    {
        hasMoved = true;
        transform.DOMoveY(1, 0.2f);
    }

    public void OutroSlice(int sliceCount)
    {
        if (!OutroManager.instance.whiteScreen.gameObject.activeSelf)
        {
            OutroManager.instance.whiteScreen.color = Color.clear;
            OutroManager.instance.whiteScreen.gameObject.SetActive(true);
        }

        Color startColor = new(1, 1, 1, 0);
        Color goalColor = new(1, 1, 1, 0.8f);

        float colorLerpRate = ((float)sliceCount - 5) / 50;
        float pitchLerpRate = ((float)sliceCount - 10) / 50;
        float volumeLerpRate = ((float)sliceCount - 10) / 75;

        if (sliceCount > 4 && OutroManager.instance.whiteScreen.color != goalColor && !fadeStarted)
        {
            OutroManager.instance.whiteScreen.color = Color.Lerp(startColor, goalColor, colorLerpRate);

            GetComponentInChildren<SlicePattern>().outroSlice = true;

            if (sliceCount > 9)
            {
                Camera.main.GetComponent<CameraHandler>().ShakeCamera(1.3f, new(shakeStr, shakeStr, shakeStr));
                shakeStr += 0.025f;
                source.pitch = Mathf.Lerp(1, 4, pitchLerpRate);
            }
            if (sliceCount == 9)
            {
                PlayerSlice.OutroLowerRequiredTickAmount(5);
            }

            if (sliceCount > 13 && !fadeStarted)
            {
                source.volume = Mathf.Lerp(startVolume, startVolume * 0.5f, volumeLerpRate);
            }

            if (sliceCount == 13)
            {
                PlayerSlice.OutroLowerRequiredTickAmount(3);
                PlayerSlice.OutroLowerRequiredDotProduct(0.8f, 0.75f);
            }

            if (sliceCount == 18)
            {
                PlayerSlice.OutroLowerRequiredTickAmount(1);
                PlayerSlice.OutroLowerRequiredDotProduct(0.6f, 0.4f);
            }
        }
        else if (OutroManager.instance.whiteScreen.color == goalColor && !fadeStarted)
        {
            fadeStarted = true;
            MusicManager.instance.SetSong(1);
            OutroManager.instance.whiteScreen.DOColor(Color.white, MusicManager.instance.musicSource.clip.length);
            source.DOFade(0, MusicManager.instance.musicSource.clip.length).OnComplete(SwitchScene);
            //Invoke(nameof(SwitchScene), MusicManager.instance.musicSource.clip.length);
        }
    }

    void SwitchScene()
    {
        DOTween.KillAll();
        MusicManager.instance.SetSong(2);
        SceneController.instance.LoadScene("VictoryScreen");
    }

}
