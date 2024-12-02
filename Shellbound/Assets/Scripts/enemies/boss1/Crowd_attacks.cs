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
    private int damage = 5;
    private float radius = 2;

    public HealthSystem healthSystem;
    private Crowd_Projectile Crowd_Projectile;
    // Start is called before the first frame update
    void Start()
    {
       attackIndicator = GameObject.Find("Circle");
       Crowd_Projectile = GetComponentInChildren<Crowd_Projectile>();
       projectileRB = projectile.GetComponent<Rigidbody>();
       attackIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
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

        Crowd_Projectile.Attack();
        
       // projectileRB.velocity = transform.up * -10;


        transform.position = player.transform.position;
        attackIndicator.transform.position = player.transform.position;
        attackIndicator.SetActive(true);

        Invoke("ThrowAttack", 0.1f);
    }

    public void ThrowAttack()
    {
           
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player") && Crowd_Projectile.crowdAttackHitGround == true)))
        {
            Debug.Log("Player take damage");
            healthSystem.TakeDamage(damage);

            Crowd_Projectile.crowdAttackHitGround = false;
        }
    }

    private void OnDrawGizmos()
    {
        //DRAWS AREA OF AOE ATTACK
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
        Debug.Log("Drawing for circle spere");
    }
    

 }
