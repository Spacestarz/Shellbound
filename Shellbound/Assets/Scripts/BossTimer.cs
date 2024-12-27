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
    private static float bestTime;

    private int maxHighScores = 5;
    private const string bestTimerString = "Best Timer";
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highscorelistText;

    private List<string> playerNamesList = new List<string>();

    private List<float> bestTimesList = new List<float>();


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
            //timerText.text = "No high score yet!";
            
        }
        else
        {
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
       
        timerText.text = "Your time to kill this boss was" + " " + timer.ToString("F0");
        Debug.Log("your time was" + " " + timer.ToString("F0"));

        PlayerPrefs.SetFloat(bestTimerString, timer);
        PlayerPrefs.Save();
        Debug.Log("your Best Timer: " + bestTime.ToString("F0"));

        SaveNewTime("player name", timer); //need to make so player can write their name


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
        if (playerNamesList.Count < maxHighScores && bestTimesList.Count < maxHighScores)
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
        highscorelistText.text = "";
        for (int i = 0; i < bestTimesList.Count; i++)
        {
           
            float time = bestTimesList[i];
            highscorelistText.text += "High Score " + (i + 1) + ": " + playerNamesList[i] + " - " + bestTimesList[i].ToString("F2") + " seconds\n";

            // Debug.Log("High Score " + (i + 1) + ": " +  " - " + time.ToString("F2") + " seconds");

        }
       
    }

    private void SortHighScores()
    {
       for (int i = 0; i < bestTimesList.Count - 1; i++)
        {
            for (int j = i + 1; j < bestTimesList.Count; j++)
            {
                if (bestTimesList[j] < bestTimesList[i])
                {
                    //swapping times
                    float tempTime = bestTimesList[i];
                    bestTimesList[i] = bestTimesList[j];
                    bestTimesList[j] = tempTime;    

                    //swappes names
                    string tempName = playerNamesList[i];
                    playerNamesList[i] = playerNamesList[j];
                    playerNamesList[j] = tempName;
                } 
            }
        }

       //gets only top 5 scores
       if (bestTimesList.Count > maxHighScores)
        {
            playerNamesList = playerNamesList.GetRange(0, 5);
            bestTimesList = bestTimesList.GetRange(0, 5);
        }
    }

    public void SaveTimes()
    {

    }
}
