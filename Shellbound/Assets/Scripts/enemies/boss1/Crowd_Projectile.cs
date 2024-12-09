using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd_Projectile : MonoBehaviour
{
    public GameObject player;
    public Crowd_attacks crowd_Attacks;

    public bool crowdAttackHitGround = false;
    public Vector3 startPos;
    private Rigidbody rb;

    //The circle 
    public GameObject attackIndicator;

    [Header("Speed of projectile")]
    [SerializeField] private float speed = 2f;

    //todo
    //DELETE THIS SCRIPT!


    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Ground"))
        {
            crowdAttackHitGround = true;

            crowd_Attacks.ThrowAttack(this);
        }       
    }
    */

    public void Attack(Vector3 target)
    {
        transform.position = new Vector3(target.x, startPos.y, target.z);

        GetComponent<Rigidbody>().velocity = Vector3.down * speed;
    }
}
