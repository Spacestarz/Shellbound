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
    public int KnockbackStrenght; 
    public HealthSystem healthSystem;
    public Rigidbody rbGoblinShark;
    private Rigidbody rbplayer;
    private Collider playerCollider;
    public GameObject player;
    public GameObject Boss2; 

    public bool aoeattackGO = false;
    private bool timerIsRunning = false;
    
    //TODO
    // Make the boss spin? or att least make an aoe attack that knock back the player


    // Start is called before the first frame update
    void Start()
    {
        rbplayer = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {     
        Mouthattack();

        if (Input.GetKeyDown(KeyCode.B))
        {
            Aoeattack();     
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Dashattack();
        }

    }

    public void Dashattack()
    {
        //TODO
        //TODO CHANGE IN NAVMESH add casual more speed

        // add so the boss dashes forward
        // (only need to get a temporary boost probarly because its already facing the player and following them
        rbGoblinShark.isKinematic = false;
        rbGoblinShark.AddForce (transform.forward * 20 * 5);
        Debug.Log("Dash attack ");
        Invoke(nameof(KinematicOFF), 1f); 

        //red 
        //kolla linerenderer
        //look at event 
    }

    public void KinematicOFF()
    {
        rbGoblinShark.isKinematic = true;
    }

    public void Mouthattack()
    {
        //Similar to the harpoon and the crabs punch arm can probarly borrow a bit of code from that 
    }

    public void Aoeattack()
    {
        //TODO
        //Fix when this will go off in the combat
        // Make this on a new script? 

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player"))))
        {

            Vector3 direction = new Vector3(playerCollider.transform.position.x - transform.position.x, 
              0f,
             playerCollider.transform.position.z - transform.position.z).normalized;

            rbplayer.AddForce(direction * KnockbackStrenght * 10, ForceMode.Impulse);

            // see the knockback direction in the scene view
            Debug.DrawRay(transform.position, direction * 10f, Color.red, 10f);

            Debug.Log("PLAYER take dmg AUCH");
            healthSystem.TakeDamage(damage);

        }

        void OnDrawGizmos()
        {
           //DRAWS AREA OF AOE ATTACK
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(transform.position, radius);

        }

    }
}

