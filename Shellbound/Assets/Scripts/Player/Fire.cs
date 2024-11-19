using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Rigidbody harpoonRigidBody;
    Harpoon harpoon;

    public GameObject Anchor;
    public GameObject harpoonObject;
    public GameObject mainCam;
    public float fireRate = 2;
    public float speedReturn = 1;

    float dist;
    public float maxDistancefromAnchor = 15f; //test this to see whats best


    //bools
    public bool fired = false;
    public bool goingAway = false;
    bool velocityZero = false;

    // Start is called before the first frame update
    void Start()
    {

        harpoonRigidBody = harpoonObject.GetComponent<Rigidbody>();
        harpoon = harpoonObject.GetComponent<Harpoon>();

        harpoonRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        harpoonRigidBody.useGravity = false;
    }



    // Update is called once per frame
    void Update()
    {
        //distance of the Anchor and rope
        dist = Vector3.Distance(Anchor.transform.position, harpoonObject.transform.position);
        if (dist > maxDistancefromAnchor)
        {
            ReturnHarpoon();
        }

        ////TODO make it check the pos and move 10% towards it and then check where is the pos etc
        ////Add methods to make the code cleaner
        //if (Input.GetKeyDown(KeyCode.K) || dist >= maxDistancefromAnchor)
        //{
        //    harpoon.SetVisibility(true);
        //    goingAway = false;
        //    harpoonRigidBody.velocity = Vector3.zero;
        //    velocityZero = true;
        //    harpoonObject.GetComponent<Harpoon>().collisionHIT = false;

        //    if (harpoonObject.GetComponent<Harpoon>().caughtObject != null)
        //    {
        //        harpoonObject.GetComponent<Harpoon>().caughtObject.GetComponent<Enemi_Health>().EnableAI();
        //        harpoonObject.GetComponent<Harpoon>().caughtObject = null;
        //    }
        //}

        //if (velocityZero == true)
        //{
            
        //    //TODO LATER
        //    //to make this look nicer change it to a lerp.
        //    harpoonObject.transform.position = Vector3.MoveTowards(harpoonObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);

        //    //if the distance of the rope and Anchor is below 5 it snaps to position and freezez
        //    if (dist < 1)
        //    {
        //        harpoonObject.transform.position = Anchor.transform.position;
        //        harpoonRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        //        harpoon.SetVisibility(false);
        //        fired = false;
        //        velocityZero = false;
        //    }
        //}
    }

    public void FireHarpoon()
    {
        //TODO MAKE PLAYER NOT BE ABLE TO SHOOT WHEN IT GOES BACK!
        harpoon.SetVisibility(true);
        goingAway = true;

        harpoonObject.transform.position = Anchor.transform.position;

        velocityZero = false;
        harpoonRigidBody.constraints = RigidbodyConstraints.None;

        //it gets the same rotation as the main camera will probarly need to be changed when the real sprites gets implemented.  
        harpoonObject.transform.rotation = Quaternion.LookRotation(mainCam.transform.up, mainCam.transform.forward);
        fired = true;

        if (harpoonObject.GetComponent<Harpoon>().collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            harpoonRigidBody.velocity = mainCam.transform.forward * fireRate;

        }
    }

    public void ReturnHarpoon()
    {
        harpoon.SetVisibility(true);
        goingAway = false;
        harpoonRigidBody.velocity = Vector3.zero;
        velocityZero = true;
        harpoonObject.GetComponent<Harpoon>().collisionHIT = false;

        if (harpoonObject.GetComponent<Harpoon>().caughtObject != null)
        {
            harpoonObject.GetComponent<Harpoon>().caughtObject.GetComponent<Enemi_Health>().EnableAI();
            harpoonObject.GetComponent<Harpoon>().caughtObject = null;
        }

        StartCoroutine(nameof(MoveHarpoonBack));
        //harpoonObject.transform.position = Vector3.MoveTowards(harpoonObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
    }

    IEnumerator MoveHarpoonBack()
    {
        while (dist >= 1)
        {
            harpoonObject.transform.position = Vector3.Lerp(harpoonObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
            yield return null;
        }

        harpoonObject.transform.position = Anchor.transform.position;
        harpoonRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        harpoon.SetVisibility(false);
        fired = false;
        velocityZero = false;

        yield break;
    }
}
