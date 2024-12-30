using System.Collections;           //DO NOT DELETE
using System.Collections.Generic;   //OH GOD OH FUCK
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public static bool gameStarted;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        //GameObject[] obj = GameObject.FindGameObjectsWithTag("GameController");
        //if(obj.Length > 1)
        //{
        //    Destroy(gameObject);
        //}
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        /*if (Input.GetKey(KeyCode.Escape))
        {
            GoToMenue();
        }
        else*/ if (Input.GetKey(KeyCode.Backspace))
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
        Debug.Log("Screee");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
                //Restart*
    public void restart()
    {
        //scene*
        var scean = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scean);
    }
    public void GoToMenue()
    {
        ShowCursor();

        if(GameObject.Find("MusicManager"))
        {
            Destroy(GameObject.Find("MusicManager"));
        }
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
            //scene*
        var scean = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(scean + 1);
    }

    public void LoadScene(string scene)
    {
        if (!scene.Contains("MainScene"))
        {
            ShowCursor();
        }

        SceneManager.LoadScene(scene);
    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
