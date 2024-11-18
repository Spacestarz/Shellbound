using System;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Anchor;
    public GameObject harpoon;
    public GameObject mainCam;
    public float fireRate = 2;
    public float speedReturn = 1;

    public float maxDistancefromAnchor = 2f; //test this to see whats best


    //bools
    public bool fired = false;
    bool velocityZero = false;
    bool collisionHIT = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        BeInvisible(); 
    }

 

    // Update is called once per frame
    void Update()
    {

        if (velocityZero)
        {
            BeInvisible();        
        }
        
        //distance of the Anchor and rope
        float dist = Vector3.Distance(Anchor.transform.position, transform.position);

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
          if ( dist <1)
            {
                rope.transform.position = Anchor.transform.position;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                BeInvisible();
                fired = false;
                velocityZero = false;
                collisionHIT = false;

            }
        }

    }

    public void FireHarpoon()
    {
        //TODO MAKE PLAYER NOT BE ABLE TO SHOOT WHEN IT GOES BACK!

        BeVisible();
        rope.transform.position = Anchor.transform.position;

        velocityZero = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //it gets the same rotation as the main camera will probarly need to be changed when the real sprites gets implemented.  
        transform.rotation = Quaternion.LookRotation(mainCam.transform.up, mainCam.transform.forward);
        fired = true;

        if (collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            GetComponent<Rigidbody>().velocity = mainCam.transform.forward * fireRate;

        }
    }

    private void BeVisible()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    private void BeInvisible()
    {
       
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false; 
        }
    }

    public void OnTriggerEnter(Collider collisioncheck)
    {
        if (collisioncheck.CompareTag("Enemy"))
        {
            collisionHIT = true;

            // Find the closest point on the collided object's surface to the rope
            Vector3 closestPoint = collisioncheck.ClosestPoint(rope.transform.position);

            // Log the closest point for debugging
            Debug.Log("Closest point on collision surface: " + closestPoint);

            // Move the rope to this closest point
            //make a lerp to make it more smooth?
            rope.transform.position = closestPoint;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            // Optionally, stop further rope movement or implement other logic
            Debug.Log("Rope stuck at: " + closestPoint);
        }
    }

}
