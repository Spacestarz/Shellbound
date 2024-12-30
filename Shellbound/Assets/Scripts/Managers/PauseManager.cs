using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance; 

    public static bool isPaused;

    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void TogglePause()
    {
        if(!isPaused)
        {
            isPaused = true;
            instance.pauseScreen.SetActive(true);
            Time.timeScale = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            isPaused = false;
            instance.pauseScreen.SetActive(false);
            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
