using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class Crowd_attacks : MonoBehaviour
{
    private GameObject player;
  //  private GameObject attackIndicator;
    private int damage = 1;
    private float radius = 2;
    public GameObject preFabCircle;

    private HealthSystem healthSystem;


    //private Vector3 attackIndicatorPosition;
    private Vector3 preFabCirclePosition;

    //testing to make courutines
    private IEnumerator coroutine;

    private bool playerInCircle = false;

    //TODO FIX WITH INSTANCE THING
    public GameObject instance;

    //time when the red circle gets destroyed
    [Header("Destroytimer of circle")]
    [SerializeField] private float destroyTimer = 5;

    /*
    //TODO AND FIXES ETC
    no projectile just after x time if player is in the attackindicator take damage
    make the attackindicator spawn with a overlapspere so the player if they go back to one circle can take dmg. 

    */
    // Start is called before the first frame update
    void Start()
    {
        //got the attackindicator here to just make it disappear on start
        //and the rest is on the projectile script. 
       // attackIndicator = GameObject.Find("attackIndicator");
        //attackIndicator.SetActive(false);

        player = GameObject.Find("Player");
        healthSystem = player.GetComponent<HealthSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        /*

        if (Input.GetKeyDown(KeyCode.R))
        {
            attackIndicatorPosition = new Vector3(player.transform.position.x, (float)0.04, player.transform.position.z);
            attackIndicator.transform.position = attackIndicatorPosition;


            var newAttack = Instantiate(attackIndicator, attackIndicatorPosition, Quaternion.Euler(-90, 0, 0)); //the rotation need to be in -90 degree
            newAttack.SetActive(true);

            StartCoroutine(nameof(IsPlayerHere) , destroyTimer);
            Destroy(newAttack, destroyTimer);

            //healthSystem.TakeDamage(damage);          

        }
        */

        if (Input.GetKeyDown(KeyCode.R))
        {
            preFabCirclePosition = new Vector3(player.transform.position.x, (float)0.04, player.transform.position.z);
            preFabCircle.transform.position = preFabCirclePosition;


            Instantiate(preFabCircle, preFabCirclePosition, Quaternion.Euler(-90, 0, 0)); //the rotation need to be in -90 degree
            //newAttack.SetActive(true);

            StartCoroutine(nameof(IsPlayerHere), destroyTimer);
            Destroy(preFabCircle, destroyTimer);

            //healthSystem.TakeDamage(damage);

        }

    }

    private IEnumerator IsPlayerHere()
    {
       
        if (playerInCircle == true)
        {
            healthSystem.TakeDamage(damage);
            
            Debug.Log("Player take damage");
            yield return null;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Collider attackIndicatorCollider = GetComponentInChildren<Collider>();

        if (other.CompareTag("Player"))
        {
            playerInCircle = true;
            Debug.Log(playerInCircle);

        }

        else
        {

            Debug.Log("Player not here");
        }
    
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exit the circle");
            playerInCircle = false;
            Debug.Log(playerInCircle);
        }
       
    }



    private void OnDrawGizmos()
    {
        //DRAWS AREA OF AOE ATTACK
        Gizmos.color = Color.red;

        // Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    /*
    public void ThrowAttack( )
    {

        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        Collider[] hitColliders = Physics.OverlapSphere(attackIndicator.transform.position, radius);

        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player"))))
        {
             Debug.Log("Player take damage");
            healthSystem.TakeDamage(damage);

            
        }

    */



    /*
    private void WhereIsPlayer()
    {   
        transform.position = player.transform.position;

        //var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        //Destroy(newProjectile, destroyTimer);

        //newProjectile.GetComponent<Crowd_Projectile>().Attack(transform.position);

        //do an invoke instead so make it based on time instead of collision based probarly
        //NEED TO MAKE A courutine instead because because

        coroutine = DelayThrowAttack(2f);
        StartCoroutine(coroutine);


    }


    private IEnumerator DelayThrowAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //ThrowAttack(newProjectile.GetComponentInChildren<Crowd_Projectile>());
    }

    public void ThrowAttack(Crowd_Projectile newProjectile)
    {

        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        Collider[] hitColliders = Physics.OverlapSphere(newProjectile.transform.position, radius);

        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player") && newProjectile.crowdAttackHitGround == true)))
        {
           // Debug.Log("Player take damage");
            healthSystem.TakeDamage(damage);

            newProjectile.crowdAttackHitGround = false;        
        }

        else if(hitColliders.Any(hitCollider => hitCollider.CompareTag("Ground")) && newProjectile.crowdAttackHitGround == true)
        {
            newProjectile.crowdAttackHitGround = false;
            //Debug.Log("Dident hit player sad");
        }
       */
}








