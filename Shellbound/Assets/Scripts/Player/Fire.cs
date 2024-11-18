using UnityEngine;

public class Fire : MonoBehaviour
{
    Rigidbody harpoonRigid;

    public GameObject Anchor;
    public GameObject harpoon;
    public GameObject mainCam;
    public float fireRate = 2;
    public float speedReturn = 1;

    public float maxDistancefromAnchor = 2f; //test this to see whats best


    //bools
    public bool fired = false;
    public bool goingAway = false;
    bool velocityZero = false;

    // Start is called before the first frame update
    void Start()
    {
        harpoonRigid = harpoon.GetComponent<Rigidbody>();
        harpoonRigid.constraints = RigidbodyConstraints.FreezeAll;
        harpoonRigid.useGravity = false;

        BeInvisible();
    }



    // Update is called once per frame
    void Update()
    {
        
        //if (velocityZero)
        //{
        //    BeInvisible();
        //}

        //distance of the Anchor and rope
        float dist = Vector3.Distance(Anchor.transform.position, harpoon.transform.position);
        Debug.Log(dist);

        //TODO make it check the pos and move 10% towards it and then check where is the pos etc
        //Add methods to make the code cleaner
        if (Input.GetKeyDown(KeyCode.K) || dist >= maxDistancefromAnchor)
        {
            harpoonRigid.velocity = Vector3.zero;
            velocityZero = true;
            fired = false;
            harpoon.GetComponent<Harpoon>().collisionHIT = false;
        }

        if (velocityZero == true)
        {
            //TODO LATER
            //to make this look nicer change it to a lerp.
            harpoon.transform.position = Vector3.MoveTowards(harpoon.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);

            //if the distance of the rope and Anchor is below 5 it snaps to position and freezez
            if (dist < 1)
            {
                harpoon.transform.position = Anchor.transform.position;
                harpoonRigid.constraints = RigidbodyConstraints.FreezeAll;

                BeInvisible();
                fired = false;
                velocityZero = false;
            }
            else
            {
                Debug.Log("dist more than 1");
            }
        }

    }

    public void FireHarpoon()
    {
        //TODO MAKE PLAYER NOT BE ABLE TO SHOOT WHEN IT GOES BACK!

        BeVisible();
        harpoon.transform.position = Anchor.transform.position;

        velocityZero = false;
        harpoonRigid.constraints = RigidbodyConstraints.None;

        //it gets the same rotation as the main camera will probarly need to be changed when the real sprites gets implemented.  
        harpoon.transform.rotation = Quaternion.LookRotation(mainCam.transform.up, mainCam.transform.forward);
        fired = true;

        if (harpoon.GetComponent<Harpoon>().collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            harpoonRigid.velocity = mainCam.transform.forward * fireRate;

        }
    }

    private void BeVisible()
    {
        var renderer = harpoon.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    private void BeInvisible()
    {

        var renderer = harpoon.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

    //public void OnTriggerEnter(Collider collisioncheck)
    //{
    //    if (collisioncheck.CompareTag("Enemy"))
    //    {
    //        collisionHIT = true;

    //        // Find the closest point on the collided object's surface to the rope
    //        Vector3 closestPoint = collisioncheck.ClosestPoint(harpoon.transform.position);

    //        // Log the closest point for debugging
    //        Debug.Log("Closest point on collision surface: " + closestPoint);

    //        // Move the rope to this closest point
    //        //make a lerp to make it more smooth?
    //        harpoon.transform.position = closestPoint;
    //        harpoonRigid.constraints = RigidbodyConstraints.FreezeAll;

    //        // Optionally, stop further rope movement or implement other logic
    //        Debug.Log("Rope stuck at: " + closestPoint);
    //    }
    //}

}
