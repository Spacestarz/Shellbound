using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class Box_doorhandle : MonoBehaviour
{
    private Vector3 startpos;
    private Vector3 endpos;
    private float pingPongResult;

    private bool stopPingPong = false;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        //endpos = startpos + new Vector3 (0.5f, 0, 0);
                              
    }

    // Update is called once per frame
    void Update()
    {
        //maybe do transform go there and then go there instead of dotween
        //kolla på mathfpingpong

        if (stopPingPong == false)
        {
            Movement();
        }
        else
        {
            transform.position = startpos;
        }
    }

    private void Movement()
    {
        pingPongResult = Mathf.PingPong(Time.time,1);
        transform.position = new Vector3(startpos.x , startpos.y, startpos.z + pingPongResult);
    }

    private void OnTriggerEnter(Collider other)
    {    
        if (other.CompareTag("Harpoon"))
        {
            stopPingPong = true;
            Debug.Log("Auch said the glove");
            

            //transform.DOMove(startpos, 1f);

           // Debug.Log("IM HOME NOW YOU BAD");
        }       
    }
}
