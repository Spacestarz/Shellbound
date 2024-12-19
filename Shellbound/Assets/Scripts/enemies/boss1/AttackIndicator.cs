using DG.Tweening;
using System.Collections;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    [Header("Destroytimer of circle")]
    public float destroyTimer = 3;
    private float destroyTime = 0;

    private Vector3 preFabCirclePosition;
    private HealthSystem healthSystem;
    private GameObject player;
    private int damage = 1;

    private bool playerInCircle = false;
    

    public GameObject preFabCircle;
    GameObject spike;

    private SpriteRenderer spriteRenderer;
    private Vector3 maxSize;
    private Base_enemy base_EnemyScript;


    void Awake()
    {
        player = GameObject.Find("Player");
        healthSystem = player.GetComponent<HealthSystem>();

        spike = transform.GetChild(0).gameObject;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 0, 0);

        //maxSize = transform.localScale;
        //transform.localScale = Vector3.zero;

        StartCoroutine(WarningCount());
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
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private IEnumerator WarningCount()
    {
        Color secondColor = spriteRenderer.color;
        float secondDestroyTime = 0;
        while (destroyTime <= destroyTimer)
        {
            destroyTime += Time.deltaTime;
            if(destroyTime <= destroyTimer * 0.5)
            {
                spriteRenderer.color = Color.Lerp(new Color(0, 0, 0, 0.2f), new Color(1, 1, 1, 0.7f), destroyTime / (destroyTimer * 0.50f));
                secondColor = spriteRenderer.color;
            }
            else
            {
                spriteRenderer.color = Color.Lerp(secondColor, Color.red, secondDestroyTime / (destroyTimer * 0.50f));
                secondDestroyTime += Time.deltaTime;
            }
            //transform.localScale = Vector3.Lerp(Vector3.zero, maxSize, destroyTime /destroyTimer);
            yield return null;
        }
        IsPlayerHere();
        DeploySpike();
        yield break;
    }


    private void DeploySpike()
    {
        spriteRenderer.DOColor(new Color(0,0,0,0), 0.2f);
        spike.transform.DOLocalMoveZ(0.2f, 0.15f).OnComplete(InvokeReturnSpike);
    }


    private void InvokeReturnSpike()
    {
        Invoke(nameof(ReturnSpike), 0.5f);
    }


    private void ReturnSpike()
    {
        spike.transform.DOLocalMoveZ(-0.21f, 0.5f).OnComplete(DestroyObject);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCircle = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCircle = false;
        }
    }
}
