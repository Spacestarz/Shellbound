using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd_Projectile : MonoBehaviour
{
    public GameObject player;
    public Crowd_attacks crowd_Attacks;

    public bool crowdAttackHitGround = false;
    private Vector3 startPos;
    private Rigidbody rb;

    private Vector3 playerPOS;



    //testing 
    public GameObject attackIndicator;

    //antingen compare tag med ground och då kommer aoe och gör skada

    //ELLER bara säg efter 3 sec t ex så tar den som är inne i spere damage


    // Start is called before the first frame update
    
    void Start()
    {
        
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
      //  attackIndicator = GameObject.Find("Circle");

        Debug.Log("Awake");
    }

    // Update is called once per frame
    void Update()
    {
        playerPOS = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Ground"))
        {
            crowdAttackHitGround = true;
            Debug.Log("Ground ");

            crowd_Attacks.ThrowAttack();
         
        }       
    }

    public void Attack()
    {
       // attackIndicator.SetActive(true);
        transform.position = new Vector3(playerPOS.x, startPos.y, playerPOS.z);
 
        attackIndicator.transform.position = player.transform.position;
      
        //transform.position = startPos;  
        rb.velocity = transform.up * -10f;

    }

}
