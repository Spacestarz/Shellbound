using System.Collections;
using UnityEngine;
public class HealthSystem : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public GameObject Player;
    public AudioSource source;
    public AudioClip audioClip;

    public UI uiScript;
    public SpriteRenderer enemySprite;


    // Start is called before the first frame update
    void Start()
    {      
        currentHP = MaxHP;
        source = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       
        source.PlayOneShot(audioClip, 0.2f);

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
        uiScript.GameOver();
    }

    IEnumerator FlashRed()
    {
        enemySprite.color = Color.red;
        yield return new WaitForSecondsRealtime(0.2f);
        enemySprite.color = Color.white;
    }
}
