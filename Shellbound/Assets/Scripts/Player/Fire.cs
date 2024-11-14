using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Anchor;
    public GameObject rope;
    public GameObject mainCam;
    public float fireRate = 2;
    public float speedReturn = 1;

    public float maxDistancefromAnchor = 2f; //test this to see whats best


    //bools
    bool fired = false;
    bool velocityZero = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        //distance of the Anchor and rope
        float dist = Vector3.Distance(Anchor.transform.position, transform.position);
        

        if (Input.GetButtonDown("Fire1"))
        {
            velocityZero = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            //it gets the same rotation as the main camera will probarly need to be changed when the real sprites gets implemented.  
            transform.rotation = Quaternion.LookRotation(mainCam.transform.up, mainCam.transform.forward);
           //It move the direction of the main cameras z axis
            GetComponent<Rigidbody>().velocity =  mainCam.transform.forward * fireRate * Time.deltaTime;

            fired = true;
        }

        //TODO AFTER X seconds it returns to the player //invoke
        //make it check the pos and move 10% towards it and then check where is the pos etc
        //Add methods to make the code cleaner
        if (Input.GetButtonDown("Jump") || dist >= maxDistancefromAnchor)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            velocityZero = true;
            fired = false;
        }

        if (velocityZero == true) 
        {
            //TODO LATER
            //to make this look nicer change it to a lerp.
            rope.transform.position = Vector3.MoveTowards(rope.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);          

            //if the distance of the rope and Anchor is below 5 it snaps to position and freezez
          if ( dist <5)
            {
                rope.transform.position = Anchor.transform.position;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                velocityZero = false;
            }
        }

    }    
    
}
