using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class Crowd_attacks : MonoBehaviour
{
    private GameObject player;

    public GameObject preFabCircle;
    private Vector3 preFabCirclePosition;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            preFabCirclePosition = new Vector3(player.transform.position.x, (float)0.04, player.transform.position.z);
            preFabCircle.transform.position = preFabCirclePosition;

            Instantiate(preFabCircle, preFabCirclePosition, Quaternion.Euler(-90, 0, 0)); //the rotation need to be in -90 degree
        }
    }
   
}








