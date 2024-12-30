using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;


public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private static float bestTime;

    [HideInInspector] public int maxHighScores = 5;
    private const string completeTime = "completeTime";
    public TextMeshProUGUI completedTimerText;
    public TextMeshProUGUI textofhereScore;

    private TextMeshProUGUI newHighScoreText;

    public List<TextMeshProUGUI> highScoreTextListAll = new List<TextMeshProUGUI>();

    private TMP_InputField playerNameInput;

    private static float CompleteTimer;
    private static float newHighScoreTime;
    private static string CompleteplayerName;
    
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
           Debug.Log("HighScoreManager instantiated");

           instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        bestTime = PlayerPrefs.GetFloat(completeTime, float.MaxValue);
        completedTimerText.text = "You best time is" + bestTime.ToString("F2");

        LoadTheScores();

        if (bestTime == float.MaxValue)
        {
            //TODO
            //make so it shows the highest score in list :)
            completedTimerText.text = "No high score yet!";

        }
        else
        {
            completedTimerText.text = ("you best time is" + bestTime.ToString("F2")); 
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadTheScores();
        }
        
        if (SceneManager.GetActiveScene().name == ("VictoryScreen"))
        {

            
           
            playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
            playerNameInput.gameObject.SetActive(false);

            if (playerNameInput = null )
            {
                Debug.Log("Where is input");
                playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
            }
            else
            {
                Debug.Log("got the input");
            }

            completedTimerText = GameObject.Find("TIME").GetComponent<TextMeshProUGUI>();
            completedTimerText.text = ($"Your time to defeat the boss was: {CompleteTimer:F2}");

            if (bestTimesList.Count > 0 && CompleteTimer < bestTimesList[0])
            {     
                //TODO MAKE SO THE NEW TIME DONT SAVE IN THE LIST OR THIS WILL NEVER BE TRU
                newHighScoreText = GameObject.Find("newHighScore").GetComponent<TextMeshProUGUI>();
                newHighScoreText.text = ($"You got a new high score!/n Your new score is /n {newHighScoreTime:F2}");
     
                playerNameInput.gameObject.SetActive(true);
                newHighScoreText.text = ("Enter your name!");
                CompleteplayerName = playerNameInput.text;

                //saving the new score
                SortHighScore();
            }
        }
    }

    public void AddScore(string playerName, float timer)
    {
        Debug.Log($"Adding high score: {playerName} - {timer:F2}");

        completedTimerText.text = ($"Your time to defeat the boss was: {playerName} - {timer:F2}");

        CompleteTimer = timer;
        CompleteplayerName = playerName;

        if (bestTimesList.Count > 0 && timer < bestTimesList[0])
        {
            Debug.Log("New high score");
            newHighScoreTime = timer;
            return;
        }

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
               
        textofhereScore.text = "";

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
            playerNamesList = playerNamesList.GetRange(0, 5);
            bestTimesList = bestTimesList.GetRange(0, 5);
        }

        //TODO
        //make this just go through like a list? 
        //

        if (bestTimesList.Count > 0)
        {
            //show the high scores

            for (int i = 0;i < highScoreTextListAll.Count; i++)
            {   
                if (i < playerNamesList.Count && i < bestTimesList.Count)
                {
                    textofhereScore.text = $"{playerNamesList[i]} - {bestTimesList[i]:F2} seconds";
                }
                else
                {
                    highScoreTextListAll[i].text = ("No score here yet");
                }
                
            }
            textofhereScore.text = ("You high scores:");  
        }
       

        /*
        // Display the loaded high scores
        for (int i = 0; i < bestTimesList.Count; i++)
        {
            highscorelistText.text += $"High Score {i + 1}: {playerNamesList[i]} - {bestTimesList[i]:F2} seconds\n";
            highScore1.text = $"{playerNamesList[0]} - {bestTimesList[0]} seconds";
            highScore2.text = $"{playerNamesList[1]} - {bestTimesList[1]} seconds";
            highScore3.text = $"{playerNamesList[2]} - {bestTimesList[2]} seconds";
            highScore4.text = $"{playerNamesList[3]} - {bestTimesList[3]} seconds";
            highScore5.text = $"{playerNamesList[4]} - {bestTimesList[4]} seconds";
            
            Debug.Log($"High Score {i + 1}: {playerNamesList[i]} - {bestTimesList[i]:F2} seconds");
        }

        */

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
        if ( textofhereScore != null)
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
