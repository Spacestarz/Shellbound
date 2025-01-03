using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance; 

    public static bool isPaused;

    public GameObject pauseScreen;

    // Start is called before the first frame update
    public Button quitButton;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        instance.quitButton.onClick.AddListener(QuitClicked);
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
        SceneController.instance.GoToMenue();
    }
}
