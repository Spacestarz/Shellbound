using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Crowd_attacks : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    private GameObject attackIndicator;

    private Rigidbody projectileRB;
    private int damage = 1;
    private float radius = 2;

    public HealthSystem healthSystem;
    private Crowd_Projectile Crowd_Projectile;

    // Start is called before the first frame update
    void Start()
    {
       //got the attackindicator here to just make it disappear on start
       //and the rest is on the projectile script. 
       attackIndicator = GameObject.Find("Circle");
       attackIndicator.SetActive(false);

       Crowd_Projectile = GetComponentInChildren<Crowd_Projectile>();
       projectileRB = projectile.GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            attackIndicator.SetActive(true);
            WhereIsPlayer();
        }
      
        /*
        TODO 
        Check where the player is and make the attack go there. 
        Make it have a aoe circle so player can see where the attack will strike. 

        one circle soon done
        maybe get like triple projectiles in a row or in random places?
        */
    }

    private void WhereIsPlayer()
    {
        transform.position = player.transform.position;
        Crowd_Projectile.Attack();

        Invoke("ThrowAttack", 0.1f);
    }

    public void ThrowAttack()
    {

        //make hit ground gets false even if it doesent hit the player
           
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
       
        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player") && Crowd_Projectile.crowdAttackHitGround == true)))
        {
            Debug.Log("Player take damage");
            healthSystem.TakeDamage(damage);

            Crowd_Projectile.crowdAttackHitGround = false;
          
        }

        else if(hitColliders.Any(hitCollider => hitCollider.CompareTag("Ground")) && Crowd_Projectile.crowdAttackHitGround == true)
        {
            Crowd_Projectile.crowdAttackHitGround = false;
           // Debug.Log("Dident hit player sad");
        }

       
        
    }

    private void OnDrawGizmos()
    {
        //DRAWS AREA OF AOE ATTACK
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
        
    }
    

 }
