using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Slider PlayerSliderobject;
    public Slider EnemySliderObject;
    public HealthSystem PlayerHealthSystem;
    public HealthSystem EnemyHealthSystem;
    public GameObject player;
    public GameObject Enemy;
    private GameObject gameOverScreen;
    private GameObject gameoverBLACK;

    private GameObject youwinScreen;

    public bool gameoverBOOL = false;
    public bool defeatedbossBOOL = false;

    // Start is called before the first frame update
    void Start()
    {
        youwinScreen = GameObject.Find("You win");
        PlayerSliderobject.maxValue = PlayerHealthSystem.MaxHP;
        EnemySliderObject.maxValue = EnemyHealthSystem.MaxHP;
        gameOverScreen = GameObject.Find("Game_Over ");
        gameoverBLACK = GameObject.Find("Background panel");
        youwinScreen.SetActive(false);
        gameoverBLACK.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerSliderobject != null)
        {
            PlayerSliderobject.value = PlayerHealthSystem.currentHP;
            
            if (PlayerSliderobject.value == 0)
            {
                PlayerSliderobject.gameObject.SetActive(false);
            }
        }

        if (EnemySliderObject != null)
        {
            EnemySliderObject.value = EnemyHealthSystem.currentHP;
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
        player.GetComponent<PlayerController>().enabled = false;
        gameoverBLACK.SetActive(true);
        gameOverScreen.SetActive(true);
        gameoverBOOL = true;

        Debug.Log ("game over is" + gameoverBOOL);      
    }

    public void DefeatedBOSS()
    {
        player.GetComponent<PlayerController>().enabled = false;
        gameoverBLACK.SetActive(true);
        youwinScreen.SetActive(true);

        defeatedbossBOOL = true;
        Debug.Log("You killed boss grats");
    }
}
