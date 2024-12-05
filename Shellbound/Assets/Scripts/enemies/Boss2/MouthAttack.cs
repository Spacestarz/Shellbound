using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MouthAttack : MonoBehaviour
{

    public Rigidbody mouthRigidBody;
    public GameObject player;
    public GameObject boss;
    public GameObject mouth;

    public GameObject mouthObject;
    public GameObject Anchor;
    public GameObject mainCam;
    public float fireRate = 20;
    public float speedReturn = 1;
    float dist;

    public float maxDistancefromAnchor = 15f; //test this to see whats best


    //bools
    public bool fired = false;
    public bool goingAway = false;
    private bool collisionHIT = false;

    private float maxDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //stealing a bit from the fire script


        dist = Vector3.Distance(Anchor.transform.position, transform.position);


        if (dist > maxDistancefromAnchor && goingAway)
        {
            MouthGoBack();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            FireMouth();
           
        }

        
    }

    private void MouthGoBack()
    {
       // mouthObject.velocity = Vector3.zero;
        mouthObject.transform.position = Vector3.Lerp(mouthObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
        Debug.Log("mouthgoback");
        collisionHIT = false;
    }

    public void FireMouth()
    {
       // mouthObject.SetActive(true);
        //harpoon.SetVisibility(true);
        goingAway = true;

        mouthObject.transform.position = Anchor.transform.position;

        mouthRigidBody.constraints = RigidbodyConstraints.FreezeRotation;

        fired = true;
        mouth.transform.position = Vector3.MoveTowards(mouth.transform.position, player.transform.position, maxDistance * Time.deltaTime);

        if (collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            mouthRigidBody.velocity = mainCam.transform.forward * fireRate;

        }
    }

    public void OnTriggerEnter(Collider collisionCheck)
    {
        if (goingAway && collisionCheck.CompareTag("Player"))
        {
            Debug.Log("player here");
            collisionHIT = true;
        }
        else if (goingAway)
        {
            MouthGoBack();
        }
    }


}
