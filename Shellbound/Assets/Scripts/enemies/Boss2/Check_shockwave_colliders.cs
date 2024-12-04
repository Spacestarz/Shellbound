using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Check_shockwave_colliders : MonoBehaviour
{

    //if player are in one spere take damage
    //if the player are in both DONT take damage

    //use on triggerstay

    public GameObject player;

    public Outer_Ring OuterRing;
    public inner_ring innerring;
    public HealthSystem healthSystem;


    [Header("Change scale of circle")]
    [SerializeField] private int Scale;
    [Header("Damage number")]
    [SerializeField] private int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 endScale = new Vector3(1 * Scale, 1 * Scale, 1);
            transform.DOScale(endScale, 2f);
        }


        if (OuterRing.playerPresent ^ innerring.playerPresent) 
        {
            Debug.Log("TAKE DAMAGE BOYYY");

            healthSystem.TakeDamage(damage);
        }
    }

}
