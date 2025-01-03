using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [HideInInspector] public float startVolume;
    [HideInInspector] public AudioSource musicSource;
    public AudioSource soundFXSource;

    [SerializeField] AudioClip[] songs;

    int currentSongIndex;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        currentSongIndex = 0;
        musicSource = GetComponent<AudioSource>();
        startVolume = musicSource.volume;

        PlayFightMusicIntro();

        DontDestroyOnLoad(gameObject);
    }

    public void NextSong()
    {
        currentSongIndex++;
        instance.musicSource.clip = songs[currentSongIndex];
        musicSource.Play();
    }

    public void SetSong(int songIndex)
    {
        Debug.Log("Yeah");
        currentSongIndex = songIndex;
        instance.musicSource.clip = songs[currentSongIndex];
        musicSource.Play();
    }

    public void StopPlaying()
    {
        instance.musicSource.Stop();
    }

    void PlayFightMusicIntro()
    {
        instance.musicSource.clip = songs[0];
        instance.musicSource.Play();
        Invoke(nameof(PlayFightMusic), instance.musicSource.clip.length);
    }

    void PlayFightMusic()
    {
        instance.musicSource.Stop();
        instance.musicSource.clip = songs[4];
        instance.musicSource.Play();

    }
}
