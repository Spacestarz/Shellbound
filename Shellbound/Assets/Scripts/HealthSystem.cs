using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public GameObject Player;
    public AudioSource sorce;
    public AudioClip audio;


    public GameObject BOSSTESTING;
    public UI uiScript;


    // Start is called before the first frame update
    void Start()
    {      
        currentHP = MaxHP;
        sorce = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       
        Debug.Log(currentHP + "/" + MaxHP);
        sorce.PlayOneShot(audio);

        if (gameObject.CompareTag("Player") && currentHP <= 0)
        {
            PlayerDead();   
        }

        if (gameObject.CompareTag("Enemy") && currentHP <= 0)
        {
            Bossdead();
        }
     
    }

    public void Bossdead()
    {
       uiScript.DefeatedBOSS();
    }

    public void PlayerDead()
    {
        //Game over screen for player
        Debug.Log("INSERT DEAD SCREEN");
        uiScript.GameOver();
    }
}
