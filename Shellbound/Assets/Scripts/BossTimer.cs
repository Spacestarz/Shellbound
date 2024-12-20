using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private static float timer = 0;
    private float bestTime;
   // public TextMeshProUGUI timerText;
    

    public HealthSystem healthSystem;

    /*
     * Make so we can have top 5 high scores
     * 
     * */
    // Start is called before the first frame update
    void Start()
    {
        TimerRunning = true;
        bestTime = PlayerPrefs.GetFloat("Best Timer", float.MaxValue);
        //timerText.text = "You best time is" + bestTime.ToString("F0");

        if (bestTime == float.MaxValue)
        {
           // timerText.text = "No high score yet!";
           Debug.Log("no high score yet");
        }
        else
        {
            Debug.Log("you best time is" + bestTime.ToString("F0"));
            //timerText.text = ("you best time is" + bestTime.ToString("F0")); 
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
       // timerText.text = "Your time to kill this boss was" + " " + timer.ToString("F0");
        Debug.Log("your time was" + " " + timer.ToString("F0"));

        if (timer < bestTime)
        {
            bestTime = timer;
            PlayerPrefs.SetFloat("Best Timer", bestTime);
            PlayerPrefs.Save();
            Debug.Log("New Best Timer: " + bestTime.ToString("F0"));

        }       
        //save string in const variable
    }
}
