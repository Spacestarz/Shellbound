using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Rigidbody harpoonRigidBody;
    Harpoon harpoon;
    HarpoonLine harpoonLine;

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

        harpoon = harpoonObject.GetComponent<Harpoon>();
        harpoonRigidBody = harpoonObject.GetComponent<Rigidbody>();
        harpoonLine = harpoonObject.GetComponent<HarpoonLine>();

        harpoonRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        harpoonRigidBody.useGravity = false;
    }

    void Update()
    {
        //distance of the Anchor and rope
        dist = Vector3.Distance(Anchor.transform.position, harpoonObject.transform.position);
        if (dist > maxDistancefromAnchor && goingAway)
        {
            ReturnHarpoon();
        }
    }

    public void FireHarpoon()
    {
        harpoon.SetVisibility(true);
        goingAway = true;

        harpoonObject.transform.position = Anchor.transform.position;

        velocityZero = false;
        harpoonRigidBody.constraints = RigidbodyConstraints.None;

        fired = true;

        if (harpoonObject.GetComponent<Harpoon>().collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            harpoonRigidBody.velocity = mainCam.transform.forward * fireRate;

        }
    }

    public void ReturnHarpoon()
    {
        if (PlayerSlice.SliceMode())
        {
            PlayerSlice.SetSliceMode(false);
        }

        harpoon.SetVisibility(true);
        goingAway = false;
        harpoonRigidBody.velocity = Vector3.zero;
        velocityZero = true;
        harpoonObject.GetComponent<Harpoon>().collisionHIT = false;

        if (harpoonObject.GetComponent<Harpoon>().caughtObject != null)
        {
            harpoonObject.GetComponent<Harpoon>().caughtObject.GetComponent<Enemi_Health>().EnableAI();
            harpoonObject.GetComponent<Harpoon>().caughtObject = null;
            Harpoon.hasCaught = false;
        }

        StartCoroutine(nameof(MoveHarpoonBack));
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
