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
    public TextMeshProUGUI completedTimerText;
    public TextMeshProUGUI textofhereScore;

    private TextMeshProUGUI newHighScoreText;

    public List<TextMeshProUGUI> highScoreTextListAll = new List<TextMeshProUGUI>();

    private TMP_InputField playerNameInput;

    private static float CompleteTimer;
    private static float newHighScoreTime;
    private static string CompleteplayerName;

    private Array alltext;

    private GameObject scoreObjectMenu;
    private GameObject canvasMenu;
    private GameObject textherescoreMenu; //TODO FIX THIS
    
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
        bestTime = PlayerPrefs.GetFloat(completeTime, float.MaxValue);
        completedTimerText.text = "You best time is" + bestTime.ToString("F2");

        LoadTheScores();

        if (bestTime == float.MaxValue)
        {
            //TODO
            completedTimerText.text = "No high score yet!";

        }
        else
        {
            completedTimerText.text = "you best time is" + bestTime.ToString("F2"); 
        }
    }
    
    void Update()
    {
             
        if (SceneManager.GetActiveScene().name == ("VictoryScreen"))
        {           
          
           // playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
           
            //adda bara sen �ka till sort //sen spara etc
            if (playerNameInput == null )
            {
               
                playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
            }
            else
            {
                
               // playerNameInput.gameObject.SetActive(false);

            }

            completedTimerText = GameObject.Find("TIME").GetComponent<TextMeshProUGUI>();
            completedTimerText.text = ($"Your time to defeat the boss was: {CompleteTimer:F2}");

            if (bestTimesList.Count > 0 && CompleteTimer < bestTimesList[0]) 
            {     
                //TODO MAKE SO YOU CAN INSERT NAME IF TOP 5
                newHighScoreText = GameObject.Find("newHighScore").GetComponent<TextMeshProUGUI>();
                newHighScoreText.text = ($"You got a new high score!/n Your new score is /n {newHighScoreTime:F2}");
                bestTimesList.Add(newHighScoreTime);
                playerNameInput.gameObject.SetActive(true);
                newHighScoreText.text = ("Enter your name!");   

            }
        } //If in Main Menu Scene
        else if (SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            
            canvasMenu = GameObject.Find("Canvas");
            Transform scoreObjectMenu = canvasMenu.transform.Find("score");
            if (textofhereScore == null)
            {
                scoreObjectMenu.GetComponent<TextMeshProUGUI>();
            }

            highScoreTextListAll.Clear();
            if (highScoreTextListAll.Count == 0 )
            {
                 
                var alltext = scoreObjectMenu.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (var textComponent in alltext)
                {
                    highScoreTextListAll.Add(textComponent);
                }

            }
        }
    }

    public void CheckIfValidName() //TODO make a try catch
    {
        if (!string.IsNullOrEmpty(playerNameInput.text))
        {
            CompleteplayerName = playerNameInput.text;
           
            playerNamesList.Add(CompleteplayerName);
            bestTimesList.Add(CompleteTimer);
            SortHighScore();
            playerNameInput.gameObject.SetActive(false);
            //newHighScoreText.text = ($"You name is now submitted: {CompleteplayerName}");
        }
        else
        {
            Debug.Log("You need to enter a name");
            newHighScoreText.text = "Please enter a name!";
        }
    }

    public void AddScore(string playerName, float timer)
    {
        completedTimerText.text = ($"Your time to defeat the boss was: {playerName} - {timer:F2}");

        CompleteTimer = timer;
        CompleteplayerName = playerName;
    }

    public void LoadTheScores ()
    {
          
       // textofhereScore.text = "";

        playerNamesList.Clear();
        bestTimesList.Clear();

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
    
        if (bestTimesList.Count > 0)
        {
            //show the high scores

            for (int i = 0;i < highScoreTextListAll.Count; i++)
            {   
                if (i < playerNamesList.Count && i < bestTimesList.Count)
                {
                    highScoreTextListAll[i].text = $"{playerNamesList[i]} - {bestTimesList[i]:F2} seconds";

                    Debug.Log($"Position {i + 1}: {bestTimesList[i]:F2} seconds");
                }
                else
                {
                    highScoreTextListAll[i].text = ("Empty");
                }            
            }
           // textofhereScore.text = ("You high scores:");  
        }

        Debug.Log($"Got this many scores: {bestTimesList.Count}");

      
    }     

    public void SortHighScore()
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
