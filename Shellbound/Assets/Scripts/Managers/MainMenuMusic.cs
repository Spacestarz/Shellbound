using UnityEngine;
using DG.Tweening;


public class MainMenuMusic : MonoBehaviour
{
    AudioSource source;

    [SerializeField] AudioClip waitingMusic;
    public AudioClip introMusic;
    [SerializeField] AudioClip mainMusic;
    public AudioClip startSound;

    bool playButtonPressed;


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


    public void PlayMainMusic()
    {
        if(!playButtonPressed)
        {
            source.Stop();
            source.clip = mainMusic;
            source.Play();
            
        }
    }

    public void PlayStartSound()
    {
        playButtonPressed = true;
        source.Stop();
        source.PlayOneShot(startSound);
    }
}
