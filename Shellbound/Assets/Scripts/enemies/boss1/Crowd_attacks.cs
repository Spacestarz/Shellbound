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

    // Start is called before the first frame update
    void Start()
    {
       //got the attackindicator here to just make it disappear on start
       //and the rest is on the projectile script. 
       attackIndicator = GameObject.Find("Circle");
       attackIndicator.SetActive(false);

       projectileRB = projectile.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            //tror attackindicator ej får spelarens position för den får den infon av crowd_projectile?
            var newAttack = Instantiate(attackIndicator, player.transform.position, Quaternion.identity); //the rotation need to be in -90 degree
            newAttack.SetActive(true);
            Destroy(newAttack, 5);
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

        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        newProjectile.GetComponent<Crowd_Projectile>().Attack(transform.position);

        attackIndicator.transform.position = player.transform.position;

        ThrowAttack(newProjectile.GetComponentInChildren<Crowd_Projectile>());
        //Invoke("ThrowAttack", 0.1f);
    }

    public void ThrowAttack(Crowd_Projectile newProjectile)
    {
              
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
       
        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player") && newProjectile.crowdAttackHitGround == true)))
        {
            Debug.Log("Player take damage");
            healthSystem.TakeDamage(damage);

            newProjectile.crowdAttackHitGround = false;
          
        }

        else if(hitColliders.Any(hitCollider => hitCollider.CompareTag("Ground")) && newProjectile.crowdAttackHitGround == true)
        {
            newProjectile.crowdAttackHitGround = false;
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
