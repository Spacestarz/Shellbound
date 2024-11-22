using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Slider Sliderobject;
    public HealthSystem HealthSystem;
    public GameObject player;
    private GameObject gameOverScreen;
    private GameObject gameoverBLACK;

    private GameObject youwinScreen;

    public bool gameoverBOOL = false;
    public bool defeatedbossBOOL = false;

    // Start is called before the first frame update
    void Start()
    {
        youwinScreen = GameObject.Find("You win");
        Sliderobject.maxValue = HealthSystem.MaxHP;
        gameOverScreen = GameObject.Find("Game_Over ");
        gameoverBLACK = GameObject.Find("Background panel");
        youwinScreen.SetActive(false);
        gameoverBLACK.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Sliderobject.value = HealthSystem.currentHP;

        if (Sliderobject.value == 0)
        {
            Sliderobject.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Restart the fight");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        /* //added to be able to defeat boss if you want to debug
        if (Input.GetKeyDown(KeyCode.X))
        {
            DefeatedBOSS();
        }
        */
        if ((Input.GetKeyDown(KeyCode.Space) && defeatedbossBOOL == true))
        {
            Debug.Log("Restart the fight");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        player.SetActive(false);
        gameoverBLACK.SetActive(true);
        gameOverScreen.SetActive(true);
        gameoverBOOL = true;

        Debug.Log ("game over is" + gameoverBOOL);      
    }

    public void DefeatedBOSS()
    {
        player.SetActive(false);
        gameoverBLACK.SetActive(true);
        player.SetActive (false);
        youwinScreen.SetActive(true);

        defeatedbossBOOL = true;
        Debug.Log("You killed boss grats");
    }
}
