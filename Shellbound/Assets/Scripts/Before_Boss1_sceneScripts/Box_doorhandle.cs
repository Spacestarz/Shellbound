using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Box_doorhandle : MonoBehaviour
{
    private Vector3 startpos;
    private Vector3 endpos;
    private Vector3 direction;
    private float distance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        startpos = transform.position;
        endpos = transform.position + direction * distance;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
       
    }

    private void Movement()
    {
        transform.DOMove(endpos, 3f);
    }
}
