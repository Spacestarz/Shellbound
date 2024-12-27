using UnityEngine;
using DG.Tweening;


public class MainMenuMusic : MonoBehaviour
{
    AudioSource source;

    [SerializeField] AudioClip waitingMusic;
    public AudioClip introMusic;
    [SerializeField] AudioClip mainMusic;
    public AudioClip startSound;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = waitingMusic;
        source.volume = 0.5f;
        source.Play();
    }


    public void PlayIntro()
    {
        float duration = introMusic.length;
        source.DOFade(0.75f, duration);
        source.PlayOneShot(introMusic);
        Invoke(nameof(PlayMainMusic), duration);
    }


    void PlayMainMusic()
    {
        source.Stop();
        source.clip = mainMusic;
        source.Play();
    }

    public void PlayStartSound()
    {
        source.Stop();
        source.PlayOneShot(startSound);
    }
}