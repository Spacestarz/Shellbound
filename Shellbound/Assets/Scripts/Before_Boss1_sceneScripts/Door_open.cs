using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Door_open : MonoBehaviour
{
    public Vector3 startpos;
    private Vector3 endpos;
    private float speed = 10f;
    private Box_doorhandle gloveScript;
    private float duration = 1;

    //private Vector3 MOVE;
    private GameObject glove;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        endpos = transform.position + transform.up * 3;
        gloveScript = GetComponentInChildren<Box_doorhandle>();      
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("is the first door done" + gloveScript.firstDoorDone);

        if (Input.GetKeyDown(KeyCode.F) || gloveScript.stopPingPong == true && gloveScript.gloveHome == true &&
            gloveScript.firstDoorDone == false)
        {
            OpenDoor();
           
            //when glove is home make a little like door close and then the big door opens
            gloveScript.gloveHome = false;
                  
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        transform.DOMove(endpos, duration);
        Debug.Log("open");
        gloveScript.firstDoorDone = true;
    }

    public void CloseDoor()
    {
        transform.DOMove(startpos, duration);
        Debug.Log("CLOSE FFS");
    }
}
