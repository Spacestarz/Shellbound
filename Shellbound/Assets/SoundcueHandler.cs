using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundcueHandler : MonoBehaviour
{
    AudioSource source;
    public AudioClip waveCue;
    public AudioClip fistCue;

    public static SoundcueHandler instance;

    private void Awake()
    {
        if(instance == null || instance != this)
        {
            Destroy(instance);
            instance = this;
        }

        instance.source = instance.GetComponentInParent<AudioSource>();
    }

    public static void PlayWaveCue()
    {
        instance.source.PlayOneShot(instance.waveCue);
    }

    public static void PlayFistCue()
    {
        instance.source.PlayOneShot(instance.fistCue);
    }
}
