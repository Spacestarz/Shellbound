using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MouthAttack : MonoBehaviour
{

    public Rigidbody mouthRigidBody;
    public GameObject player;
    //public GameObject boss;
    public GameObject mouth;

    //public GameObject MouthAnchor;
    public GameObject Anchor;
    public GameObject mainCam;
    public float fireRate = 20;
    public float speedReturn = 1;
    float dist;
    Base_enemy enemy;
    Boss2_attacks attacks;

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
        enemy = GetComponentInParent<Base_enemy>();
        attacks = GetComponentInParent<Boss2_attacks>();
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
            FireMouth(20,1,15);       
        }       
    }

    private IEnumerator MouthGoBack()
    {
       // Debug.Log("mouthgoback METHOD");

        mouthRigidBody.velocity = Vector3.zero;
        while (dist >= 1)
        {
            mouth.transform.position = Vector3.Lerp(mouth.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
            yield return null;
        }

        mouth.transform.position = Anchor.transform.position;
        mouthRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        //mouthObject.transform.position = Vector3.Lerp(mouthObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
        //Debug.Log("Mouth is home");
        collisionHIT = false;
        attacks.BeInvisible(gameObject);
        enemy.start();
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

    public IEnumerator FireMouth(float MouthSpeed, float MouthReturn, float MouthDistance)
    {
        fireRate = MouthSpeed;
        speedReturn = MouthReturn;
        maxDistancefromAnchor = MouthDistance;
        enemy.stop();
        yield return new WaitForSeconds(2);
        attacks.BeVisible(gameObject);
        Anchor.transform.LookAt(player.transform);
        sr.enabled = true;
       // mouthObject.SetActive(true);
        //harpoon.SetVisibility(true);
        goingAway = true;

        mouth.transform.position = Anchor.transform.position;

        mouthRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; ;
        
        fired = true;

        mouth.transform.position = Vector3.MoveTowards(mouth.transform.position, player.transform.position, maxDistance * Time.deltaTime);
        
        if (collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            mouthRigidBody.velocity = transform.forward * fireRate;
            
        }
        
    }

    public void OnTriggerEnter(Collider collisionCheck)
    {
        //make a ontriggerstay? So the player gets continued eaten on if they dont dash away? 
        // the player will be stuck in like 1 sec and then they can dash away.

        if (goingAway && collisionCheck.CompareTag("Player"))
        {
            collisionHIT = true;
            // Debug.Log("player here");
            StartCoroutine(nameof(MouthGoBack));
            Debug.Log("Go back because of player");
        }
        /*
        else if (goingAway)
        {
            Debug.Log("mouthattack 99");
            MouthGoBack();
        }
        */
    }
}
