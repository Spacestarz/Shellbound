using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class DashAttack_Boss2 : MonoBehaviour
{
    private Rigidbody rb;
    public float dashDistance = 10f;
    private float speed = 2;
    private Vector3 directiondash;
    private float dashduration = 2;
    private Camera cam;

    public GameObject player;
    private Rigidbody rbPlayer;
    private LineRenderer lineRenderer;

    private Vector3 endOfDash;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        cam = Camera.main;
        directiondash = transform.forward;
        rb = GetComponent<Rigidbody>();
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DashAttack()
    {
                     
        Vector3 direction = new Vector3(player.transform.position.x - transform.position.x,
          0f,
         player.transform.position.z - transform.position.z).normalized;

        transform.DOMove(transform.position + direction * dashDistance, dashduration);

        endOfDash = transform.position + direction * dashDistance;
        DrawLine(transform.position, endOfDash);

        Debug.Log("Dash attack");       
    }

    private void DrawLine(Vector3 position, Vector3 endOfDash)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endOfDash);
         
       Color color = Color.yellow;
    }
}