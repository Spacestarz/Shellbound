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
           
        }
        catch { }
      
    
    }
    
}
