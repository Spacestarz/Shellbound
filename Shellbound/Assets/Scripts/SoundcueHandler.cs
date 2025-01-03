using UnityEngine;

public class SoundcueHandler : MonoBehaviour
{
    public AudioSource source;
    public AudioClip waveCue;
    public AudioClip doubleWaveCue;
    public AudioClip fistCue;

    public static SoundcueHandler instance;

    private void Awake()
    {
        if(instance == null || instance != this)
        {
            Destroy(instance);
            instance = this;
        }

        //instance.source = instance.GetComponent<AudioSource>();
    }

    public static void PlayWaveCue()
    {
        instance.StopPlaying();
        instance.source.clip = instance.waveCue;
        instance.source.volume = 0.5f;
        instance.source.Play();
    }

    public static void PlayDoubleWaveCue()
    {
        instance.StopPlaying();
        instance.source.clip = instance.doubleWaveCue;
        instance.source.volume = 0.5f;
        instance.Invoke(nameof(PlayDoubleSound), 1);
    }

    public static void PlayFistCue()
    {
        instance.StopPlaying();

        instance.source.clip = instance.fistCue;
        instance.source.volume = 0.25f;
        instance.source.Play();
    }

    public void StopPlaying()
    {
        instance.source.Stop();
    }

    void PlayDoubleSound()
    {
        instance.source.Play();
    }
}
