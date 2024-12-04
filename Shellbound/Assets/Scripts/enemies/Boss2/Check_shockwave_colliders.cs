using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_shockwave_colliders : MonoBehaviour
{

    //if player are in one spere take damage
    //if the player are in both DONT take damage

    //use on triggerstay

    public GameObject player;
    public Collider SpereColliders;
    public Collider MaskCollider;

    public bool outerSpere = false;
    public bool innerSpere = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            Debug.Log("Hello player");
        }

        if (other.CompareTag("Inner spere"))
        {
            Debug.Log("Inner spere");
        }

        if (other.CompareTag("Outer Spere"))
        {
            Debug.Log("Outer Spere");
        }
     
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Goodbye player");
        }
    }
}
