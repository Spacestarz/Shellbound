using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class Boss2_attacks : MonoBehaviour
{
    //testing spere thing
    private float radius = 10f;


    private int damage = 12;
    public HealthSystem healthSystem;

    public bool aoeattackGO = false;
    private bool timerIsRunning = false;

    //TODO
    // Make the boss spin? or att least make an aoe attack that knock back the player


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Dashattack();

        Mouthattack();



        if (Input.GetKeyDown(KeyCode.B))
        {
            Aoeattack();
           
        }

    }

    public void Dashattack()
    {

    }

    public void Mouthattack()
    {

    }

    public void Aoeattack()
    {
        //TODO
        //Fix when this will go off in the combat
        //ALSO ADD KNOCKBACK

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player"))))
        {
          //  HealthSystem = Player.GetComponent<HealthSystem>();

            Debug.Log("PLAYER take dmg AUCH");
            //healthSystem.TakeDamage(damage);

        }


         void OnDrawGizmos()
        {
            // Set the color for the Gizmos
            Gizmos.color = Color.red;
            // Draw a wireframe sphere
            Gizmos.DrawWireSphere(transform.position, radius);

        }



    }
}

