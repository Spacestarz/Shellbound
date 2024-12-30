using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private static float timer = 0;
    private string playerName = "insert player name";
    /*
     * Make so we can have top 5 high scores
     * 
     * */
    // Start is called before the first frame update
    void Start()
    {
        TimerRunning = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StopTimer();        
        }

      

        if (TimerRunning)
        {
            timer += Time.deltaTime;
            
        }
    }

    public void StopTimer()
    {
        TimerRunning = false;
       
       // timerText.text = "Your time to kill this boss was" + " " + timer.ToString("F0");
        Debug.Log("your time was" + " " + timer.ToString("F2"));

       // PlayerPrefs.SetFloat(bestTimerString, timer);
        PlayerPrefs.Save();
       // Debug.Log("your Best Timer: " + bestTime.ToString("F0"));

        //add score to highscoremanager
        HighScoreManager.instance.AddScore(playerName, timer);

        SaveNewTime(playerName, timer); //need to make so player can write their name


        /*
        if (timer < bestTime)
        {
            bestTime = timer;
            PlayerPrefs.SetFloat(bestTimerString, bestTime);
            PlayerPrefs.Save();
            Debug.Log("New Best Timer: " + bestTime.ToString("F0"));

            SaveNewTime("player name" , bestTime ); //need to make so player can write their name

        }     
        */
        //save string in const variable
    }

    public void SaveNewTime(string playername, float newTime)
    {
        Debug.Log("savenewtime method");

        // Retrieve the current count of high scores from PlayerPrefs
        int highScoreCount = PlayerPrefs.GetInt("HighScoreCount", 0);

        // Save the player's name and time for this score
        PlayerPrefs.SetString($"HighScoreName{highScoreCount}", playername);
        PlayerPrefs.SetFloat($"HighScoreTime{highScoreCount}", newTime);

        // Increase the high score count
        PlayerPrefs.SetInt("HighScoreCount", highScoreCount + 1);

        // Save all the data to PlayerPrefs
        PlayerPrefs.Save();

        Debug.Log($"Saved high score: {playername} - {newTime}");
    }

}
