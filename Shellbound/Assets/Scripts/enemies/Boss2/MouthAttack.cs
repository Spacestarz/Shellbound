using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MouthAttack : MonoBehaviour
{

    public Rigidbody mouthRigidBody;
    private GameObject player;
    private GameObject boss;
    public GameObject mouth;

    public GameObject mouthObject;
    public GameObject Anchor;
    public GameObject mainCam;
    public float fireRate = 20;
    public float speedReturn = 1;
    float dist;

    public float maxDistancefromAnchor = 15f; //test this to see whats best

    private SpriteRenderer sr;

    //bools
    public bool fired = false;
    public bool goingAway = false;
    private bool collisionHIT = false;

    private float maxDistance = 10f;

    //TODO
    //MAKE MOUTH INVISIBLE WHEN ON THE BOSS

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;

       // player = getgame
    }

    // Update is called once per frame
    void Update()
    {
        //stealing a bit from the fire script
        dist = Vector3.Distance(Anchor.transform.position, transform.position);

        if (dist > maxDistancefromAnchor && goingAway)
        {
            StartCoroutine(nameof(MouthGoBack));
           // MouthGoBack();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            FireMouth();
           
        }
        
    }

    private IEnumerator MouthGoBack()
    {
        Debug.Log("mouthgoback METHOD");
        mouthRigidBody.velocity = Vector3.zero;
        while (dist >= 1)
        {
            mouthObject.transform.position = Vector3.Lerp(mouthObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
            yield return null;
        }

        mouthObject.transform.position = Anchor.transform.position;
        mouthRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        //mouthObject.transform.position = Vector3.Lerp(mouthObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
        Debug.Log("mouthgoback");
        collisionHIT = false;
        yield return null;
    }

    /*
    private void MouthGoBack()
    {
        mouthRigidBody.velocity = Vector3.zero;
        mouthObject.transform.position = Vector3.Lerp(mouthObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
        Debug.Log("mouthgoback");
        collisionHIT = false;
    }
    */
    public void FireMouth()
    {
        sr.enabled = true;
       // mouthObject.SetActive(true);
        //harpoon.SetVisibility(true);
        goingAway = true;

        mouthObject.transform.position = Anchor.transform.position;

        mouthRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; ;
        

        fired = true;

        mouth.transform.position = Vector3.MoveTowards(mouth.transform.position, player.transform.position, maxDistance * Time.deltaTime);

        if (collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            mouthRigidBody.velocity = boss.transform.forward * fireRate;

        }
    }

    public void OnTriggerEnter(Collider collisionCheck)
    {
        //make a ontriggerstay? So the player gets continued eaten on if they dont dash away? 
        // the player will be stuck in like 1 sec and then they can dash away.

        if (goingAway && collisionCheck.CompareTag("Player"))
        {
            Debug.Log("player here");
            collisionHIT = true;
            MouthGoBack();
            Debug.Log("Time to go back");
        }

        else if (goingAway)
        {
            Debug.Log("mouthattack 99");
            MouthGoBack();
        }
    }
}
