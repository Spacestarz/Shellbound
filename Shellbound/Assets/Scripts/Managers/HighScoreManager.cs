using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
