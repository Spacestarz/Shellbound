using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    AudioSource source;
    [SerializeField] AudioClip[] songs;

    int currentSongIndex;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        currentSongIndex = 0;
        source = GetComponent<AudioSource>();

        source.clip = songs[currentSongIndex];
        source.Play();
    }

    void NextSong()
    {
        currentSongIndex++;
        instance.source.clip = songs[currentSongIndex];
        source.Play();
    }

    void SetSong(int songIndex)
    {
        currentSongIndex = songIndex;
        instance.source.clip = songs[currentSongIndex];
        source.Play();
    }

    public void StopPlaying()
    {
        instance.source.Stop();
    }
}
