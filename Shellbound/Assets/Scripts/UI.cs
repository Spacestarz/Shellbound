using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider Sliderobject;
    public HealthSystem HealthSystem;
    public GameObject player;
    private GameObject gameOverScreen;
    private GameObject gameoverBLACK;

    public bool gameoverBOOL = false;

    // Start is called before the first frame update
    void Start()
    {
        Sliderobject.maxValue = HealthSystem.MaxHP;
        gameOverScreen = GameObject.Find("Game_Over ");
        gameoverBLACK = GameObject.Find("Background panel");
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

        if (gameoverBOOL == true)
        {
            Debug.Log ("Game over bool is " + gameoverBOOL);
        }
      
        if (Input.GetKeyDown(KeyCode.V) )
        {
            Debug.Log("Restart the fight");
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
}
