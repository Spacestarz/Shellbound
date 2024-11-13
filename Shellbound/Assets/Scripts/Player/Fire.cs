using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Harpoon;
    public GameObject rope;
    public float fireRate = 2;
    public float speedReturn = 1;

    public float maxDistanceHarpoon = 2f; //test this to see whats besst

    //bools
    bool fired = false;
    bool velocityZero = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Harpoon.transform.position, transform.position);

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<Rigidbody>().velocity = transform.up * fireRate * Time.deltaTime;

            fired = true;
        }

        //TODO AFTER X seconds it returns to the player //invoke
        //make it check the pos and move 10% towards it and then check where is the pos etc
        //Add methods to make the code cleaner
        if (Input.GetButtonDown("Jump") || dist >= maxDistanceHarpoon)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            velocityZero = true;
            fired = false;
        }

        if (velocityZero) 
        {
            rope.transform.position = Vector3.MoveTowards(rope.transform.position, Harpoon.transform.position, speedReturn * Time.deltaTime);
        }

    }    
    
}
