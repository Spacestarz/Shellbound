using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    //public Slider PlayerSliderobject;       //Line breaks to separate these variable declarations would be helpful
    //public Slider EnemySliderObject;        //Right now it all kinda blends together and it's hard to see what
    GameObject player;                      //Goes together with what
    public GameObject Enemy;    //enemy*
    private GameObject gameOverScreen;
    private GameObject gameoverBLACK;

    private GameObject youWin;


    public bool gameoverBOOL = false;       //gameIsOver
    public bool defeatedbossBOOL = false;   //bossIsDefeated(?)
  
    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player");

        //if (PlayerSliderobject != null)
        //    PlayerSliderobject.maxValue = player.GetComponent<HealthSystem>().MaxHP;
        // if (EnemySliderObject != null)
        //EnemySliderObject.maxValue = Enemy.GetComponent<HealthSystem>().MaxHP;
        gameOverScreen = GameObject.Find("Game_Over ");
        youWin = GameObject.Find("You Win");
        
        gameoverBLACK = GameObject.Find("Background panel");
        gameoverBLACK.SetActive(false);
        gameOverScreen.SetActive(false);

        youWin.SetActive(false);
    }

    void Update()
    {
        //if (PlayerSliderobject != null)
        //{
        //    PlayerSliderobject.value = player.GetComponent<HealthSystem>().currentHP;

        //    if (PlayerSliderobject.value == 0)
        //    {
        //        PlayerSliderobject.gameObject.SetActive(false);
        //    }
        //}

        //if (EnemySliderObject != null)
        //{
        //    EnemySliderObject.value = Enemy.GetComponent<HealthSystem>().currentHP;
        //}

        if ((Input.GetKeyDown(KeyCode.Space) && defeatedbossBOOL == true))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /* //added to be able to defeat boss if you want to debug
        if (Input.GetKeyDown(KeyCode.X))
        {
            DefeatedBOSS();
        }
        */
    }


    public void GameOver()
    {
        player.GetComponent<PlayerController>().enabled = false;
        gameoverBLACK.SetActive(true);
        gameOverScreen.SetActive(true);
        gameoverBOOL = true;
    }

    public void DefeatedBOSS()
    {
        //player.GetComponent<PlayerController>().enabled = false;
        //gameoverBLACK.SetActive(true);

        youWin.SetActive(true);
        defeatedbossBOOL = true;
        Invoke(nameof(BackToMainMenu), 5);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}



