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
   
    private TextMeshProUGUI newHighScoreText;

    private TMP_InputField playerNameInput;

    private static float CompleteTimer;
    private static float newHighScoreTime;
    private static string CompleteplayerName;
 
    private GameObject scoreObjectMenu;
    private GameObject canvasMenu;
    private GameObject textherescoreMenu; //TODO FIX THIS

    [HideInInspector] public List<string> playerNamesList = new List<string>();

    [HideInInspector] public List<float> bestTimesList = new List<float>();

    private DisplayScore displayScoreScript;



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

        if (SceneManager.GetActiveScene().name == ("VictoryScreen"))
        {                            
            //adda bara sen åka till sort //sen spara etc
            if (playerNameInput == null )
            {
               
                playerNameInput = GameObject.Find("enteryourname").GetComponent<TMP_InputField>();
            }
            else
            {
                
               // playerNameInput.gameObject.SetActive(false);

            }
            displayScoreScript = GameObject.Find("Canvas").GetComponent<DisplayScore>();         
            displayScoreScript.completedTimerText.text = ($" {CompleteTimer:F2}");

            if (bestTimesList.Count > 0 && CompleteTimer < bestTimesList[0]) 
            {     
                //TODO MAKE SO YOU CAN INSERT NAME IF TOP 5
                newHighScoreText = GameObject.Find("newHighScore").GetComponent<TextMeshProUGUI>();
                newHighScoreText.text = ($"You got a new high score!/n Your new score is /n {newHighScoreTime:F2}");
                bestTimesList.Add(newHighScoreTime);
                playerNameInput.gameObject.SetActive(true);
                newHighScoreText.text = ("Enter your name!");   

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
       // completedTimerText.text = ($"Your time to defeat the boss was: {playerName} - {timer:F2}");

        CompleteTimer = timer;
        CompleteplayerName = playerName;
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
