using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI completedTimerText;
    public TextMeshProUGUI textofhereScore;

    public List<TextMeshProUGUI> highScoreTextListAll = new List<TextMeshProUGUI>();
    private HighScoreManager highScoreManager;

    private TMP_InputField playerNameInput;
    [HideInInspector] public TextMeshProUGUI newHighScoreText;
   [HideInInspector] public static string CompleteplayerName;
    [HideInInspector] public static float CompleteTimer;

    private float newHighScoreTime;

  
    void Start()
    {
      
        highScoreManager = FindObjectOfType<HighScoreManager>();

        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found in the scene!");
            return;
        }
        if (SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            highScoreManager.top5 = false;
           // Debug.Log("Name list" + " " + highScoreManager.playerNamesList.Count);
            //Debug.Log("time list" + "" + highScoreManager.bestTimesList.Count);
            LoadTheScores();
        }

        if (SceneManager.GetActiveScene().name == ("VictoryScreen"))
        {
           // Debug.Log("Name list" + " " + highScoreManager.playerNamesList.Count);
           // Debug.Log("time list" + "" + highScoreManager.bestTimesList.Count);
            VictoryScreen();     
        }
    }

    public void LoadTheScores()
    {    

        highScoreManager.playerNamesList.Clear();
        highScoreManager.bestTimesList.Clear();

        //testing from internet
        // Load scores from PlayerPrefs
        int highScoreCount = PlayerPrefs.GetInt("HighScoreCount", 0);
        for (int i = 0; i < highScoreCount; i++)
        {
            string playerName = PlayerPrefs.GetString($"HighScoreName{i}", "");
            float playerTime = PlayerPrefs.GetFloat($"HighScoreTime{i}", float.MaxValue);

            if (playerTime != float.MaxValue)
            {
                highScoreManager.playerNamesList.Add(playerName);
                highScoreManager.bestTimesList.Add(playerTime);
            }
        }

        if (highScoreManager.bestTimesList.Count > 0)
        {
            //show the high scores

            for (int i = 0; i < highScoreTextListAll.Count; i++)
            {
                if (i < highScoreManager.playerNamesList.Count && i < highScoreManager.bestTimesList.Count)
                {
                    highScoreTextListAll[i].text = $"{highScoreManager.playerNamesList[i]} - {highScoreManager.bestTimesList[i]:F2} seconds";

                    Debug.Log($"Position {i + 1}: {highScoreManager.bestTimesList[i]:F2} seconds");
                }
                else
                {
                    highScoreTextListAll[i].text = ("Empty");
                }
            }
              
        }
      
    }

    public void VictoryScreen()
    {
           
        if (playerNameInput == null)
        {

            playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
        }
      
        newHighScoreTime = HighScoreManager.AfterFightTime;
        //Debug.Log("this is new time" + "" + newHighScoreTime);

        completedTimerText.text = ($" {newHighScoreTime:F2}");

        if (highScoreManager.top5 == true)
        {
            
            newHighScoreText = GameObject.Find("newHighScore").GetComponent<TextMeshProUGUI>();
            //newHighScoreText.text = ($"You got a new high score!/n Your new score is /n {newHighScoreTime:F2}");
            //highScoreManager.bestTimesList.Add(newHighScoreTime);
            playerNameInput.gameObject.SetActive(true);
            newHighScoreText.text = ("New high score!");
        }
        else
        {
            newHighScoreText = GameObject.Find("newHighScore").GetComponent<TextMeshProUGUI>();
            playerNameInput.gameObject.SetActive (false);
            newHighScoreText.text = ($"Not in top 5");
        }
       
    }


    public void CheckIfValidName() //TODO make a try catch
    {
        if (!string.IsNullOrEmpty(playerNameInput.text))
        {
            CompleteplayerName = playerNameInput.text;

            highScoreManager.playerNamesList.Add(CompleteplayerName);
            highScoreManager.bestTimesList.Add(newHighScoreTime);            
            highScoreManager.SortHighScore();
            playerNameInput.gameObject.SetActive(false);
            
            int playerRank = highScoreManager.playerNamesList.IndexOf(CompleteplayerName) + 1;
            newHighScoreText.text = ($" {CompleteplayerName} is now number {playerRank}");
        }
        else
        {
            Debug.Log("You need to enter a name");
            newHighScoreText.text = "Please enter a name!";
        }


    }
}


        
        
    






