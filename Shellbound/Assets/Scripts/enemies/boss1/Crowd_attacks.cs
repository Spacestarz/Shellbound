using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

public class Crowd_attacks : MonoBehaviour
{
    private GameObject player;
    public GameObject preFabCircle;
    private Vector3 preFabCirclePosition;

    private float randomSpawnTime;

    public Boss1_AI Boss1_AI;
   
    /*
    //random range i float
    3-10 sekunder. Ffunkar bra!

    second fas
    ska vara lite snabbare.

    sista fasen ska det vara segare 
    typ 5-12 sekunder

    */

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
   
        StartCoroutine(nameof(Spawn));
    }

    // Update is called once per frame
    void Update()
    {     
        
        Debug.Log("Boss phase is now" + " " + Boss1_AI.phase);
        if (Input.GetKeyDown(KeyCode.R))
        {
           
        }
    }  
    
    IEnumerator Spawn()
    {
        while (true)
        {
            randomSpawnTime = Random.Range(3f, 10f);
            Debug.Log("Spawning another circle in" + " " + randomSpawnTime);
            yield return new WaitForSeconds(randomSpawnTime);

            preFabCirclePosition = new Vector3(player.transform.position.x, (float)0.04, player.transform.position.z);
            preFabCircle.transform.position = preFabCirclePosition;

            Instantiate(preFabCircle, preFabCirclePosition, Quaternion.Euler(-90, 0, 0)); //the rotation need to be in -90 degree
        }   

    }
}








