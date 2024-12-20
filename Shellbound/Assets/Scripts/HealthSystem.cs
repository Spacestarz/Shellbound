using DG.Tweening;
using System.Collections;
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

    public BossTimer BossTimerScript;

    bool playerInvulnerable;
    readonly float playerInvulnTime = 0.75f;

    DamageFlash damageFlash;

    void Awake()
    { 
        currentHP = MaxHP;
        source = GetComponent<AudioSource>();

        if(healthBar)
        {
            healthBar.maxValue = MaxHP;
            healthBar.value = MaxHP;
        }

        damageFlash = GetComponentInChildren<DamageFlash>();
    }

    public void TakeDamage(int damageTaken)
    {
        if(gameObject.CompareTag("Player") && !PlayerSlice.SliceMode() && !playerInvulnerable)
        {
            currentHP -= damageTaken;

            source.PlayOneShot(audioClip, 0.3f);

            Camera.main.GetComponent<CameraHandler>().ShakeCamera(0.2f, new Vector3(1f, 0.2f, 0));
            DamageVignette.ShowVignette();

            StartCoroutine(PlayerInvulnerability());

            if (currentHP <= 0)
            {
                PlayerDead();
            }
        }
        else if(!gameObject.CompareTag("Player") && damageFlash)
        {
            currentHP -= damageTaken;
            damageFlash.FlashRed();
        }
        else
        {
            currentHP -= damageTaken;
        }


        if (gameObject.CompareTag("Enemy") && currentHP <= 0)
        {
            GameObject mantisShrimp = GameObject.Find("MantisShrimp");
            if (gameObject.name == "MantisShrimp")
            {
                Bossdead();
            }

            if (GetComponent<HookableObject>().isCaught)
            {
                PlayerSlice.SetSliceMode(false);
                Camera.main.GetComponent<RotateCamera>().DOKill();
                Camera.main.GetComponent<RotateCamera>().isLocked = false;
            }

            if(gameObject != mantisShrimp)
            {
                //gameObject.SetActive(false);
                gameObject.GetComponent<Collider>().enabled = false;
                //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Invoke(nameof(dead),deathTime);
            }
         
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
        Invoke(nameof(dead), deathTime);
        uiScript.DefeatedBOSS();
    }

    public void PlayerDead()
    {
        //Game over screen for player
        uiScript.GameOver();
    }


    IEnumerator PlayerInvulnerability()
    {
        playerInvulnerable = true;
        yield return new WaitForSeconds(playerInvulnTime);
        playerInvulnerable = false;
    }
}
