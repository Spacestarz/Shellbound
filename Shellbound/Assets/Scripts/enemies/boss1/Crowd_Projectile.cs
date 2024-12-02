using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd_Projectile : MonoBehaviour
{
    public Transform player;
    public Crowd_attacks crowd_Attacks;

    public bool crowdAttackHitGround = false;
    private Vector3 startPos;
    private Rigidbody rb;


    //antingen compare tag med ground och då kommer aoe och gör skada

    //ELLER bara säg efter 3 sec t ex så tar den som är inne i spere damage

    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Ground"))
        {
            crowdAttackHitGround = true;
            Debug.Log("Ground hello");

            crowd_Attacks.ThrowAttack();

            
        } 

        
    }

    public void Attack()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.y);
        //transform.position = startPos;  
        rb.velocity = transform.up * -10f;
    }


}
