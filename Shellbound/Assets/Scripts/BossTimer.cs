using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private static float timer = 0;
    private float bestTime;
    private const string bestTimerString = "Best Timer";
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highscorelistText;

    private List<string> playerNamesList = new List<string>();

    private List<float> bestTimesList = new List<float>();

    public HealthSystem healthSystem;

    /*
     * Make so we can have top 5 high scores
     * 
     * */
    // Start is called before the first frame update
    void Start()
    {
        TimerRunning = true;
        bestTime = PlayerPrefs.GetFloat(bestTimerString, float.MaxValue);
        //timerText.text = "You best time is" + bestTime.ToString("F0");

        if (bestTime == float.MaxValue)
        {
           // timerText.text = "No high score yet!";
           Debug.Log("no high score yet");
        }
        else
        {
            Debug.Log("you best time is" + bestTime.ToString("F0"));
            //timerText.text = ("you best time is" + bestTime.ToString("F0")); 
        }
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
       
        //timerText.text = "Your time to kill this boss was" + " " + timer.ToString("F0");
        Debug.Log("your time was" + " " + timer.ToString("F0"));

        if (timer < bestTime)
        {
            bestTime = timer;
            PlayerPrefs.SetFloat(bestTimerString, bestTime);
            PlayerPrefs.Save();
            Debug.Log("New Best Timer: " + bestTime.ToString("F0"));

            SaveNewTime("player name" , bestTime ); //need to make so player can write their name

        }       
        //save string in const variable
    }

    public void SaveNewTime(string playername, float newTime)
    {
        Debug.Log("savenewtime method");
        if (playerNamesList.Count <=5 && bestTimesList.Count <= 5)
        {
            playerNamesList.Add(playername);
            bestTimesList.Add(newTime);
        }
        else
        {

        }
       
        PlayerPrefs.Save();
        ShowHighScores();
    }

    public void ShowHighScores()
    {
        Debug.Log("ShowHighScore");
       for (int i = 0; i < bestTimesList.Count; i++)
        {
           
            float time = bestTimesList[i];
            highscorelistText.text="High Score " + (i + 1) + ": " + " - " + time.ToString("F2") + " seconds";
                
           // Debug.Log("High Score " + (i + 1) + ": " +  " - " + time.ToString("F2") + " seconds");

        }
       
    }

    public void SaveTimes()
    {

    }
}
