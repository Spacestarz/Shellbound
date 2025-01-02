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

    public bool top5 = false;



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

    public void AddScore(string playerName, float timer)
    {
       // completedTimerText.text = ($"Your time to defeat the boss was: {playerName} - {timer:F2}");       
        AfterFightTime = timer;
        AfterFightName = playerName;
        CheckIfTop5(timer, playerName);
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
            Debug.Log("removing some");
            bestTimesList.RemoveAt(bestTimesList.Count - 1);
            playerNamesList.RemoveAt(playerNamesList.Count - 1);
        }

        SavedScores();      
    }

    public void CheckIfTop5(float temptime, string tempname)
    {
        Debug.Log("got this count" + "" + bestTimesList.Count);

        if (bestTimesList.Count < 4)
        {
            top5 = true;
            // Add the new score and name to the temporary lists (will not save yet)
            //playerNamesList.Add(tempname);
            //bestTimesList.Add(temptime);       
        }

        if (temptime < bestTimesList[bestTimesList.Count -1] && bestTimesList.Count >= 4)
        {
            Debug.Log("you in top 5");
            top5 = true;
            // Add the new score and name to the temporary lists (will not save yet)
            //bestTimesList.Add(temptime);
            //playerNamesList.Add(tempname);
        }
       
       
    }

    public void SavedScores()
    {

        // Check if the lists are the same size
        if (bestTimesList.Count != playerNamesList.Count)
        {
            Debug.LogError("The lists bestTimesList and playerNamesList must have the same length.");
            return; // Exit the method early to avoid the error
        }

        PlayerPrefs.SetInt("HighScoreCount", bestTimesList.Count);
        for (int i = 0; i < bestTimesList.Count; i++)
        {
            PlayerPrefs.SetString($"HighScoreName{i}", playerNamesList[i]);

            PlayerPrefs.SetFloat($"HighScoreTime{i}", bestTimesList[i]);   
        }
        PlayerPrefs.Save();     
    }
   
}
