using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public GameObject Player;
    public AudioSource sorce;
    public AudioClip audio;

    public UI uiScript;
    public SpriteRenderer enemySprite;


    // Start is called before the first frame update
    void Start()
    {      
        currentHP = MaxHP;
        sorce = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       
        sorce.PlayOneShot(audio);

        if (gameObject.CompareTag("Player") && currentHP <= 0)
        {
            PlayerDead();   
        }

        if (gameObject.CompareTag("Enemy") && currentHP <= 0)
        {
            Bossdead();
        }

        if (gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(FlashRed());
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

    IEnumerator FlashRed()
    {
        enemySprite.color = Color.red;
        yield return new WaitForSecondsRealtime(0.2f);
        enemySprite.color = Color.white;
    }
}
