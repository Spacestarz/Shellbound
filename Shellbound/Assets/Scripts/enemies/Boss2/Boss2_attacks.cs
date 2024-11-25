using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;

public class Boss2_attacks : MonoBehaviour
{
    public int KnockbackStrenght; 
    public HealthSystem healthSystem;
    public Rigidbody rbGoblinShark;
    private Rigidbody rbplayer;
    private Collider playerCollider;
    public GameObject player;
    public GameObject Boss2;

   
    private bool timerIsRunning = false;

    private DashAttack_Boss2 callDashAttack;

    
    
    //TODO
    // Make the boss spin? or att least make an aoe attack that knock back the player


    // Start is called before the first frame update
    void Start()
    {
        callDashAttack = GetComponent<DashAttack_Boss2>();

        rbplayer = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();
        rbGoblinShark.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {         
        if (Input.GetKeyDown(KeyCode.F))
        {
            Dashattack();
        }
    }

    public void Dashattack()
    {
        callDashAttack.DashAttack(); 
    }

    public void Mouthattack()
    {
        //Similar to the harpoon and the crabs punch arm can probarly borrow a bit of code from that 
    }
   
}

