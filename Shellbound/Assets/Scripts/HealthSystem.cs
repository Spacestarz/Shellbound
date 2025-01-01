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

    [HideInInspector] public AudioSource source;
    public AudioClip playerTakeDamage;
    public AudioClip[] bossTakeDamage;
    public AudioClip bossDeath;

    [SerializeField] GameObject bossSushi;

    [SerializeField] Sprite crackedFrame; 

    public BossTimer BossTimerScript;

    [HideInInspector] public bool playerInvulnerable;
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
        if(gameObject.CompareTag("Player") && !playerInvulnerable && !OutroManager.isRunning)
        {
            currentHP -= damageTaken;

            source.PlayOneShot(playerTakeDamage, 0.3f);

            Camera.main.GetComponent<CameraHandler>().ShakeCamera(0.3f, new Vector3(1f, 0.2f, 0));
            DamageVignette.ShowVignette();

            StartCoroutine(PlayerInvulnerability());

            if(currentHP == 5)
            {
                healthBar.transform.Find("Frame").GetComponent<Image>().sprite = crackedFrame;
            }

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
        else if(!gameObject.CompareTag("Player"))
        {
            currentHP -= damageTaken;
        }


        if (gameObject.CompareTag("Enemy") && currentHP <= 0)
        {
            GameObject mantisShrimp = GameObject.Find("MantisShrimp");
            if (gameObject.name == "MantisShrimp")
            {
                GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Die");
                BossDie();
            }

            if (GetComponent<HookableObject>().isCaught)
            {
                PlayerSlice.SetSliceMode(false);
                Camera.main.GetComponent<RotateCamera>().DOKill();
                Camera.main.GetComponent<RotateCamera>().isLocked = false;
            }

            if(gameObject != mantisShrimp)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                Invoke(nameof(Die),deathTime);

                if(gameObject.name.Contains("urchin"))
                {
                    UrchinSpawner.RemoveFromList(gameObject);
                }
            }
         
        }

        if(hasHealthBar)
        {
            healthBar.value = currentHP;
        }

    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void BossDie()
    {
        source.spatialBlend = 0;
        source.PlayOneShot(bossDeath);

        GetComponent<Enemi_health>().DisableAI();
        GetComponent<Boss1_AI>().enabled = false;
        BossTimerScript.StopTimer();

        Invoke(nameof(BossSpawnSushi), bossDeath.length);
        Invoke(nameof(Die), bossDeath.length);
        OutroManager.StartOutro(true);
    }

    public void BossSpawnSushi()
    {
        Vector3 pos = transform.position;
        bossSushi.SetActive(true);
        bossSushi.transform.position = new Vector3(pos.x, bossSushi.transform.position.y, pos.z);
    }

    public void PlayerDead()
    {
        //Game over screen for player
        OutroManager.StartOutro(false);
    }


    IEnumerator PlayerInvulnerability()
    {
        playerInvulnerable = true;
        yield return new WaitForSeconds(playerInvulnTime);
        playerInvulnerable = false;
    }
}
