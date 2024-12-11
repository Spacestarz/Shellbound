using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Check_shockwave_colliders : MonoBehaviour
{
    /*
    //TODO
    if the player is not on ground 
    the player wont take damage
    */

    public GameObject player;

    public Outer_Ring OuterRing;
    public inner_ring innerring;
    public HealthSystem healthSystem;
    Base_enemy enemy;


    [Header("Change scale of circle")]
    [SerializeField] private float Scale;
    [Header("Damage number")]
    [SerializeField] private int damage = 1;
    [Header("time to reach max size")]
    [SerializeField]private float duration = 2;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {   
       // playerController = GetComponent<PlayerController>();
        transform.localScale = Vector3.zero;
        enemy = GetComponentInParent<Base_enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            shackwave(Scale, duration);
        }

        if (OuterRing.playerPresent ^ innerring.playerPresent && playerController.grounded == true)  
        {
            Debug.Log("TAKE DAMAGE BOYYY");

            healthSystem.TakeDamage(damage);
        }
    }

    private void ResetShockwave()
    {
        transform.localScale = Vector3.zero;
        enemy.atta = true;
    }
    public void shackwave(float WaveDistance, float WaveDuration)
    {
        Scale = WaveDistance;
        duration = WaveDuration;
        Vector3 endScale = new Vector3(1 * Scale, 1 * Scale, 1);
        transform.DOScale(endScale, duration).OnComplete(ResetShockwave);
    }
}
