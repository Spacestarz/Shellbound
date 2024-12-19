using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    public bool TimerRunning = false;
    private float timer = 0;

    public HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        TimerRunning = true;
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
        Debug.Log("your time was" + " " +  (int)timer);
    }
}
