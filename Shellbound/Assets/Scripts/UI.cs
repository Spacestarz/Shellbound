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
    GameObject player;
    public GameObject Enemy;
    private GameObject gameOverScreen;
    private GameObject gameoverBLACK;

    private GameObject youwinScreen;

    public bool gameoverBOOL = false;
    public bool defeatedbossBOOL = false;
  
    // Start is called before the first frame update
    void Start()
    {
       
      player = GameObject.FindGameObjectWithTag("Player");

      youwinScreen = GameObject.Find("You win");

      if (PlayerSliderobject != null)
          PlayerSliderobject.maxValue = player.GetComponent<HealthSystem>().MaxHP;
       if (EnemySliderObject != null)
      EnemySliderObject.maxValue = Enemy.GetComponent<HealthSystem>().MaxHP;
      gameOverScreen = GameObject.Find("Game_Over ");
      gameoverBLACK = GameObject.Find("Background panel");
      youwinScreen.SetActive(false);
      gameoverBLACK.SetActive(false);
      gameOverScreen.SetActive(false);
    
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSliderobject != null)
        {
            PlayerSliderobject.value = player.GetComponent<HealthSystem>().currentHP;

            if (PlayerSliderobject.value == 0)
            {
                PlayerSliderobject.gameObject.SetActive(false);
            }
        }

        if (EnemySliderObject != null)
        {
            EnemySliderObject.value = Enemy.GetComponent<HealthSystem>().currentHP;
        }

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
    }

    public void DefeatedBOSS()
    {
        player.GetComponent<PlayerController>().enabled = false;
        gameoverBLACK.SetActive(true);
        youwinScreen.SetActive(true);

        defeatedbossBOOL = true;
    }


}



