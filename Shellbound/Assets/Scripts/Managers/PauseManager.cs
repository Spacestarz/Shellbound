using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance; 

    public static bool isPaused;

    public GameObject pauseScreen;

    // Start is called before the first frame update

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
        
        if (instance == null)
        {
            instance = this;
        }
        pauseScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public static void TogglePause()
    {
        if(!isPaused)
        {
            isPaused = true;
            instance.pauseScreen.SetActive(true);
            MusicManager.instance.musicSource.Pause();
            Time.timeScale = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            isPaused = false;
            instance.pauseScreen.SetActive(false);
            MusicManager.instance.musicSource.UnPause();

            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void QuitClicked()
    {
        SceneController.instance.LoadScene("MainMenu");
    }
}
