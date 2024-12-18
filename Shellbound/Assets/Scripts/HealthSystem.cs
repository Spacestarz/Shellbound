using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public bool hasHealthBar = true;
    public Slider healthBar;

    public float MaxHP; //maxHP
    public float currentHP;
    public float deathTime = 1;

    public GameObject Player; //player*

    public AudioSource source;
    public AudioClip audioClip;

    public UI uiScript;
    public SpriteRenderer enemySprite;

    public BossTimer BossTimerScript;
    void Awake()
    { 
       
        currentHP = MaxHP;
        source = GetComponent<AudioSource>();

        if(healthBar)
        {
            healthBar.maxValue = MaxHP;
            healthBar.value = MaxHP;
        }
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       

        if (gameObject.CompareTag("Player") && currentHP <= 0)
        {
            source.PlayOneShot(audioClip, 0.2f);
            if(currentHP <= 0)
            {
                PlayerDead();   
            }
        }

        if (gameObject.CompareTag("Enemy") && currentHP <= 0)
        {
            GameObject mantisShrimp = GameObject.Find("MantisShrimp");
            if ( mantisShrimp != null )
            {
                Bossdead();
            }

            if (GetComponent<HookableObject>().isCaught)
            {
                PlayerSlice.SetSliceMode(false);
                Camera.main.GetComponent<RotateCamera>().DOKill();
                Camera.main.GetComponent<RotateCamera>().isLocked = false;
            }

            Invoke(nameof(dead),deathTime);
         
        }

        if(hasHealthBar)
        {
            healthBar.value = currentHP;
        }

    }
    void dead()
    {   
        Destroy(gameObject);
    }

    public void Bossdead()
    {
        BossTimerScript.StopTimer();
        uiScript.DefeatedBOSS();
    }

    public void PlayerDead()
    {
        //Game over screen for player
        uiScript.GameOver();
    }
}
