using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI completedTimerText;
    public TextMeshProUGUI textofhereScore;

    public List<TextMeshProUGUI> highScoreTextListAll = new List<TextMeshProUGUI>();
    private HighScoreManager highScoreManager;

    private TMP_InputField playerNameInput;
    private TextMeshProUGUI newHighScoreText;
   [HideInInspector] public static string CompleteplayerName;
    [HideInInspector] public static float CompleteTimer;

    private float newHighScoreTime;

    // Start is called before the first frame update
    void Start()
    {
        // Find the HighScoreManager in the scene
        highScoreManager = FindObjectOfType<HighScoreManager>();

        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found in the scene!");
            return;
        }
        if (SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            LoadTheScores();
        }

        if (SceneManager.GetActiveScene().name == ("VictoryScreen"))
        {
            VictoryScreen();     
        }
    }

    public void LoadTheScores()
    {

        // textofhereScore.text = "";

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
            // textofhereScore.text = ("You high scores:");  
        }

        Debug.Log($"Got this many scores: {highScoreManager.bestTimesList.Count}");
    }

    public void VictoryScreen()
    {
        //adda bara sen åka till sort //sen spara etc
        if (playerNameInput == null)
        {

            playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
        }
        else
        {

            // playerNameInput.gameObject.SetActive(false);

        }

        newHighScoreTime = HighScoreManager.AfterFightTime;
        Debug.Log("this is new time" + "" + newHighScoreTime);

        completedTimerText.text = ($" {newHighScoreTime:F2}");

        if (highScoreManager.bestTimesList.Count > 0 && newHighScoreTime < highScoreManager.bestTimesList[i])
        {
            //TODO MAKE SO YOU CAN INSERT NAME IF TOP 5
            newHighScoreText = GameObject.Find("newHighScore").GetComponent<TextMeshProUGUI>();
            newHighScoreText.text = ($"You got a new high score!/n Your new score is /n {newHighScoreTime:F2}");
            highScoreManager.bestTimesList.Add(newHighScoreTime);
            playerNameInput.gameObject.SetActive(true);
            newHighScoreText.text = ("Enter your name!");
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
            //newHighScoreText.text = ($"You name is now submitted: {CompleteplayerName}");
        }
        else
        {
            Debug.Log("You need to enter a name");
            newHighScoreText.text = "Please enter a name!";
        }
    }
}


        
        
    






