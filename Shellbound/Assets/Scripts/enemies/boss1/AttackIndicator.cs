using DG.Tweening;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    [Header("Destroytimer of circle")]
    public float destroyTimer = 5;
    private float destroyTime = 0;

    private Vector3 preFabCirclePosition;
    private HealthSystem healthSystem;
    private Enemi_health enemi_Health;

    private GameObject player;
    private GameObject Boss;
    private int damage = 1;

    private bool playerInCircle = false;
    private bool BossInCircle = false;
    

    public GameObject preFabCircle;

    private SpriteRenderer spriteRenderer;
    private Vector3 maxSize;
    private Base_enemy base_EnemyScript;

    private GameObject crowdAttackObject; //here for hit monster crowd
    private Crowd_attacks crowd_AttacksScript; //here for hit monster crowd

    //how many hits the boss takes (crowdattack) before weak.
    private int TotalBeforeWEAK = 3;

    void Awake()
    {
        player = GameObject.Find("Player");
        Boss = GameObject.Find("MantisShrimp");
        crowdAttackObject = GameObject.Find("CrowdAttack"); //here for hit monster crowd
        crowd_AttacksScript = crowdAttackObject.GetComponent<Crowd_attacks>(); //here for hit monster crowd
        base_EnemyScript = Boss.GetComponent<Base_enemy>();
        healthSystem = player.GetComponent<HealthSystem>();
        enemi_Health = Boss.GetComponent<Enemi_health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 0, 0);

        maxSize = transform.localScale;
        transform.localScale = Vector3.zero;

        StartCoroutine(DestroyCount());
        //Invoke("IsPlayerHere", destroyTimer);
        //Invoke("DestroyObject", destroyTimer);

    }

    public void IsPlayerHere()
    {
        // Debug.Log("check if player circle");
        if (playerInCircle == true)
        {
            healthSystem.TakeDamage(damage);
        }

        if (BossInCircle == true)
        {
            //Insert sounds like klank so the player knows the boss has been hit but it dident do anything
            // KLONK
               
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyCount()
    {
        Color secondColor = spriteRenderer.color;
        float secondDestroyTime = 0;
        while (destroyTime <= destroyTimer)
        {
            destroyTime += Time.deltaTime;
            if(destroyTime <= destroyTimer * 0.9)
            {
                spriteRenderer.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.7f), destroyTime / (destroyTimer * 0.95f));
                secondColor = spriteRenderer.color;
            }
            else
            {
                spriteRenderer.color = Color.Lerp(secondColor, Color.red, secondDestroyTime / (destroyTimer * 0.05f));
                secondDestroyTime += Time.deltaTime;
            }
            transform.localScale = Vector3.Lerp(Vector3.zero, maxSize, destroyTime /destroyTimer);
            yield return null;
        }
        IsPlayerHere();
        DestroyObject();
        yield break;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {         
            BossInCircle = true;
        }

        if (other.CompareTag("Player"))
        {
            playerInCircle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            BossInCircle = false;
        }

        if (other.CompareTag("Player"))
        {
            playerInCircle = false;
        }
    }
}
