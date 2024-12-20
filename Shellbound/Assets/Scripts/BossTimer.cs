using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private static float timer = 0;
    private float bestTime;
    public TextMeshProUGUI timerText;
    

    public HealthSystem healthSystem;

    /*
     * Make so the player can have a "highscore" how fast they killed the boss
     * 
     * */
    // Start is called before the first frame update
    void Start()
    {
        TimerRunning = true;
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
        timerText.text = "Your time to kill this boss was" + " " + (int)timer + "" + "seconds";
        Debug.Log("your time was" + " " +  (int)timer);
    }
}
