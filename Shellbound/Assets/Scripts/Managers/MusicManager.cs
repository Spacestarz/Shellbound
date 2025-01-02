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

        musicSource.clip = songs[currentSongIndex];
        musicSource.Play();

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
}
