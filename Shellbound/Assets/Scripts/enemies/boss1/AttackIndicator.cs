using System.Collections;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    [Header("Destroytimer of circle")]
    public float destroyTimer = 5;
    private float destroyTime = 0;

    private Vector3 preFabCirclePosition;
    private HealthSystem healthSystem;

    private GameObject player;
    private int damage = 1;

    private bool playerInCircle = false;

    public GameObject preFabCircle;

    private SpriteRenderer spriteRenderer;
    private Vector3 maxSize;

    void Awake()
    {
        player = GameObject.Find("Player");
        healthSystem = player.GetComponent<HealthSystem>();
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
