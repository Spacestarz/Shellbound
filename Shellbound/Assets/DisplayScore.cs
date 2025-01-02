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
            
    }

    // Update is called once per frame
    void Update()
    {
      
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
}
