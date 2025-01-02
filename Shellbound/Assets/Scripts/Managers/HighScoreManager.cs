using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private static float bestTime;

    [HideInInspector] public int maxHighScores = 5;
    private const string completeTime = "completeTime";

    private static float newHighScoreTime;

    private GameObject scoreObjectMenu;
    private GameObject canvasMenu;
    private GameObject textherescoreMenu; //TODO FIX THIS

    [HideInInspector] public List<string> playerNamesList = new List<string>();

    [HideInInspector] public List<float> bestTimesList = new List<float>();

    private DisplayScore displayScoreScript;

    [HideInInspector] public static string AfterFightName;
    [HideInInspector] public static float AfterFightTime;



    private void Awake()
    {
       
        if (instance != null && instance != this)
        {          
            Destroy (this);
            return;           
        }
        else
        {

           instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        /*
        bestTime = PlayerPrefs.GetFloat(completeTime, float.MaxValue);
        completedTimerText.text = "You best time is" + bestTime.ToString("F2");

        

        if (bestTime == float.MaxValue)
        {
            //TODO
            completedTimerText.text = "No high score yet!";

        }
        else
        {
            completedTimerText.text = "you best time is" + bestTime.ToString("F2"); 
        }
        */
       
    }
    
    void Update()
    {

       
      
    }   

    public void AddScore(string playerName, float timer)
    {
       // completedTimerText.text = ($"Your time to defeat the boss was: {playerName} - {timer:F2}");

        AfterFightTime = timer;
        AfterFightName = playerName;
    }

    public void SortHighScore()
    {
        //bubble sort
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

                    Debug.Log($"Rank {i + 1}: {playerNamesList[i]} - {bestTimesList[i]:F2} seconds");
                }
            }
        }

        if (bestTimesList.Count > 5)
        {
            bestTimesList.RemoveAt(bestTimesList.Count - 1);
            playerNamesList.RemoveAt(playerNamesList.Count - 1);
        }

        SavedScores();      
    }

    public void SavedScores()
    {
        PlayerPrefs.SetInt("HighScoreCount", bestTimesList.Count);
        for (int i = 0; i < bestTimesList.Count; i++)
        {
            PlayerPrefs.SetString($"HighScoreName{i}", playerNamesList[i]);

            PlayerPrefs.SetFloat($"HighScoreTime{i}", bestTimesList[i]);   
        }
        PlayerPrefs.Save();     
    }
}
