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
    bool rayHits = false;

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
            fired = true;

            Raycasting();    //TESTING raycast I REPEAT IN TESTING PHASE

            if (rayHits == false)
            {
                //It move the direction of the main cameras z axis
                GetComponent<Rigidbody>().velocity = mainCam.transform.forward * fireRate;
               
            }
          
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

    private void Raycasting()
    {

        //TODO make raycast check 1 meter ahead and if it hits bam
        //TODO MAKE THE RAYCAST WAIT UNTIL THE ROPE HAS ACTUALLY HIT A COLLISION THEN STOP
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1);

        if (Physics.Raycast(ray, out RaycastHit hit) )
        {
            Debug.Log (hit.collider.gameObject.name + "was hit"); //it works omg :) im learning FUCK YEE
            Vector3 hitpoint = hit.point;

            Debug.Log(hit.point);

            //TODO fix rayhits on the other stuff
          
            rayHits = true;
            rope.transform.position = hitpoint;
        }
     
    }
}
