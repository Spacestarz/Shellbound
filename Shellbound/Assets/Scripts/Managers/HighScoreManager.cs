using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private static float bestTime;

    [HideInInspector] public int maxHighScores = 5;
    private const string bestTimerString = "Best Timer";
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highscorelistText;

    private List<string> playerNamesList = new List<string>();

    [HideInInspector] public List<float> bestTimesList = new List<float>();

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
        bestTime = PlayerPrefs.GetFloat(bestTimerString, float.MaxValue);
        timerText.text = "You best time is" + bestTime.ToString("F2");

        LoadTheScores();

        if (bestTime == float.MaxValue)
        {
            timerText.text = "No high score yet!";

        }
        else
        {
            timerText.text = ("you best time is" + bestTime.ToString("F2")); 
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadTheScores();
        }
    }

    public void AddScore(string playerName, float timer)
    {
        Debug.Log($"Adding high score: {playerName} - {timer}");

        //if (timer > )
        // Add the new name and score
        playerNamesList.Add(playerName);
        bestTimesList.Add(timer);
        PlayerPrefs.Save();

        // Sort the scores
        SortHighScore();

        SavedScores();
    }

    public void LoadTheScores ()
    {
        Debug.Log("LoadTheScores Method");
               
        highscorelistText.text = "";

        playerNamesList.Clear();
        bestTimesList.Clear();

        //loading scores from playerprefs hopefully
        SavedScores ();

        //testing from internet
        // Load scores from PlayerPrefs
        int highScoreCount = PlayerPrefs.GetInt("HighScoreCount", 0);
        for (int i = 0; i < highScoreCount; i++)
        {
            string playerName = PlayerPrefs.GetString($"HighScoreName{i}", "");
            float playerTime = PlayerPrefs.GetFloat($"HighScoreTime{i}", float.MaxValue);

            if (playerTime != float.MaxValue)
            {
                playerNamesList.Add(playerName);
                bestTimesList.Add(playerTime);
            }
        }

        //gets only top 5 scores
        if (bestTimesList.Count > maxHighScores)
        {
            Debug.Log("To mucho");
            playerNamesList = playerNamesList.GetRange(0, 5);
            bestTimesList = bestTimesList.GetRange(0, 5);

        }

        // Display the loaded high scores
        for (int i = 0; i < bestTimesList.Count; i++)
        {
            highscorelistText.text += $"High Score {i + 1}: {playerNamesList[i]} - {bestTimesList[i]:F2} seconds\n";
            Debug.Log($"High Score {i + 1}: {playerNamesList[i]} - {bestTimesList[i]:F2} seconds");
        }

        Debug.Log($"Got this many scores: {bestTimesList.Count}");
        /*

        for (int i = 0; i < bestTimesList.Count; i++)
        {
            float time = bestTimesList[i];
            highscorelistText.text += "High Score " + (i + 1) + ": " + playerNamesList[i] + " - " + bestTimesList[i].ToString("F2") + " seconds\n";

             Debug.Log("High Score " + (i + 1) + ": " +  " - " + time.ToString("F2") + " seconds");

            PlayerPrefs.Save();
        }

        Debug.Log("Got this many score" + "" + "" + bestTimesList.Count);
        */
    }     

    public void SortHighScore()
    {
        if ( highscorelistText != null)
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

                        SavedScores();
                    }
                }
            }
        }
       

    }

    public void SavedScores()
    {
        int highScoreCount = PlayerPrefs.GetInt("HighScoreCount", 0);
        for (int i = 0; i < bestTimesList.Count; i++)
        {
            PlayerPrefs.SetString($"HighScoreName{i}", playerNamesList[i]);
            PlayerPrefs.SetFloat($"HighScoreTime{i}", bestTimesList[i]);   
        }

        PlayerPrefs.Save();
        Debug.Log("saved the HIGHscores)");
    }
}
