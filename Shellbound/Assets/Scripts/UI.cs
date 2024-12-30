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
    private GameObject gameoverBLACK;


    public bool gameoverBOOL = false;       //gameIsOver
    public bool defeatedbossBOOL = false;   //bossIsDefeated(?)
  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        gameoverBLACK = GameObject.Find("Background panel");
        //gameoverBLACK.SetActive(false);
    }

    public void DefeatedBOSS()
    {
        //player.GetComponent<PlayerController>().enabled = false;
        //gameoverBLACK.SetActive(true);

        defeatedbossBOOL = true;
        Invoke(nameof(BackToMainMenu), 5);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}



