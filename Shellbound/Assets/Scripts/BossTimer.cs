using UnityEngine;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private static float timer = 0;
    [HideInInspector]public string playerName = "insert player name";
   
  
    void Start()
    {
        if( IntroManager.instance == null)
        {
            TimerRunning = true;
        }         
    }

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
      
       // Debug.Log("your time was" + " " + timer.ToString("F2"));

        //add score to highscoremanager
        try
        {
            HighScoreManager.instance.AddScore(playerName, timer);
           
        }
        catch { }
      
        timer = 0;
    }    
}
