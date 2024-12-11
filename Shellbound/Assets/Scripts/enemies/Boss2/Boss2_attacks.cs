using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;

public class Boss2_attacks : BossAttacksCommon
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
    MouthAttack mouth;
    Check_shockwave_colliders wave;

    
    
    //TODO
    // Make the boss spin? or att least make an aoe attack that knock back the player


    // Start is called before the first frame update
    void Start()
    {
        callDashAttack = GetComponent<DashAttack_Boss2>();
        mouth = GetComponentInChildren<MouthAttack>();
        wave = GetComponentInChildren<Check_shockwave_colliders>();

        rbplayer = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();
        rbGoblinShark.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {         
        if (Input.GetKeyDown(KeyCode.F))
        {
            Dashattack(10,2);
        }
    }

    public void Dashattack(float DashDistance, float DashDuration)
    {
        callDashAttack.DashAttack(DashDistance, DashDuration); 
    }

    public void Mouthattack(float MouthSpeed, float MouthReturn, float MouthDistance)
    {
        //Similar to the harpoon and the crabs punch arm can probarly borrow a bit of code from that 
        mouth.FireMouth(MouthSpeed, MouthReturn, MouthDistance);
    }

    public void shockwave(float WaveDistance, float WaveDuration)
    {
        wave.shackwave(WaveDistance, WaveDuration);
    }
   
}

