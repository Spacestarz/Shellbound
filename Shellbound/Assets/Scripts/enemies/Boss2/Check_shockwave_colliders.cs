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
    bool damaged = false;


    [Header("Change scale of circle")]
    [SerializeField] private float Scale;
    [Header("Damage number")]
    [SerializeField] private int damage = 1;
    [Header("time to reach max size")]
    [SerializeField]private float duration = 2;

    [SerializeField] float knockback = 100;

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
            StartCoroutine(shackwave(Scale, duration));
        }

        if (OuterRing.playerPresent ^ innerring.playerPresent && playerController.grounded == true && !damaged)  
        {
            Debug.Log("TAKE DAMAGE BOYYY");
            damaged = true;
            healthSystem.TakeDamage(damage);
            //transform.LookAt(new Vector3(0,healthSystem.transform.position.y,healthSystem.transform.position.z));
            Vector3 vec = (new Vector3(transform.position.x, healthSystem.transform.position.y,transform.position.z) - healthSystem.transform.position).normalized;
            Debug.Log(transform.position + "|" + healthSystem.transform.position);
            healthSystem.transform.GetComponent<PlayerController>().GetKnockedBack();
            healthSystem.transform.GetComponent<Rigidbody>().AddForce(-vec * knockback);
            Debug.DrawLine(healthSystem.transform.position,-healthSystem.transform.position + vec * 1000,Color.red,100);
        }
    }

    private void ResetShockwave()
    {
        transform.localScale = Vector3.zero;
        enemy.start();
        enemy.attacking();
        damaged = false;
    }
    public IEnumerator shackwave(float WaveDistance, float WaveDuration)
    {
        enemy.stop();
        yield return new WaitForSeconds(2);
        Scale = WaveDistance;
        duration = WaveDuration;
        Vector3 endScale = new Vector3(1 * Scale, 1 * Scale, 1);
        transform.DOScale(endScale, duration).OnComplete(ResetShockwave);
        //add push back
    }
}
