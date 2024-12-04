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

   
    private Vector3 attackIndicatorPosition;

    //time when the red circle gets destroyed
    [Header("Destroytimer of circle and projectile")]
    [SerializeField] private float destroyTimer = 5;

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
            attackIndicatorPosition = new Vector3(player.transform.position.x, (float)0.04, player.transform.position.z);
            attackIndicator.transform.position = attackIndicatorPosition;


            var newAttack = Instantiate(attackIndicator, attackIndicatorPosition, Quaternion.Euler(-90,0,0)); //the rotation need to be in -90 degree
            newAttack.SetActive(true);
            Destroy(newAttack, destroyTimer);
            WhereIsPlayer();

        }

        /*
        TODO 
        make circle not float above ground.
        */
    }

    private void WhereIsPlayer()
    {   
        transform.position = player.transform.position;

        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Destroy(newProjectile, destroyTimer);

        newProjectile.GetComponent<Crowd_Projectile>().Attack(transform.position);

        ThrowAttack(newProjectile.GetComponentInChildren<Crowd_Projectile>());
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
