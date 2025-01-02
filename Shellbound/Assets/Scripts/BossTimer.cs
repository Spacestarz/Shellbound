using UnityEngine;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private static float timer = 0;
    [HideInInspector]public string playerName = "insert player name";
    /*
     * Make so we can have top 5 high scores
     * 
     * */
    // Start is called before the first frame update
    void Start()
    {
        if( IntroManager.instance == null)
        {
            TimerRunning = true;
            Debug.Log("Starting timer");
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
      
        Debug.Log("your time was" + " " + timer.ToString("F2"));

        //add score to highscoremanager
        try
        {
            HighScoreManager.instance.AddScore(playerName, timer);
            //SaveNewTime(playerName, timer); 
        }
        catch { }
      
        //save string in const variable
    }
    /*
    public void SaveNewTime(string playername, float newTime)
    {
        
        // Retrieve the current count of high scores from PlayerPrefs
        int highScoreCount = PlayerPrefs.GetInt("HighScoreCount", 0);

        // Save the player's name and time for this score
        PlayerPrefs.SetString($"HighScoreName{highScoreCount}", playername);
        PlayerPrefs.SetFloat($"HighScoreTime{highScoreCount}", newTime);

        // Increase the high score count
        PlayerPrefs.SetInt("HighScoreCount", highScoreCount + 1);

        // Save all the data to PlayerPrefs
        PlayerPrefs.Save();
       
    }
    */
    
}
