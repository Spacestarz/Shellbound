using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public GameObject Player;

    public UI uiScript;


    // Start is called before the first frame update
    void Start()
    {      
        currentHP = MaxHP;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       
        Debug.Log(currentHP + "/" + MaxHP);

        if (gameObject.CompareTag("Player") && currentHP <= 0)
        {
            PlayerDead();   
        }
    }

    public void PlayerDead()
    {
        //Game over screen for player
        Debug.Log("INSERT DEAD SCREEN");
        uiScript.GameOver();
    }
}
