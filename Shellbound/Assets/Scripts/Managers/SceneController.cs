using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("GameController");
        if(obj.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GoToMenue();
        }
        else if (Input.GetKey(KeyCode.Backspace))
        {
            restart();
        }
        //debug
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            NextLevel();
        }
    }
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
    public void restart()
    {
        var scean = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scean);
    }
    public void GoToMenue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        var scean = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scean + 1);
    }
}
