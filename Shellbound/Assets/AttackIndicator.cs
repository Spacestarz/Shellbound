using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
   
    private GameObject crowdScriptLocation;

    [Header("Destroytimer of circle")]
    public float destroyTimer = 5;

    private Vector3 preFabCirclePosition;
    private HealthSystem healthSystem;

    private GameObject player;
    private int damage = 1;

    private bool playerInCircle = false;

    public GameObject preFabCircle;
   
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        healthSystem = player.GetComponent<HealthSystem>();

        Invoke("IsPlayerHere", destroyTimer);
          
    }

    // Update is called once per frame
    void Update()
    {
        
        Invoke("DestroyObject", destroyTimer);
    }

    public void IsPlayerHere()
    {
        Debug.Log("check if player circle");
        if (playerInCircle == true)
        {
            healthSystem.TakeDamage(damage);

        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
           playerInCircle = true;

        }

        else
        {
            
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
